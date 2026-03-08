using ElectroLight.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;


namespace ElectroLight.Infrastructure.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) :base(options)
        {
            
        }
        public DbSet<Category> Categories { get; set; }
        //public DbSet<Product> Products { get; set; }

    }
}
