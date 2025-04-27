using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectModel;

namespace ProjectDAL
{
    public interface IDataAccessLayer
    {
        List<IEmployee> ViewAllEmployees();
        List<IEmployee> ViewByDepartment(string department);
        IEmployee ViewById(int id);
        IEmployee AddNewEmployee(IEmployee employee);
        IEmployee UpdateEmployee(IEmployee employee);
        IEmployee DeleteById(int id);
        List<IEmployee> DeleteByDepartment(string department);
    }
}
