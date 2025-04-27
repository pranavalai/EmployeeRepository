using ProjectDAL;
using ProjectModel;

namespace ProjectApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            IDataAccessLayer dataAccessLayer = new XMLDataAccessLayer("Employees.xml");
            EmployeePortalView view = new EmployeePortalView(dataAccessLayer);

            int loginAttempts = 0;
            bool loginSuccess = false;

            while (!loginSuccess && loginAttempts < 3)
            {
                Console.Clear();
                Console.WriteLine("----- Welcome to Employee Portal -----\n");
                Console.WriteLine("1. Login");
                Console.WriteLine("2. Register");
                Console.Write("\nChoose an option (1 or 2): ");
                string option = Console.ReadLine();

                if (option == "1")
                {
                    // Login
                    loginSuccess = view.ShowLogin();
                    if (!loginSuccess)
                    {
                        loginAttempts++;
                        Console.WriteLine("Login failed. Press Enter to try again...");
                        Console.ReadLine();
                    }
                }
                else if (option == "2")
                {
                    // Register
                    view.RegisterUser();
                    Console.WriteLine("\nRegistration successful. Now please login.");
                    Console.WriteLine("Press Enter to continue...");
                    Console.ReadLine();
                }
                else
                {
                    Console.WriteLine("Invalid option. Press Enter to retry...");
                    Console.ReadLine();
                }
            }

            if (!loginSuccess)
            {
                Console.Clear();
                Console.WriteLine("You have reached 3 failed login attempts.");
                Console.WriteLine("Access is blocked. Please try again after 24 hours.");
                Console.ReadLine();
                return; // Exit the application
            }

            // User successfully logged in
            bool running = true;
            while (running)
            {
                view.DisplayMainMenu();
                Console.Write("\nEnter your choice: ");
                if (!int.TryParse(Console.ReadLine(), out int choice))
                {
                    Console.WriteLine("Invalid input. Please enter a number.");
                    Console.ReadLine();
                    continue;
                }

                switch (choice)
                {
                    case 1:
                        view.DisplayViewSubMenu();
                        break;
                    case 2:
                        view.DisplayAddEmployeeScreen();
                        break;
                    case 3:
                        view.DisplayEditEmployeeScreen();
                        break;
                    case 4:
                        view.DisplayDeleteEmployeeScreen();
                        break;
                    case 9:
                        Console.Clear();
                        Console.WriteLine("Thank you for using Employee Portal!");
                        Console.WriteLine("Press Enter to exit...");
                        Console.ReadLine();
                        running = false;
                        break;
                    default:
                        Console.WriteLine("Invalid choice. Press Enter to retry...");
                        Console.ReadLine();
                        break;
                }
            }
        }
    }
}
