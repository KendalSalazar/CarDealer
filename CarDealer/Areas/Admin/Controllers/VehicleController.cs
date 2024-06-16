using CarDealer.Data.Repository.Interfaces;
using CarDealer.Models;
using CarDealer.Models.ViewModels;
using CarDealer.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CarDealer.Areas.Admin.Controllers
{
    [Area("Admin")] //Para que solo se pueda acceder por el rol Admin
    [Authorize(Roles = CarDealerRoles.Role_Admin)]

    public class VehicleController : Controller
    {
        private IUnitOfWork unitOfWork; //Es la interfaz ya que gracias a esto se puede usar cualquiera de las clases que hereden de el
        private IWebHostEnvironment webHostEnvironment;

        public VehicleController(IUnitOfWork unitOfWork, IWebHostEnvironment webHostEnvironment)
        {
            this.unitOfWork = unitOfWork;
            this.webHostEnvironment = webHostEnvironment;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Upsert(int? id)
        {
            VehicleVM myModel = new()
            {
                Vehicle = new(),
                VehicleModelList = unitOfWork.VehicleModel.GetAll(includeProperties: "Make").Select(i => new SelectListItem
                {
                    Text = i.Make.Name + " " + i.Name,
                    Value = i.Id.ToString()
                })
            };

            if (id == null || id == 0) //Es insert
            {
                return View(myModel);
            }

            myModel.Vehicle = unitOfWork.vehicle.Get(x => x.Id == id); //=> en donde

            if (myModel.Vehicle == null)
            {
                return NotFound();
            }

            return View(myModel);
        }

        [HttpPost]
        public IActionResult Upsert(VehicleVM vehicleVM, IFormFile? file)
        {
            if (ModelState.IsValid)
            {

                string wwwRootPath = webHostEnvironment.WebRootPath;

                if (file != null)
                {
                    string fileName = Guid.NewGuid().ToString(); //Para generar un string aleatorio muy poco probable de repetirse 
                    string extension = Path.GetExtension(file.FileName);
                    var uploads = Path.Combine(wwwRootPath, @"images\vehicles");

                    if (vehicleVM.Vehicle.PictureUrl != null) //Update
                    {
                        var oldImageUrl = Path.Combine(wwwRootPath, vehicleVM.Vehicle.PictureUrl);

                        if (System.IO.File.Exists(oldImageUrl))
                            System.IO.File.Delete(oldImageUrl);
                    }

                    using (var fileStream = new FileStream(Path.Combine(uploads, fileName + extension), FileMode.Create))
                    {
                        file.CopyTo(fileStream);
                    }

                    vehicleVM.Vehicle.PictureUrl = @"images\vehicles\" + fileName + extension;

                }

                if (vehicleVM.Vehicle.Id == 0)
                {
                    unitOfWork.vehicle.Add(vehicleVM.Vehicle);
                }
                else
                {
                    unitOfWork.vehicle.Update(vehicleVM.Vehicle);
                }

                unitOfWork.Save();
                TempData["success"] = "Vehicle saved succesfully";
            }
            return RedirectToAction("Index");
        }

        #region API

        public IActionResult GetAll()
        {
            var vehicleList = unitOfWork.vehicle.GetAll(includeProperties: "Model,Model.Make");
            return Json(new { data = vehicleList });
        }

        [HttpDelete]
        public IActionResult Delete(int? id)
        {

            Vehicle? vm = unitOfWork.vehicle.Get(x => x.Id == id);

            if (vm == null)
                return Json(new { success = false, message = "Error while deleting" });

            unitOfWork.vehicle.Remove(vm);
            unitOfWork.Save();

            return Json(new { success = true, message = "Deleted successfully" });

        }

        #endregion

    }
}
