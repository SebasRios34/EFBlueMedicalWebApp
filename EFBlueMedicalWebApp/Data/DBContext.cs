using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EFBlueMedicalWebApp.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;

namespace EFBlueMedicalWebApp.Data
{
    public class DBContext : DbContext
    {
        public DBContext(DbContextOptions<DBContext> options) : base(options)
        { 
        }

        public DbSet<Users> Users { get; set; }
        public DbSet<Persona> Personas { get; set; }
        public DbSet<FixedAssets> FixedAssets { get; set; }
        public DbSet<Department> Departments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Users>().ToTable("Users");
            modelBuilder.Entity<Users>(entity =>
            {
                entity.HasKey(x => x.UserID);

            });

            modelBuilder.Entity<Persona>().ToTable("Persona");
            modelBuilder.Entity<Persona>(entity =>
            {
                entity.HasNoKey();

            });

            modelBuilder.Entity<FixedAssets>().ToTable("FixedAssets");
            modelBuilder.Entity<FixedAssets>(entity =>
            {
                entity.HasKey(x => x.AssetID);

            });

            modelBuilder.Entity<Department>().ToTable("Departments");
            modelBuilder.Entity<Department>(entity =>
            {
                entity.HasKey(x => x.DepartmentID);

            });
        }
    }
}
