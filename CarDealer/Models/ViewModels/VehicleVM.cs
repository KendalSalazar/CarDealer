using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CarDealer.Models.ViewModels
{
    public class VehicleVM
    {
        //VM es un modelo que se utiliza especificamente para una vista
        //Se necesita la lista de modelos para la creacion de vehiculo

        [ValidateNever]
        public Vehicle Vehicle { get; set; }

        [ValidateNever]
        public IEnumerable<SelectListItem> VehicleModelList { get; set; }
    }
}
