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
    public class UserRepository
    {
        private readonly AppDbContext _context;
        public UserRepository(AppDbContext context)
        {
            _context = context;
        }
        public User? GetUserByUsername (string userName) 
        {
          return _context.Users.FirstOrDefault(u=>u.Username == userName);          
        }      
        public void AddUser(User user) 
        {
           _context.Users.Add(user);
           _context.SaveChanges();
        }

      
    }
}
