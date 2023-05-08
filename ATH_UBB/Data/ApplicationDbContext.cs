﻿using ATH_UBB.Model;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using ATH_UBB.Models;
using ATH_UBB.Areas.Admin.Models;

namespace ATH_UBB.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, int>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {

        }
        public DbSet<Vehicle> Vehicles { get; set; }
        public DbSet<VehicleType> VehicleTypes { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
        public DbSet<RentalPoint> RentalPoints { get; set; }
        public DbSet<ATH_UBB.Models.VehicleItemViewModel>? VehicleItemViewModel { get; set; }
        public DbSet<ATH_UBB.Models.VehicleDetailViewModel>? VehicleDetailViewModel { get; set; }
        public DbSet<ATH_UBB.Models.RentalPointViewModel>? RentalPoionViewModel { get; set; }

        public DbSet<ApplicationRole> Roles { get; set; }
        public DbSet<ApplicationUser> Users { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<ApplicationRole>().HasData(
                new ApplicationRole { Id = 1, Name = "Administrator", Description = "Admini" },
                new ApplicationRole { Id = 2, Name = "Operator", Description = "Operatorzy" },
                new ApplicationRole { Id = 3, Name = "Użytkownik", Description = "Userzy" }
            );

        }
    }
}