using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using DbShoppingMallProject.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DbShoppingMallProject.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Stall> Stalls { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Salary> Salaries { get; set; }
        public DbSet<StallManager> StallManagers { get; set; }
        public DbSet<Services> Services { get; set; }
    }

    public class ApplicationUser : IdentityUser
    {
        public DateTime Dob { get; set; }
        public string ImagePath { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Salary> Salaries { get; set; }
    }
    public class Employee
    {
        public int Id { get; set; }
        public string Name { get; set; }

        [ForeignKey("Salary")]
        public int DesigId { get; set; }
        public string Email { get; set; }
        public DateTime DOB { get; set; }
        public int Age { get; set; }
        public decimal TotalSalary { get; set; }
        public string Picture { get; set; }
        public virtual Salary Salary { get; set; }
    }

    public class Salary
    {
        public int Id { get; set; }
        public string Designation { get; set; }
        public decimal Basic { get; set; }
        public decimal Medical { get; set; }
        public decimal HouseRent { get; set; }
        public ICollection<Employee> Employees { get; set; }
    }


    public class StallManager
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public String Email { get; set; }
        public DateTime DOB { get; set; }
        public string Phone { get; set; }
      public string Address { get; set; }
        public string Picture { get; set; }
        [ForeignKey("Stall")]
        public int StallId { get; set; }
        public virtual Stall Stall { get; set; }
    }
    public class Services
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        [ForeignKey("Stall")]
        public int StallId { get; set; }
        public virtual Stall Stalls { get; set; }
    }
}
