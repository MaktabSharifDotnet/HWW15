using HWW15.DataAccess;
using HWW15.Enums;
using HWW15.Infrastructure;
using HWW15.Repositories;
using HWW15.Services;

AppDbContext appDbContext = new AppDbContext(); 
UserRepository userRepository = new UserRepository(appDbContext);
UserService userService = new UserService(userRepository);

while (true)
{
    if (LocalStorage.LoginUser==null)
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
                        userService.Register(username , password , roleEnum);
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
      
    }
}
