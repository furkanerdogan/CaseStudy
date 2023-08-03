
using Entities;
using Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete.EntityFramewrok.Context
{
    public class MyDbContext : DbContext

    {

    

        public MyDbContext(DbContextOptions<MyDbContext> options) : base(options) { }
        public MyDbContext()
        {

        }
  
        public DbSet<Restaurant> Restaurants { get; set; }
    
    }
}
