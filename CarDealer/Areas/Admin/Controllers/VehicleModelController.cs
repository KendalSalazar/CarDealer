using CarDealer.Data.Repository;
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
    public class VehicleModelController : Controller
    {
        #region Properties_Constructor
        private readonly IUnitOfWork unitOfWork;

        public VehicleModelController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            return View();
        }
        #endregion

        #region HTTP_GET_POST 
        //El region solo sirve como para juntar secciones, es solo estetico
        [HttpGet]
        public IActionResult Upsert(int? id)
        {
            //Si id es null es porque hay un insert
            //En caso contrario es porque hay un update

            VehicleModelVM myModel = new VehicleModelVM();



            myModel.MakeList = unitOfWork.Make.GetAll().Select(i => new SelectListItem
            {
                Text = i.Name,
                Value = i.Id.ToString()
            });

            myModel.VehicleModel = new VehicleModel();

            if (id == null || id <= 0)
                return View(myModel);

            myModel.VehicleModel = unitOfWork.VehicleModel.Get(x => x.Id == id);
            return View(myModel);

        }

        [HttpPost]
        public IActionResult Upsert(VehicleModelVM vm)
        {
            if (ModelState.IsValid)
            {
                if (vm.VehicleModel.Id == 0)
                {
                    unitOfWork.VehicleModel.Add(vm.VehicleModel);
                }
                else
                {
                    unitOfWork.VehicleModel.Update(vm.VehicleModel);
                }
                unitOfWork.Save();
                TempData["success"] = "Model created succesfully"; //TempData es llave-valor
            }
            else
            {
                TempData["error"] = "Error creating model"; //TempData es llave-valor
            }

            return RedirectToAction("Index");
        }
        #endregion

        #region API
        //En API se pueden hacer Get, Post, Put (actualizar un recurso), Delete

        [HttpGet]
        public IActionResult GetAll()
        {
            var modelList = unitOfWork.VehicleModel.GetAll(includeProperties: "Make");
            return Json(new { data = modelList });
        }

        [HttpDelete]
        public IActionResult Delete(int? id)
        {

            VehicleModel? vm = unitOfWork.VehicleModel.Get(x => x.Id == id);

            unitOfWork.VehicleModel.Remove(vm);
            unitOfWork.Save();

            if (vm == null)
                return Json(new { success = false, message = "Error while deleting" });

            return Json(new { success = true, message = "Deleted successfully" });

        }

        #endregion
    }



}
