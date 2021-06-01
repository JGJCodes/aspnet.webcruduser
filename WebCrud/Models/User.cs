using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebCrud.Models
{
    public class User
    {
        [Key] //Llave primaria
        public int Id { get; set; }

        /**Propiedades del campo nombre
         * Campo requerido para el registro
         * Longitud minima y maxima del campo
         * Mensajes de error en caso de no cumplir los requerimientos
         * **/
        [Required(ErrorMessage = "The name is required")]
        [Display(Name="Nombre")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "The name must be longer than 3 characters and cannot exceed 50 characters")]
        public String Name { get; set; }

        [Required(ErrorMessage = "The lastname is required")]
        [Display(Name = "Apellidos")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "The lastname must be longer than 3 characters and cannot exceed 50 characters")]
        public String LastName { get; set; }

        
        [Required(ErrorMessage = "The email is required")]
        [Display(Name = "Correo")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "The email must be longer than 3 characters and cannot exceed 50 characters")]
        public String Email { get; set; }

        [Required(ErrorMessage = "The date is required")]
        [Display(Name = "Fecha de nacimiento")]
        public DateTime Date { get; set; }

        //True = Activo, False = Inactivo
        [Display(Name = "Estado")]
        public Boolean State { get; set; }
    }
}
