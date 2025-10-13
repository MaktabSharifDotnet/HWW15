using HWW15.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HWW15.DTOs
{
    public class HotelRoomWithRoomDetailDto
    {
        public HotelRoom HotelRoom { get; set; }
        public RoomDetail RoomDetail { get; set; }
    }
}
