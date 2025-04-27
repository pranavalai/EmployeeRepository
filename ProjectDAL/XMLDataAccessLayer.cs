using ProjectModel;

namespace ProjectDAL
{
    public class XMLDataAccessLayer : IDataAccessLayer
    {
        private List<IEmployee> employees;
        private XMLDataSource dataSource;
        private static int counter = 100;

        public XMLDataAccessLayer(string xmlFilePath)
        {
            dataSource = new XMLDataSource(xmlFilePath);
            employees = dataSource.LoadEmployees();
            if (employees.Any())
            {
                // Extract the number part of IDs like 101, 102, etc.
                counter = employees.Max(e => e.Id);
            }
            else
            {
                counter = 100; // default start
            }

        }


        public IEmployee AddNewEmployee(IEmployee employee)
        {
            counter++;
            employee.Id = counter;
            employees.Add(employee);
            dataSource.SaveEmployeesToXML(employees); //  Save after adding
            return employee;
        }

        public List<IEmployee> DeleteByDepartment(string department)
        {
            var delByDept = (from e in employees
                             where e.Department.Equals(department, StringComparison.OrdinalIgnoreCase)
                             select e).ToList();

            if (delByDept.Count > 0)
            {
                employees.RemoveAll(emp => emp.Department.Equals(department, StringComparison.OrdinalIgnoreCase));
                dataSource.SaveEmployeesToXML(employees); // Save after deleting
            }

            return delByDept;
        }

        public IEmployee DeleteById(int id)
        {
            var employee = employees.FirstOrDefault(emp => emp.Id == id);
            if (employee != null)
            {
                employees.Remove(employee);
                dataSource.SaveEmployeesToXML(employees); //  Save after deleting
            }
            return employee;
        }

        public IEmployee UpdateEmployee(IEmployee updatedEmployee)
        {
            var existingEmployee = employees.FirstOrDefault(emp => emp.Id == updatedEmployee.Id);
            if (existingEmployee != null)
            {
                existingEmployee.FirstName = updatedEmployee.FirstName;
                existingEmployee.LastName = updatedEmployee.LastName;
                existingEmployee.Department = updatedEmployee.Department;
                existingEmployee.Salary = updatedEmployee.Salary;

                dataSource.SaveEmployeesToXML(employees); // Save after updating
            }
            return existingEmployee;
        }

        public List<IEmployee> ViewAllEmployees()
        {
            return employees;
        }

        public List<IEmployee> ViewByDepartment(string department)
        {
            var viewByDept = from e in employees where e.Department == department select e;
            return viewByDept.ToList();
        }

        public IEmployee ViewById(int id)
        {
            return employees.FirstOrDefault(emp => emp.Id == id);
        }
    }
}
