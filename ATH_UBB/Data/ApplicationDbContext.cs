﻿using ATH_UBB.Model;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace ATH_UBB.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
           
        }
        public DbSet<Vehicle> Vehicles { get; set; }
        public DbSet<VehicleType> VehicleTypes { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
        public DbSet<RentalPoint> RentalPoints { get; set; }

    }
}