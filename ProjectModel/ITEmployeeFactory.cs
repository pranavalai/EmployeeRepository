using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectModel
{
    public class ITEmployeeFactory : IEmployeeFactory
    {
        public IEmployee CreateEmployee()
        {
            return new ITEmployee();
        }
    }
}
