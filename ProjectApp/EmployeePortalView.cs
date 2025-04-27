using ProjectDAL;
using ProjectModel;

namespace ProjectApp
{
    public class EmployeePortalView
    {
        private IDataAccessLayer _dal;
        public EmployeePortalView(IDataAccessLayer dal)
        {
             _dal = dal;
        }

        public void AuthenticateUser()
        {
            Console.WriteLine("1. Login");
            Console.WriteLine("2. Register");
            Console.WriteLine("Select an option (1 or 2): ");
            var option = Console.ReadLine();

            if (option == "1")
            {
                ShowLogin();
            }
            else if (option == "2")
            {
                RegisterUser();
            }
            else
            {
                Console.WriteLine("Invalid option.");
            }
        }

        public bool ShowLogin()
        {
            Console.WriteLine("Enter username: ");
            var name = Console.ReadLine();
            Console.WriteLine("Enter password: ");
            var pwd = Console.ReadLine();

            User input = new User()
            {
                UserName = name,
                Password = pwd
            };

            bool isVerified = Login.Authentication(input);

            return isVerified;
        }

        public void RegisterUser()
        {
            Console.WriteLine("Enter a username for new account: ");
            var name = Console.ReadLine();
            Console.WriteLine("Enter a password for new account: ");
            var pwd = Console.ReadLine();

            // Create new user object
            User newUser = new User()
            {
                UserName = name,
                Password = pwd
            };

            // Register the user (add to XML)
            bool isRegistered = Register.NewUser(newUser);

            if (isRegistered)
            {
                Console.WriteLine("Registration successful.");
            }
            else
            {
                Console.WriteLine("Registration failed. Username may already exist.");
            }
        }

        public void DisplayMainMenu()
        {
            Console.Clear();
            Console.WriteLine("------ Employee Portal Main Menu --------");
            Console.WriteLine("1. View\t\t2. Add\t\t3. Edit\t\t4. Delete\t\t9. Exit");
        }
        public void DisplayViewSubMenu()
        {
            try
            {
                Console.Clear();
                Console.WriteLine("-------- View Employees option ------------");
                Console.WriteLine("a. All Employees\t\tb. By Department\t\tc. By Id");
                Console.Write("\nEnter your choice - ");
                var choice = Console.ReadLine();
                Console.WriteLine();
                switch (choice)
                {
                    case "a":
                        var employees = _dal.ViewAllEmployees();
                        if (employees.Count>=1)
                        {
                            foreach (var emp in employees)
                            {
                                Console.WriteLine(emp + "\n");
                            }
                        }
                        else
                        {
                            Console.WriteLine("Sorry, no data found in the database!!!");
                        }
                        break;
                    case "b":
                        try
                        {
                            Console.Write("1. IT\t2. HR\t3. Admin (please enter number only) - ");
                            var deptChoiceInput = Console.ReadLine();
                            if (!int.TryParse(deptChoiceInput, out int deptChoice))
                            {
                                Console.WriteLine("Invalid input! Please enter a valid number for department choice.");
                                break;
                            }

                            string department = string.Empty;
                            switch (deptChoice)
                            {
                                case 1:
                                    department = "IT";
                                    break;
                                case 2:
                                    department = "HR";
                                    break;
                                case 3:
                                    department = "Admin";
                                    break;
                                default:
                                    Console.WriteLine("Invalid department choice!");
                                    break;
                            }

                            if (!string.IsNullOrEmpty(department))
                            {
                                var deptEmployees = _dal.ViewByDepartment(department);

                                if (deptEmployees.Count>=1)
                                {
                                    foreach (var emp in deptEmployees)
                                    {
                                        Console.WriteLine(emp + "\n");
                                    }
                                }
                                else
                                {
                                    Console.WriteLine("Sorry, no data found in the database!!!");
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"An error occurred: {ex.Message}");
                        }
                        break;

                    case "c":
                        try
                        {
                            Console.Write("Enter id - ");
                            var idInput = Console.ReadLine();
                            if (!int.TryParse(idInput, out int id))
                            {
                                Console.WriteLine("Invalid input! Please enter a valid numeric id.");
                                break;
                            }

                            var empById = _dal.ViewById(id);

                            if (empById != null)
                            {
                                Console.WriteLine(empById);
                            }
                            else
                            {
                                Console.WriteLine($"Sorry, employee with id - {id} not found!!!");
                            }
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"An error occurred: {ex.Message}");
                        }
                        break;

                    default:
                        Console.WriteLine("Invalid choice!");
                        break;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An unexpected error occurred: {ex.Message}");
            }

            Console.WriteLine("\nPress enter to continue...");
            Console.ReadLine();
        }
        public void DisplayAddEmployeeScreen()
        {
            Console.Clear();
            Console.WriteLine("--------- Add New Employee ----------\n");

            Console.Write("First name - ");
            var fName = Console.ReadLine();

            Console.Write("Last name - ");
            var lName = Console.ReadLine();

            Console.Write("1. IT\t2. HR\t3. Admin (please enter number only) - ");
            var deptChoice = int.Parse(Console.ReadLine());

            Console.Write("Salary - ");
            var salary = decimal.Parse(Console.ReadLine());

            DepartmentType deptType = (DepartmentType)deptChoice;

            IEmployee newEmployee = EmployeeFactory.CreateEmployee(deptType);
            newEmployee.FirstName = fName;
            newEmployee.LastName = lName;
            newEmployee.Salary = salary;
            newEmployee.Department = deptType.ToString();
            var retEmp = _dal.AddNewEmployee(newEmployee);
            if (retEmp != null)
            {
                Console.WriteLine("\nEmployee Added Successfully:");
                Console.WriteLine(retEmp);
            }
            else
            {
                Console.WriteLine("Failed to add employee.");
            }

            Console.WriteLine("\nPress enter to continue...");
            Console.ReadLine();
        }
        public void DisplayEditEmployeeScreen()
        {
            Console.Clear();
            Console.WriteLine("--------- Update Employee Record ----------\n");

            Console.Write("Enter id - ");
            var id = int.Parse(Console.ReadLine());
            var employee = _dal.ViewById(id);

            if (employee == null)
            {
                Console.WriteLine($"Employee with id {id} does not exist!");
                Console.WriteLine("Press enter to continue...");
                Console.ReadLine();
                return;
            }

            Console.Write($"First name ({employee.FirstName}) - ");
            var fName = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(fName)) fName = employee.FirstName;

            Console.Write($"Last name ({employee.LastName}) - ");
            var lName = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(lName)) lName = employee.LastName;

