namespace ProjectModel
{
    /// <summary>
    /// Employee factory class is declared static as we dont want to create an object of it rather than 
    /// just call the createEmployee method.
    /// </summary>
    public static class EmployeeFactory
    {
        public static IEmployee CreateEmployee(DepartmentType department)
        {
            IEmployeeFactory factory;

            // Using Abstract Factory pattern to create a specific department factory
            switch (department)
            {
                case DepartmentType.IT:
                    factory = new ITEmployeeFactory();
                    break;
                case DepartmentType.Admin:
                    factory = new AdminEmployeeFactory();
                    break;
                case DepartmentType.HR:
                    factory = new HREmployeeFactory();
                    break;
                default:
                    throw new ArgumentException("Invalid department type");
            }

            return factory.CreateEmployee();  // Use the factory to create the employee
        }

        /// <summary>
        ///  This method takes a string input like "IT" or "HR" and returns a DepartmentType enum. Inside, it uses Enum.
        ///  TryParse(department, true, out DepartmentType deptType) to try converting the string to the 
        ///  corresponding enum value. The true flag makes the parsing case-insensitive (e.g., "IT", "it", "It" all work).
        ///  If parsing succeeds, it returns the deptType to the caller, successfully mapping the string to the correct enum.
        /// </summary>
        /// <param name="department"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public static DepartmentType GetDepartmentType(string department)
        {
            if (Enum.TryParse(department, true, out DepartmentType deptType))
            {
                return deptType;
            }
            else
            {
                throw new ArgumentException("Unknown department: " + department);
            }
        }
    }
}
