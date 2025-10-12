using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HWW15.Entities
{
    public class RoomDetail
    {
        public int RoomId { get; set; }
        public string Description { get; set; }

        public bool HasWifi { get; set; }

        public bool HasAirConditioner { get; set; }



    }
}
