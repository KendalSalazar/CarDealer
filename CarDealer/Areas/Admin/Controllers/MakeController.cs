using CarDealer.Data;
using CarDealer.Data.Repository.Interfaces;
using CarDealer.Models;
using CarDealer.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CarDealer.Areas.Admin.Controllers
{
    [Area("Admin")] //Para que solo se pueda acceder por el rol Admin
    [Authorize(Roles = CarDealerRoles.Role_Admin)]
    public class MakeController : Controller
    {
        private IUnitOfWork unitOfWork; //Es la interfaz ya que gracias a esto se puede usar cualquiera de las clases que hereden de el

        public MakeController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        [HttpGet]
        //Si el metodo view no recibe un nombre por parametros, va a retornar la vista que tenga el mismo nombre del metodo
        public IActionResult Index()
        {
            List<Make> makeList = unitOfWork.Make.GetAll().ToList();
            return View(makeList);
            //Leer TempData y lo utilizo
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost] //Los que no tienen etiqueta por defecto es un get
        public IActionResult Create(Make make)
        {
            if (ModelState.IsValid)
            {
                unitOfWork.Make.Add(make);
                unitOfWork.Save();
                TempData["success"] = "Make created succesfully"; //TempData es llave-valor
            }
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Edit(int? id) //En el index, en el edit en asp-route le sigue id porque es el parametro que recibe la vista de edit
        {
            if (id == null || id <= 0)
            {
                return NotFound(); //Siempre es bueno controlar todo tipo de errores
            }

            Make? makeFromDb = unitOfWork.Make.Get(x => x.Id == id); //=>: En donde

            if (makeFromDb == null)
            {
                return NotFound();
            }

            return View(makeFromDb); //Manda por parametros el make a edit.cshtml
        }

        [HttpPost] //Se esta enviando algo desde un form al servidor
        public IActionResult Edit(Make make)
        {
            if (ModelState.IsValid)
            {
                unitOfWork.Make.Update(make);
                unitOfWork.Save();
                TempData["success"] = "Make edited succesfully";
                return RedirectToAction("Index");
            }
            return View();
        }

        [HttpGet]
        public IActionResult Delete(int? id) //En el index, en el delete en asp-route le sigue id porque es el parametro que recibe la vista de edit
        {
            if (id == null || id <= 0)
            {
                return NotFound(); //Siempre es bueno controlar todo tipo de errores
            }

            Make? makeFromDb = unitOfWork.Make.Get(x => x.Id == id); //x no significa nada, puede ser otro

            if (makeFromDb == null)
            {
                return NotFound();
            }

            return View(makeFromDb); //Manda por parametros el make a delete.cshtml
        }

        [HttpPost, ActionName("Delete")] //Esto es debido a que a veces necesitamos un get y post que reciban los mismos parametros
        public IActionResult DeletePOST(int? id)
        {
            Make? makeFromDB = unitOfWork.Make.Get(x => x.Id == id);

            if (makeFromDB == null)
            {
                return NotFound();
            }
            unitOfWork.Make.Remove(makeFromDB);
            unitOfWork.Save();
            TempData["success"] = "Make deleted succesfully";
            return RedirectToAction("Index");
        }

        /*
        [HttpPost]
        public IActionResult Delete(Make obj)
        {
            if (ModelState.IsValid)
            {
                db.Makes.Remove(obj);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View();
        }
        */
    }
}
