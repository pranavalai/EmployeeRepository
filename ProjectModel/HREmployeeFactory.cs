using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectModel
{
    public class HREmployeeFactory : IEmployeeFactory
    {
        public IEmployee CreateEmployee()
        {
            return new HREmployee();
        }
    }
}
