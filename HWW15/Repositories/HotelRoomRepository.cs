using HWW15.DataAccess;
using HWW15.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HWW15.Repositories
{
    public class HotelRoomRepository
    {
        private readonly AppDbContext _context;
        public HotelRoomRepository(AppDbContext context)
        {
            _context = context;
        }
        public void AddRoom(HotelRoom hotelRoom) 
        {
            _context.HotelRooms.Add(hotelRoom);
            _context.SaveChanges();
        }
        public HotelRoom? GetHotelRoomByRoomNumber(string roomNumber) 
        {
          return _context.HotelRooms.FirstOrDefault(h=>h.RoomNumber == roomNumber);
        }

    }
}
