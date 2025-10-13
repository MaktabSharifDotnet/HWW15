using HWW15.DataAccess;
using HWW15.DTOs;
using HWW15.Entities;
using HWW15.Enums;
using HWW15.Infrastructure;
using HWW15.Repositories;
using HWW15.Services;

AppDbContext appDbContext = new AppDbContext();
UserRepository userRepository = new UserRepository(appDbContext);
HotelRoomRepository hotelRoomRepository = new HotelRoomRepository(appDbContext);
UserService userService = new UserService(userRepository);
HotelRoomService hotelRoomService  = new HotelRoomService(hotelRoomRepository);

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
                            HotelRoomWithRoomDetailDto hotelRoomWithRoom =GetHotelRoomAndRoomDetail(infoAddRoomDto.Description , infoAddRoomDto.HasWifi ,infoAddRoomDto.HasAirConditionerBool , infoAddRoomDto.RoomNumber , infoAddRoomDto.Capacity);
                            try 
                            {
                                hotelRoomService.AddRoom(hotelRoomWithRoom);
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
                break;
            case RoleEnum.NormalUser:
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

HotelRoomWithRoomDetailDto GetHotelRoomAndRoomDetail( string description ,bool hasWifiBool ,bool hasAirConditionerBool , string roomNumber , int capacity) 
{
    RoomDetail roomDetail = new RoomDetail
    {
        Description = description,
        HasWifi = hasWifiBool,
        HasAirConditioner = hasAirConditionerBool,
    };
    HotelRoom hotelRoom = new HotelRoom
    {
        RoomNumber = roomNumber,
        Capacity = capacity,
        RoomDetail = roomDetail
    };
    HotelRoomWithRoomDetailDto hotelRoomWithRoomDetailDto = new HotelRoomWithRoomDetailDto
    {
        RoomDetail = roomDetail,
        HotelRoom = hotelRoom,
    };
    return hotelRoomWithRoomDetailDto;
}
