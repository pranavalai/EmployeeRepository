namespace ProjectModel
{
    public class ITEmployee : IEmployee
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Department { get; set; } = string.Empty;
        public decimal Salary { get; set; }
        public override string ToString()
        {
            return ($"Id : {Id}\tName : {FirstName +" " + LastName}\tDepartment : {Department}\tSalary : {Salary}");
        }
    }
}
