using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebCrud.Models;

namespace WebCrud.Data
{
    public class DbInitializer
    {
        /**
       * Metodo inicializador de la Base de Datos
       * Recibe un parametro contexto vacio y le 
       * asigna los valores de la BD
       * **/
        public static void Initialize(WebCrudContext context)
        {
            context.Database.EnsureCreated();//Crea la Base de Datos

            //Buscar si existen registros en la tabla User
            if (context.User.Any())
            {
                return;
            }

            //Crea un objeto User para asignarlo en la tabla
            User user1 = new User();
            user1.Name = "user";
            user1.LastName = "main";
            user1.Date = DateTime.Now;
            user1.State = true;
            user1.Email = "user@example.com";

            context.User.Add(user1); //Agrega un registro a la tabla

            context.SaveChanges();//Guarda los cambios realizados
        }
    }
}
