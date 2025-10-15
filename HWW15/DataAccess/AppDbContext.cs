using HWW15.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HWW15.DataAccess
{
    public class AppDbContext : DbContext
    {
        public DbSet<HotelRoom> HotelRooms { get; set; }
        public DbSet<Reservation> Reservation { get; set; }
        public DbSet<RoomDetail> RoomDetail { get; set; }
        public DbSet<User> Users { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer("Server=DESKTOP-M2BLLND\\SQLEXPRESS;Database=HotelReservationDb;Integrated Security=True;TrustServerCertificate=True;");

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<User>(entity =>
            {
                entity.Property(u => u.Username).HasMaxLength(100).IsRequired();
                entity.HasIndex(u => u.Username).IsUnique();
                entity.Property(u => u.Password).HasMaxLength(100).IsRequired();
                entity.Property(u => u.Role).HasConversion<string>();
                entity.HasMany(u => u.Reservations).WithOne(r => r.User);
                entity.HasData(
                     new User
                     {
                         Id = 1,
                         Username = "admin",
                         Password = "123",
                         Role = Enums.RoleEnum.Admin,
                         CreatedAt = new DateTime(2024, 10, 1)
                     },
                    new User
                    {
                        Id = 2,
                        Username = "reception",
                        Password = "123",
                        Role = Enums.RoleEnum.Receptionist,
                        CreatedAt = new DateTime(2024, 10, 1)
                    }
                );
            });
            modelBuilder.Entity<HotelRoom>(entity =>
            {
                entity.Property(h => h.RoomNumber).HasMaxLength(4).IsRequired();
                entity.HasIndex(h => h.RoomNumber).IsUnique();

                entity.HasOne(h => h.RoomDetail)
                .WithOne(r => r.HotelRoom)
                .HasForeignKey<RoomDetail>(r => r.RoomId);

                entity.HasMany(h => h.Reservations).WithOne(r => r.HotelRoom);

              
            });
            modelBuilder.Entity<RoomDetail>(entity =>
            {
                entity.HasKey(r => r.RoomId);
            });
            modelBuilder.Entity<HotelRoom>().HasData(
                new HotelRoom
                {
                    Id = 1,
                    RoomNumber = "101",
                    Capacity = 2,
                    PricePerNight = 150,
                    CreatedAt = new DateTime(2024, 10, 1)
                },
                new HotelRoom
                {
                    Id = 2,
                    RoomNumber = "102",
                    Capacity = 4,
                    PricePerNight = 250,
                    CreatedAt = new DateTime(2024, 10, 1)
                }
            );
            modelBuilder.Entity<RoomDetail>().HasData(
                new RoomDetail
                {
                    RoomId = 1,
                    Description = "A standard double room.",
                    HasWifi = true,
                    HasAirConditioner = true
                },
                new RoomDetail
                {
                    RoomId = 2, 
                    Description = "A spacious family room.",
                    HasWifi = true,
                    HasAirConditioner = false
                }
            );

            modelBuilder.Entity<Reservation>(entity =>
            {
                entity.Property(r => r.Status).HasConversion<string>();
            });
            
        }
    }
}

