using HWW15.DTOs;
using HWW15.Entities;
using HWW15.Enums;
using HWW15.Infrastructure;
using HWW15.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace HWW15.Services
{
    public class UserService
    {
        private readonly UserRepository _userRepository;
  
        public UserService(UserRepository userRepository )
        {
            _userRepository = userRepository;
            
        }
        public void Register(string username , string password , RoleEnum role) 
        {
            User? user=_userRepository.GetUserByUsername(username);
            if (user != null) 
            {
                throw new Exception("A user with this username has already registered.");           
            }
            User newUser = new User
            {
                Username = username ,
                Password = password ,
                Role = role,
                CreatedAt = DateTime.Now
            };
            _userRepository.AddUser(newUser);
        }
        public void LogIn(string username, string password) 
        {
            User? user = _userRepository.GetUserByUsername(username);
            if (user == null || user.Password != password)
            {
                throw new Exception("The username or password is incorrect..");
            }
           
            LocalStorage.Login(user);
        }

        public User? GetUserByUsername(string userName)
        {
            return _userRepository.GetUserByUsername(userName);

        }

    }
}
