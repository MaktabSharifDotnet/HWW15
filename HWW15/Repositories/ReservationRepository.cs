using HWW15.DataAccess;
using HWW15.Entities;
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


    }
}
