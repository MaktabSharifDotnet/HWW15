using HWW15.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HWW15.DataAccess
{
    public class AppDbContext :DbContext
    {
        public DbSet<HotelRoom> HotelRooms { get; set; }
        public DbSet<Reservation> Reservation { get; set; }
        public DbSet<RoomDetail> RoomDetail { get; set; }
        public DbSet<User> Users { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer("Server=DESKTOP-M2BLLND\\SQLEXPRESS;Database=HotelReservationDb;Integrated Security=True;TrustServerCertificate=True;")
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<User>(entity =>
            {
                entity.Property(u=>u.Username).HasMaxLength(100).IsRequired();
                entity.HasIndex(u => u.Username).IsUnique();
                entity.Property(u => u.Password).HasMaxLength(100).IsRequired();
                entity.Property(u => u.Role).HasConversion<string>();
            });
            modelBuilder.Entity<HotelRoom>(entity =>
            {
                entity.Property(h => h.RoomNumber).HasMaxLength(4).IsRequired();
                entity.HasIndex(h => h.RoomNumber).IsUnique();

                entity.HasOne(h => h.RoomDetail)
                .WithOne(r=>r.HotelRoom)
                .HasForeignKey<RoomDetail>(r => r.RoomId);
            });          
        }
    }
}

