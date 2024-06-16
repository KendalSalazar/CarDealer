using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CarDealer.Models.ViewModels
{
    public class VehicleModelVM
    {
        [ValidateNever]
        public VehicleModel VehicleModel{get; set;}



        //SelectListItem lo convierte como un combobox y agrega elemento por elemento, no hace falta un foreach
        [ValidateNever]
        public IEnumerable<SelectListItem> MakeList{ get; set;} 
    }
}
