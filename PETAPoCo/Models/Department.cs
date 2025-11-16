namespace PETAPoCo.ApplicationSR.Models
{
    [TableName("Department")]
    [PrimaryKey("DepartmentId", AutoIncrement = true)]
    public class Department
    {
        public int DepartmentId { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }
    }
    public class DepartmentEmployeeRequest
    {
        public Department Department { get; set; }
        public Employee Employee { get; set; }
    }

}
