using HWW15.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HWW15.Infrastructure
{
    public static class LocalStorage
    {
        public static User? LoginUser { get; private set; }

        public static void Login(User user) 
        {
          LoginUser = user;
        }
        public static void Logout() 
        {
            LoginUser = null;
        }
    }
}
