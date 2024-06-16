using CarDealer.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CarDealer.Data
{


    public class ApplicationDbContext : IdentityDbContext
    {
        //Las migraciones lo que hacen es buscar en la BD y en el DBContext que cambios hay para realizarlos ya sea en la BD o en el DBContext
        //Para cada cambio hay que crear una migracion, luego se realiza el update-database
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options){ 
        
        }

        public DbSet<Make> Makes { get; set; }

        public DbSet<VehicleModel> VehicleModels{ get; set; }

        public DbSet<Vehicle> Vehicles { get; set; }

        public DbSet<ApplicationUser> ApplicationUsers { get; set; } //Siempre despues de crear una entidad hay que añadirla al ApplicationDbContext
    }

    //ir a la bd
    //cargar las marcas en una lista
    //enviar la lista a una vista
    //mostrar la vista al usuario
}
