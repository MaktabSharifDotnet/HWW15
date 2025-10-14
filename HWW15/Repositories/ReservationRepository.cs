using HWW15.DataAccess;
using HWW15.DTOs;
using HWW15.Entities;
using HWW15.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HWW15.Repositories
{
    public class ReservationRepository
    {
        private readonly AppDbContext _context;
        public ReservationRepository(AppDbContext context)
        {
            _context = context;
        }
        public void AddReservation(Reservation reservation) 
        {
          _context.Reservation.Add(reservation);
          _context.SaveChanges();
        }

        public List<Reservation> GetActiveReservationsByRoomId(int roomId)
        {
            return _context.Reservation
                .Where(r => r.HotelRoomId == roomId &&
                            (r.Status == StatusEnum.Pending || r.Status == StatusEnum.Confirmed))
                .ToList();
        }
        public List<InfoReservationNormalUserDto> GetReservationNormalUser(int userId)
        {
            return _context.Reservation.Where(r => r.UserId == userId).
                 Select(reservation => new InfoReservationNormalUserDto
                 {
                     RoomNumber = reservation.HotelRoom.RoomNumber,
                     CheckInDate = reservation.CheckInDate,
                     CheckOutDate = reservation.CheckOutDate,

                 }).ToList();
        }
    }
}
