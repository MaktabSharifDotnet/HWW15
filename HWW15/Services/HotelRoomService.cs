using HWW15.DTOs;
using HWW15.Entities;
using HWW15.Enums;
using HWW15.Infrastructure;
using HWW15.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HWW15.Services
{
    public class HotelRoomService
    {
        private readonly HotelRoomRepository _hotelRoomRepository;
        public HotelRoomService(HotelRoomRepository hotelRoomRepository)
        {
            _hotelRoomRepository = hotelRoomRepository;
        }
        public void AddRoom(HotelRoomWithRoomDetailDto hotelRoomWithRoom)
        {
            if (LocalStorage.LoginUser == null)
            {
                throw new Exception("User is not logged in.");
            }
            if (LocalStorage.LoginUser.Role != RoleEnum.Admin)
            {
                throw new Exception("The logged in user is not an admin.");
            }
            HotelRoom? hotel= _hotelRoomRepository.GetHotelRoomByRoomNumber(hotelRoomWithRoom.HotelRoom.RoomNumber);
            if (hotel != null) 
            {
                throw new Exception("A room with the same room number has already been added.");
            }
            HotelRoom hotelRoom = new HotelRoom
            {
                RoomNumber = hotelRoomWithRoom.HotelRoom. RoomNumber,
                Capacity = hotelRoomWithRoom.HotelRoom.Capacity,
                PricePerNight = hotelRoomWithRoom.HotelRoom.PricePerNight,             
                CreatedAt = DateTime.Now,
                RoomDetail = hotelRoomWithRoom.RoomDetail,
            };
            _hotelRoomRepository.AddRoom(hotelRoom);
        }
    }
}
