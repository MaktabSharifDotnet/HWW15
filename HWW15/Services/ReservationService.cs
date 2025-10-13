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
    public class ReservationService
    {
        private readonly ReservationRepository _reservationRepository;
        private readonly HotelRoomRepository _hotelRoomRepository;
        public ReservationService(ReservationRepository reservationRepository , HotelRoomRepository hotelRoomRepository)
        {
            _reservationRepository = reservationRepository;
            _hotelRoomRepository = hotelRoomRepository;
        }
        public void CreateReservation(int userId, int hotelRoomId , DateTime checkInDate ,DateTime checkOutDate) 
        {
            if (LocalStorage.LoginUser == null)
            {
                throw new Exception("User is not logged in.");
            }
            if (LocalStorage.LoginUser.Role != RoleEnum.Receptionist)
            {
                throw new Exception("The logged in user is not a Receptionist.");
            }
            Reservation reservation = new Reservation() 
            {
              UserId = userId,
              HotelRoomId = hotelRoomId,
              CheckInDate = checkInDate,
              CheckOutDate = checkOutDate,
              CreatedAt = DateTime.Now,
              Status = StatusEnum.Pending
            };
            _reservationRepository.AddReservation(reservation);
        }
        public List<InfoRoomDto> GetAllRooms() 
        {
          return  _hotelRoomRepository.GetAllRooms();
        }
    }
}
