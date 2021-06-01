using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebCrud.Models;

namespace WebCrud.Data
{
    public class WebCrudContext : DbContext
    {
        public WebCrudContext (DbContextOptions<WebCrudContext> options)
            : base(options)
        {
        }

        public DbSet<WebCrud.Models.User> User { get; set; }
    }
}
