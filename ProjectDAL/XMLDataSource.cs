using ProjectModel;
using System.Xml.Linq;

namespace ProjectDAL
{
    public class XMLDataSource
    {
        private readonly string _xmlFilePath;
        //Provide XML file path in constructor as it provides flexibility to pass whichever file
        //user wants to use.
        public XMLDataSource(string xmlFilePath)
        {
            _xmlFilePath = xmlFilePath;
        }

        public List<IEmployee> LoadEmployees()
        {
            List<IEmployee> employeeList = new List<IEmployee>();
            var employees = XDocument.Load(_xmlFilePath).Descendants("Employee");

            foreach (var emp in employees)
            {
                int id = int.Parse(emp.Attribute("Id")?.Value ?? "0");
                string firstName = emp.Element("FirstName")?.Value ?? string.Empty;
                string lastName = emp.Element("LastName")?.Value ?? string.Empty;
                string department = emp.Element("Department")?.Value ?? string.Empty;
                decimal salary = decimal.Parse(emp.Element("Salary")?.Value ?? "0");

                DepartmentType type = EmployeeFactory.GetDepartmentType(department);
                IEmployee employee = EmployeeFactory.CreateEmployee(type);

                employee.Id = id;
                employee.FirstName = firstName;
                employee.LastName = lastName;
                employee.Department = department;
                employee.Salary = salary;

                employeeList.Add(employee);
            }
            return employeeList;
        }

        public void SaveEmployeesToXML(List<IEmployee> employeeList)
        {
            XElement root = new XElement("Employees",
                employeeList.Select(emp =>
                    new XElement("Employee",
                        new XAttribute("Id", emp.Id),
                        new XElement("FirstName", emp.FirstName),
                        new XElement("LastName", emp.LastName),
                        new XElement("Department", emp.Department),
                        new XElement("Salary", emp.Salary)
                    )
                )
            );

            root.Save(_xmlFilePath);
        }
    }
}
