using CompanyDomain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CompanyWebApi.DAL
{
    public class CompanyContext :DbContext
    {

        public DbSet<Company> Companies { get; set; }
        public DbSet<Employee> Employees { get; set; }

        public CompanyContext(DbContextOptions<CompanyContext> options) : base(options) { }
      
        //protected override void OnConfiguring(DbContextOptionsBuilder options)
          //  => options.UseSqlite("Data Source=CompanyDB.db");
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>()
                .Property(e => e.JobTitle)
                .HasConversion(
                p => p.ToString(),
                p => (JobTitles)Enum.Parse(typeof(JobTitles), p));  
        }
    }
}
