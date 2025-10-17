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
        public void CreateReservation(int hotelRoomId, int userId, DateTime checkInDate, DateTime checkOutDate)
        {

            if (LocalStorage.LoginUser == null)
            {
                throw new Exception("User is not logged in.");
            }
            if (LocalStorage.LoginUser.Role != RoleEnum.Receptionist)
            {
                throw new Exception("The logged in user is not a Receptionist.");
            }

            if (checkInDate.Date < DateTime.Now.Date)
            {
                throw new Exception("Check-in date cannot be in the past.");
            }
            

            var activeReservations = _reservationRepository.GetActiveReservationsByRoomId(hotelRoomId);


            foreach (var existingReservation in activeReservations)
            {

                bool isOverlap = checkInDate < existingReservation.CheckOutDate && checkOutDate > existingReservation.CheckInDate;

                if (isOverlap)
                {

                    throw new Exception($"The room is already booked from {existingReservation.CheckInDate.ToShortDateString()} to {existingReservation.CheckOutDate.ToShortDateString()}.");
                }
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

        public List<InfoReservationNormalUserDto> GetReservationNormalUser()
        {
            if (LocalStorage.LoginUser == null)
            {
                throw new Exception("User is not logged in.");
            }
            if (LocalStorage.LoginUser.Role != RoleEnum.NormalUser)
            {
                throw new Exception("The logged in user is not a NormalUser.");
            }
            return _reservationRepository.GetReservationNormalUser(LocalStorage.LoginUser.Id);
        }

        public void UpdateReservationStatus(int reservationId, StatusEnum newStatus)
        {
            if (LocalStorage.LoginUser == null)
            {
                throw new Exception("User is not logged in.");
            }
            if (LocalStorage.LoginUser.Role != RoleEnum.Receptionist)
            {
                throw new Exception("The logged in user is not a Receptionist.");
            }

            var reservation = _reservationRepository.GetReservationById(reservationId);
            if (reservation == null)
            {
                throw new Exception("Reservation not found.");
            }

            reservation.Status = newStatus;
            _reservationRepository.UpdateReservation(reservation);
        }

        public List<Reservation> GetAllReservations()
        {
            if (LocalStorage.LoginUser == null || LocalStorage.LoginUser.Role != RoleEnum.Receptionist)
            {
                throw new Exception("Unauthorized access.");
            }
            return _reservationRepository.GetAllReservations();
        }
    }
}