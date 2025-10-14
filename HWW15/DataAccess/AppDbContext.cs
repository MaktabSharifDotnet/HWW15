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
            optionsBuilder.UseSqlServer("Server=DESKTOP-M2BLLND\\SQLEXPRESS;Database=HotelReservationDb_Practice;Integrated Security=True;TrustServerCertificate=True;");

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<User>(user =>
            {
                user.Property(u=>u.Username).HasMaxLength(100).IsRequired();
                user.Property(u=>u.Password).HasMaxLength(100).IsRequired();
                user.HasIndex(u=>u.Username).IsUnique();
                user.Property(u => u.Role).HasConversion<string>();
                user.HasMany(u => u.Reservations).WithOne(r => r.User).HasForeignKey(r=>r.UserId);
                user.HasData(new User
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
            modelBuilder.Entity<HotelRoom>(room =>
            {

                room.HasMany(r => r.Reservations).WithOne(reservation => reservation.HotelRoom);
                room.Property(r=>r.RoomNumber).HasMaxLength(4).IsRequired();
                room.HasIndex(r=>r.RoomNumber).IsUnique();
                room.HasOne(r => r.RoomDetail).WithOne(rd => rd.HotelRoom).HasForeignKey<RoomDetail>(rd=>rd.RoomId);
                room.HasData(new HotelRoom
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
            });
            modelBuilder.Entity<RoomDetail>(rd =>
            {
                rd.HasKey(r => r.RoomId);   
            });
        }
    }
}

