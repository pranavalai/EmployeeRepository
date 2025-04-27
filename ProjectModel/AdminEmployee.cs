using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectModel
{
    public class AdminEmployee : IEmployee
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Department { get; set; }
        public decimal Salary { get; set; }
        public override string ToString()
        {
            return ($"Id : {Id}\tName : {FirstName + " "+ LastName}\tDepartment : {Department}\tSalary : {Salary}");
        }
    }
}
