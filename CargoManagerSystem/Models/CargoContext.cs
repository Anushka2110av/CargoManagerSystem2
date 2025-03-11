using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using CargoClass;

namespace CargoManagerSystem.Models
{
    public class CargoContext:DbContext
    {
          public DbSet<Employee> Employees { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<CargoFare> CargoFares { get; set; }
        public DbSet<CargoOrder> CargoOrders { get; set; }
        public DbSet<CargoType> CargoTypes { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<Administrator> Administrators { get; set; }
        public DbSet<Pricing> Pricings { get; set; }

    }
}