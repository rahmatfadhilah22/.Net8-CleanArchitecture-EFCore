﻿using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Database
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options) { }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Suppliers> Suppliers { get; set; }
        public DbSet<Categories> Categories { get; set; }
        public DbSet<Products> Products { get; set; }
        public DbSet<Customers> Customers { get; set; }
        public DbSet<Shippers> Shippers { get; set; }
        public DbSet<Region> Region { get; set; }
        public DbSet<Territories> Territories { get; set; }
        public DbSet<Orders> Orders { get; set; }
        public DbSet<OrderDetails> OrderDetails { get; set; }
    }
}
