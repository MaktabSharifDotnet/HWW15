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
        public void AddRoom(InfoAddRoomDto infoAddRoomDto)
        {
            if (LocalStorage.LoginUser == null)
            {
                throw new Exception("User is not logged in.");
            }
            if (LocalStorage.LoginUser.Role != RoleEnum.Admin)
            {
                throw new Exception("The logged in user is not an admin.");
            }
            HotelRoom? hotel= _hotelRoomRepository.GetHotelRoomByRoomNumber(infoAddRoomDto.RoomNumber);
            if (hotel != null) 
            {
                throw new Exception("A room with the same room number has already been added.");
            }
            RoomDetail roomDetail = new RoomDetail
            {
                Description = infoAddRoomDto.Description,
                HasAirConditioner = infoAddRoomDto.HasAirConditionerBool,
                HasWifi = infoAddRoomDto.HasWifi,
            };
            HotelRoom hotelRoom = new HotelRoom
            {
                RoomNumber = infoAddRoomDto. RoomNumber,
                Capacity = infoAddRoomDto.Capacity,
                PricePerNight = infoAddRoomDto.PricePernight,             
                CreatedAt = DateTime.Now,
                RoomDetail = roomDetail,
            };
            _hotelRoomRepository.AddRoom(hotelRoom);
        }
        public int GetHotelRoomIdByRoomNumber(string roomNumber) 
        {
          HotelRoom? hotelRoomDb = _hotelRoomRepository.GetHotelRoomByRoomNumber(roomNumber);
            if (hotelRoomDb==null)
            {
                throw new Exception("There is no room with that number.");

            }
            return hotelRoomDb.Id;
        }
    }
}
