using CarDealer.Data.Repository.Interfaces;
using CarDealer.Models;
using Microsoft.AspNetCore.Mvc;

namespace CarDealer.Areas.Customer.Controllers
{
    [Area("Customer")] //Para que solo se pueda acceder por el rol Customer
    public class HomeController : Controller
    {

        private readonly IUnitOfWork unitOfWork;

        public HomeController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            IEnumerable<Vehicle> vehicleList = unitOfWork.vehicle.GetAll(includeProperties: "Model,Model.Make");
            return View(vehicleList);
        }

        public IActionResult Details(int id)
        {
            Vehicle vehicle = unitOfWork.vehicle.Get(v => v.Id == id, "Model,Model.Make");
            if (vehicle == null)
            {
                return NotFound();
            }

            return View(vehicle);
        }
    }
}
