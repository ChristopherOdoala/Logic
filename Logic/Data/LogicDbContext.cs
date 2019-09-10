using Logic.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Logic.Data
{
    public class LogicDbContext : DbContext
    {
        public LogicDbContext(DbContextOptions dbContext):base(dbContext)
        {

        }

        public DbSet<Department> departments { get; set; }
        public DbSet<Employee> employees { get; set; }
        public DbSet<Status> status { get; set; }
    }
}
