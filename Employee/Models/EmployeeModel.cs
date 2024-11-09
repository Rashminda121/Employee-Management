namespace Employee.Models
{
    public class EmployeeModel
    {
        public int EmployeeId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public string Email { get; set; }

        public string Dob { get; set; }

        public int Age { get; set; }

        public float Salary { get; set;}

        public string Department { get; set;}
    }
}
