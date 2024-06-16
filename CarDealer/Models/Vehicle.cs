using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarDealer.Models
{
    public class Vehicle
    {
        [Key] //No es necesario si el atributo se llama Id
        public int Id { get; set; }

        //Validar que el año no sea menor que 1950 y no sea mayor que el año actual +1 (custom validator)
        [Required]
        [YearValidation("Year not valid")]
        public int Year { get; set; }

        [Required]
        public double Price { get; set; }

        [Required]
        public string Description { get; set; }

        public string? PictureUrl { get; set; }

        [DisplayName("Make/Model")] //La etiqueta pondra este texto
        public int ModelId { get; set; }

        [Required]
        [ForeignKey("ModelId")]
        public VehicleModel Model { get; set; }


    }

    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    class YearValidation : ValidationAttribute { 
        public YearValidation(string errorMessage) {
            ErrorMessage = errorMessage;
        }

        public override bool IsValid(object value)
        {
            if ((int)value >= 1950 && (int)value<= DateTime.Now.Year+1) {
                return true;
            }
            return false;
        }
    }

}

//Crear migracion y actualizar BD
//En el applicationDBContext se debe agregar el dbset de vehicle para luego hacer el add-migration [nombre de la migracion]
//Luego se hace el update-database

//Crear repositorio de vehicle
//Se crea la interfaz IVehicleRepository
//Se crear la clase concreta (VehicleRepository)
//Se agrega la interfaz al IUnitOfWork
//Se agrega la interfaz al UnitOfWork

//CRUD de vehiculos
//Creando Endpoints y utilizando DataTables, SweetAlert, toastr, bootStrapIcons
