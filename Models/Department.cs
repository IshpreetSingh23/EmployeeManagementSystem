using System.ComponentModel.DataAnnotations;

namespace EmployeeManagementSystem.Models
{
    public class Department
    {
        public int DepartmentId { get; set; }

        [Required]
        public string DepartmentName { get; set; } = "";

        [Required]
        public string Location { get; set; } = "";
    }
}