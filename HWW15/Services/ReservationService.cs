using HWW15.Enums;
using HWW15.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HWW15.Services
{
    public class ReservationService
    {
        public void AddReservation() 
        {
            if (LocalStorage.LoginUser == null)
            {
                throw new Exception("User is not logged in.");
            }
            if (LocalStorage.LoginUser.Role != RoleEnum.Receptionist)
            {
                throw new Exception("The logged in user is not a Receptionist.");
            }

        }
    }
}
