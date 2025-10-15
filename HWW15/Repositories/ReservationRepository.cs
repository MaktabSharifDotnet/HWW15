using HWW15.DataAccess;
using HWW15.DTOs;
using HWW15.Entities;
using Microsoft.EntityFrameworkCore.Query.Internal;
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
        public List<InfoReservationDto> GetStatusRoomByRoomId(int roomId) 
        {
           return _context.Reservation.Where(r => r.HotelRoomId == roomId)
                 .Select(reservation => new InfoReservationDto
                 {
                     Id = reservation.Id,
                     CheckInDate = reservation.CheckInDate,
                     CheckOutDate = reservation.CheckOutDate,
                 }).ToList();
        }
        public void AddReservation ( Reservation reservation) 
        {
            _context.Reservation.Add(reservation);
            _context.SaveChanges();
        }
    }
}
