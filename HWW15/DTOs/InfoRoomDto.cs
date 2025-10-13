using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HWW15.DTOs
{
    public class InfoRoomDto
    {
        public string RoomNumber { get; set; }
        public int PricePerNight { get; set; }
        public string Description { get; set; }
        public bool HasWifi { get; set; }
        public bool HasAirConditioner { get; set; }
    }
}
