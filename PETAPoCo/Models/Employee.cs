namespace PETAPoCo.ApplicationSR.Models
{
    [TableName("Employee")]
    [PrimaryKey("EmployeeId", AutoIncrement = true)]
    public class Employee
    {
        public int EmployeeId { get; set; }
        public int DepartmentId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public decimal Salary { get; set; }
    }

}
