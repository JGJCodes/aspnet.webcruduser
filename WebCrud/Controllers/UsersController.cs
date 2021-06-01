using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebCrud.Data;
using WebCrud.Models;

namespace WebCrud.Controllers
{
    public class UsersController : Controller
    {
        private readonly WebCrudContext _context;

        public UsersController(WebCrudContext context)
        {
            _context = context;
        }

        //Metodo Index que realiza el filtrado de datos 
        public async Task<IActionResult> Index(string sortOrden, string searchString, string currentFilter, int? page)
        {
            //Ordenar datos por columna
            ViewData["NombreSortParm"] = String.IsNullOrEmpty(sortOrden) ? "nombre_desc" : "";
            ViewData["ApellidosSortParm"] = sortOrden == "apellidos_asc" ? "apellidos_desc" : "apellidos_asc";
            ViewData["CorreoSortParm"] = sortOrden == "correo_asc" ? "correo_desc" : "correo_asc";
            ViewData["FechaSortParm"] = sortOrden == "fecha_asc" ? "fecha_desc" : "fecha_asc";
            ViewData["EstadoSortParm"] = sortOrden == "estado_asc" ? "estado_desc" : "estado_asc";
            
            //Verifica si existe alguna busqueda u ordenacion en la pagina
            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            //Filtro de busqueda
            ViewData["CurrentFilter"] = searchString;
            //Filtro de ordenacion
            ViewData["CurrentSort"] = sortOrden;

            //Obtiene los datos de la tabla Categoria
            var users = from s in _context.User select s;

            if (!String.IsNullOrEmpty(searchString)) //Analiza si se esta realizando una busqueda de datos
            {
                users = users.Where(s => s.Name.Contains(searchString));
            }

            switch (sortOrden) //Analisa los diferentes tipos de ordenamiento de filas
            {
                case "nombre_desc": users = users.OrderByDescending(s => s.Name); break;
                case "apellidos_desc": users = users.OrderByDescending(s => s.LastName); break;
                case "apellidos_asc": users = users.OrderBy(s => s.LastName); break;
                case "correo_asc": users = users.OrderBy(s => s.Email); break;
                case "correo_desc": users = users.OrderByDescending(s => s.Email); break;
                case "fecha_asc": users = users.OrderBy(s => s.Date); break;
                case "fecha_desc": users = users.OrderByDescending(s => s.Date); break;
                case "estado_asc": users = users.OrderBy(s => s.State); break;
                case "estado_desc": users = users.OrderByDescending(s => s.State); break;
                default: users = users.OrderBy(s => s.Name); break;
            }

            //return View(await categorias.AsNoTracking().ToListAsync());
            //return View(await _context.Categoria.ToListAsync());

            int pageSize = 3;//tamaño total de paginas de division de datos
            return View(await Paginacion<User>.CreateAsync(users.AsNoTracking(), page ?? 1, pageSize));
        }


        // GET: Users
        public async Task<IActionResult> Index()
        {
            return View(await _context.User.ToListAsync());
        }

        // GET: Users/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.User
                .FirstOrDefaultAsync(m => m.Id == id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // GET: Users/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Users/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,LastName,Email,Date,State")] User user)
        {
            if (ModelState.IsValid)
            {
                _context.Add(user);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(user);
        }

        // GET: Users/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.User.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }

        // POST: Users/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,LastName,Email,Date,State")] User user)
        {
            if (id != user.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(user);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserExists(user.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(user);
        }

        // GET: Users/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.User
                .FirstOrDefaultAsync(m => m.Id == id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var user = await _context.User.FindAsync(id);
            _context.User.Remove(user);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UserExists(int id)
        {
            return _context.User.Any(e => e.Id == id);
        }
    }
}
