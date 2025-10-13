using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HWW15.DTOs
{
    public class InfoAddRoomDto
    {
        public  string RoomNumber { get; set; }
        public int Capacity { get; set; }
        public int PricePernight { get; set; }
        public  string Description { get; set; }
        public bool HasWifi { get; set; }
        public bool HasAirConditionerBool { get; set; }
    }
}
