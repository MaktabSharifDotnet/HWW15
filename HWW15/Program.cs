using HWW15.DataAccess;
using HWW15.DTOs;
using HWW15.Entities;
using HWW15.Enums;
using HWW15.Infrastructure;
using HWW15.Repositories;
using HWW15.Services;
using System;
using System.Collections.Generic;
using System.Net.Http.Headers;

AppDbContext appDbContext = new AppDbContext();
UserRepository userRepository = new UserRepository(appDbContext);
HotelRoomRepository hotelRoomRepository = new HotelRoomRepository(appDbContext);
ReservationRepository reservationRepository = new ReservationRepository(appDbContext);
UserService userService = new UserService(userRepository);
HotelRoomService hotelRoomService = new HotelRoomService(hotelRoomRepository);
ReservationService reservationService = new ReservationService(reservationRepository,hotelRoomRepository);

while (true)
{
    if (LocalStorage.LoginUser == null)
    {
        Console.WriteLine("1.Register or 2. Login");
        try
        {
            int choice = int.Parse(Console.ReadLine()!);
            switch (choice)
            {
                case 1:
                    Console.WriteLine("please enter Username");
                    string username = Console.ReadLine();
                    Console.WriteLine("please enter password");
                    string password = Console.ReadLine();
                    Console.WriteLine("please enter Role 1.Admin , 2.Receptionist , 3.NormalUser");
                    try
                    {
                        int role = int.Parse(Console.ReadLine()!);
                        RoleEnum roleEnum = (RoleEnum)role;
                        userService.Register(username, password, roleEnum);
                        Console.WriteLine("Register is done");
                    }
                    catch (FormatException)
                    {
                        Console.WriteLine
                         ("invalid option Please enter the number of one of these three options. 1.Admin , 2.Receptionist , 3.NormalUser");

                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                        Console.WriteLine("Registration failed.");
                    }
                    break;
                case 2:
                    Console.WriteLine("please enter Username");
                    username = Console.ReadLine();
                    Console.WriteLine("please enter password");
                    password = Console.ReadLine();
                    try
                    {
                        userService.LogIn(username, password);
                        Console.WriteLine("login is done");
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                        Console.WriteLine("Login failed.");
                    }
                    break;
            }
        }
        catch (FormatException)
        {
            Console.WriteLine
            ("invalid option Please enter the number of one of these two options. 1.Register or 2. Login");
        }

    }
    else
    {
        User user = LocalStorage.LoginUser;
        switch (user.Role)
        {
            case RoleEnum.Admin:

                Console.WriteLine("Please select an option.");
                Console.WriteLine("1.Add Room 2.Exit");
                try
                {
                    int choice = int.Parse(Console.ReadLine()!);
                    switch (choice)
                    {
                        case 1:
                            InfoAddRoomDto infoAddRoomDto = GetInfoForAddRoom();

                            try
                            {
                                hotelRoomService.AddRoom(infoAddRoomDto);
                                Console.WriteLine("add Room is done");
                            }
                            catch (Exception e)
                            {
                                Console.WriteLine(e.Message);
                                Console.WriteLine("addroom failed");
                            }

                            break;
                        case 2:
                            LocalStorage.Logout();
                            break;
                    }
                }
                catch (FormatException)
                {
                    Console.WriteLine("invalid option Please select one of the options provided.1.Add Room 2.Exit");
                }
                break;
            case RoleEnum.Receptionist:
                Console.WriteLine("Please select an option.");
                Console.WriteLine("1.AddReservation , 2.Exit");
                try
                {
                    int choice = int.Parse(Console.ReadLine()!);
                    switch (choice)
                    {
                        
                        case 1:
                            Console.WriteLine("please enter Checkin");
                            DateTime checkIn = DateTime.Parse(Console.ReadLine()!);
                            Console.WriteLine("please enter CheckOut");
                            DateTime checkOut = DateTime.Parse(Console.ReadLine()!);
                            Console.WriteLine("please enter roomId");
                            ShowFreeRoom(checkIn, checkOut);
                            try
                            {
                                int roomId = int.Parse(Console.ReadLine()!);
                                Console.WriteLine("please enter username");
                                string username = Console.ReadLine()!;
                                User? userDb = userService.GetUserByUsername(username);
                                if (userDb == null)
                                {
                                    Console.WriteLine("please enter password");
                                    string pass = Console.ReadLine()!;
                                    userService.Register(username, pass, RoleEnum.NormalUser);
                                    userDb = userService.GetUserByUsername(username);
                                }
                                reservationService.CreateReservation(userDb.Id, roomId, checkIn, checkOut);
                                Console.WriteLine("reservation is done");

                            }
                            catch (FormatException e)
                            {
                                Console.WriteLine("invalid roomId");
                            }
                            catch (Exception e)
                            {
                                Console.WriteLine(e.Message);
                            }

                            break;
                        case 2:
                            LocalStorage.Logout();
                            break;
                    }
                }
                catch (FormatException)
                {
                    Console.WriteLine("invalid option Please select one of the options provided. 1.AddReservation , 2.Exit");
                }
                break;
            case RoleEnum.NormalUser:
                Console.WriteLine("Please select an option.");
                Console.WriteLine("1.ShowInfoReservation , 2.Exit");
                try 
                {
                    int choice = int.Parse(Console.ReadLine()!);
                    switch (choice)
                    {
                        case 1:
                            ShowInfoReservation();
                            break;
                        case 2:
                            LocalStorage.Logout();
                            break;

                    }
                }
                catch (FormatException) 
                {
                    Console.WriteLine("invalid option Please select one of the options provided.1.ShowInfoReservation , 2.Exit\"");
                }
                
                break;
        }
    }
}
InfoAddRoomDto GetInfoForAddRoom()
{
    Console.WriteLine("please enter roomNumber");
    string roomNumber = Console.ReadLine();
    Console.WriteLine("please enter capacity:");
    int capacity = int.Parse(Console.ReadLine()!);
    Console.WriteLine("please enter pricePerNight");
    int pricePernight = int.Parse(Console.ReadLine()!);
    Console.WriteLine("please enter Description:");
    string description = Console.ReadLine()!;
    Console.WriteLine("HasWifi ? 1.yes 2.No");
    bool hasWifiBool = false;
    try
    {
        int hasWifi = int.Parse(Console.ReadLine()!);
        if (hasWifi == 1)
        {
            hasWifiBool = true;
        }
        else
        {
            hasWifiBool = false;
        }
    }
    catch (FormatException)
    {
        Console.WriteLine("invalid option Please select one of the options provided. HasWifi ? 1.yes 2.No");
    }
    Console.WriteLine("HasAirConditioner ? 1.yes 2.No");
    bool hasAirConditionerBool = false;
    try
    {
        int hasAirConditioner = int.Parse(Console.ReadLine()!);
        if (hasAirConditioner == 1)
        {
            hasAirConditionerBool = true;
        }
        else
        {
            hasAirConditionerBool = false;
        }
    }
    catch (FormatException)
    {
        Console.WriteLine("invalid option Please select one of the options provided. HasAirConditioner ? 1.yes 2.No");
    }
    InfoAddRoomDto infoAddRoomDto = new InfoAddRoomDto
    {
        RoomNumber = roomNumber,
        Capacity = capacity,
        PricePernight = pricePernight,
        Description = description,
        HasWifi = hasWifiBool,
        HasAirConditionerBool = hasAirConditionerBool
    };
    return infoAddRoomDto;
}




void ShowInfoReservation()
{
    List<InfoReservationNormalUserDto> infoReservations = reservationService.GetReservationNormalUser();
    foreach (var info in infoReservations)
    {
        Console.WriteLine($"RoomNumber : {info.RoomNumber} , CheckIn : {info.CheckInDate} , CheckOut : {info.CheckOutDate}");
    }
}
void ShowFreeRoom(DateTime checkIn, DateTime checkOut)
{
    List<HotelRoom> rooms = hotelRoomService.GetFreeRooms(checkIn, checkOut);
    foreach (var room in rooms)
    {
        Console.WriteLine($"RoomId :{room.Id} , roomNumber{room.RoomNumber} , capacity : {room.Capacity} , " +
            $"PricePerNight : {room.PricePerNight} , Description : {room.RoomDetail.Description} , " +
            $"hasWifi : {room.RoomDetail.HasWifi} , HasAirConditioner : {room.RoomDetail.HasAirConditioner}");
    }
}