            Console.Write($"1. IT\t2. HR\t3. Admin (please enter number only) ({employee.Department}) - ");
            var dChoice = Console.ReadLine();

            DepartmentType deptType;
            if (string.IsNullOrWhiteSpace(dChoice))
            {
                // Keep the same department as current
                deptType = employee.Department switch
                {
                    "IT" => DepartmentType.IT,
                    "HR" => DepartmentType.HR,
                    "Admin" => DepartmentType.Admin,
                    _ => throw new Exception("Invalid department")
                };
            }
            else
            {
                deptType = (DepartmentType)int.Parse(dChoice);
            }

            Console.Write($"Salary ({employee.Salary}) - ");
            var salInput = Console.ReadLine();
            var salary = string.IsNullOrWhiteSpace(salInput) ? employee.Salary : decimal.Parse(salInput);

            IEmployee updatedEmployee = EmployeeFactory.CreateEmployee(deptType);

            updatedEmployee.Id = id;
            updatedEmployee.FirstName = fName;
            updatedEmployee.LastName = lName;
            updatedEmployee.Salary = salary;
            updatedEmployee.Department = deptType.ToString();

            var retEmp = _dal.UpdateEmployee(updatedEmployee);
            if (retEmp != null)
            {
                Console.WriteLine("\nEmployee Updated Successfully:");
                Console.WriteLine(retEmp);
            }
            else
            {
                Console.WriteLine("Failed to update employee.");
            }

            Console.WriteLine("\nPress enter to continue...");
            Console.ReadLine();
        }
        public void DisplayDeleteEmployeeScreen()
        {
            Console.Clear();
            Console.WriteLine("----- Remove Employee ------");
            Console.WriteLine("1. Delete by Id");
            Console.WriteLine("2. Delete by Department");
            Console.Write("\nEnter your choice - ");

            try
            {
                var choice = int.Parse(Console.ReadLine());

                switch (choice)
                {
                    case 1:
                        Console.Write("\nEnter employee id - ");
                        var id = int.Parse(Console.ReadLine());
                        var employee = _dal.DeleteById(id);

                        if (employee != null)
                        {
                            Console.WriteLine($"\n{employee.FirstName} {employee.LastName} removed successfully!");
                        }
                        else
                        {
                            Console.WriteLine($"\nEmployee with id {id} not found.");
                        }
                        break;

                    case 2:
                        Console.Write("\nEnter department name (IT / HR / Admin) - ");
                        var department = Console.ReadLine();

                        var deletedEmployees = _dal.DeleteByDepartment(department);

                        if (deletedEmployees.Count > 0)
                        {
                            Console.WriteLine($"\nDeleted {deletedEmployees.Count} employees from {department} department:");
                            foreach (var emp in deletedEmployees)
                            {
                                Console.WriteLine($"{emp.FirstName} {emp.LastName}");
                            }
                        }
                        else
                        {
                            Console.WriteLine($"\nNo employees found in {department} department.");
                        }
                        break;

                    default:
                        Console.WriteLine("\nInvalid choice. Please select 1 or 2.");
                        break;
                }
            }
            catch (FormatException)
            {
                Console.WriteLine("\nInvalid input! Please enter numbers only.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\nAn error occurred: {ex.Message}");
            }

            Console.WriteLine("\nPress enter to continue...");
            Console.ReadLine();
        }
    }
}
