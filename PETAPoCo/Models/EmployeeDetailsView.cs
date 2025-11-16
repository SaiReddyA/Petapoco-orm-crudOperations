namespace PETAPoCo.ApplicationSR.Models
{
    [TableName("vw_EmployeeDetails")]
    [ExplicitColumns]
    public class EmployeeDetailsView
    {
        [Column("EmployeeId")] public int EmployeeId { get; set; }
        [Column("FullName")] public string FullName { get; set; }
        [Column("Email")] public string Email { get; set; }
        [Column("Salary")] public decimal Salary { get; set; }
        [Column("DepartmentName")] public string DepartmentName { get; set; }
        [Column("Location")] public string Location { get; set; }
    }

}
