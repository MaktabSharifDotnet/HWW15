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
        public ReservationService(ReservationRepository reservationRepository, HotelRoomRepository hotelRoomRepository)
        {
            _reservationRepository = reservationRepository;
            _hotelRoomRepository = hotelRoomRepository;
        }
        public void AddReservation(int userId , int roomId , DateTime checkInDate , DateTime checkOutDate) 
        {
            if (LocalStorage.LoginUser == null)
            {
                throw new Exception("User is not logged in.");
            }
            if (LocalStorage.LoginUser.Role != RoleEnum.Receptionist)
            {
                throw new Exception("The logged in user is not a Receptionist.");
            }
            List<InfoReservationDto> infoReservations= _reservationRepository.GetStatusRoomByRoomId(roomId);
            foreach (var info in infoReservations)
            {
                if (info.CheckInDate<checkOutDate && info.CheckOutDate>checkInDate)
                {
                    throw new Exception($"The room is already booked from {info.CheckInDate.ToShortDateString()} to {info.CheckOutDate.ToShortDateString()}.");
                }
            }
            Reservation reservation = new Reservation() 
            {
               UserId = userId,
               HotelRoomId = roomId,
               CheckInDate = checkInDate,
               CheckOutDate = checkOutDate,
               Status  = StatusEnum.Pending,
               CreatedAt = DateTime.Now,
            };
            _reservationRepository.AddReservation(reservation);
        }
    }
}
