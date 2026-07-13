using System.ComponentModel.DataAnnotations;

namespace EmployeeManagementSystem.Models
{
    public class Employee
    {
        public int EmployeeId { get; set; }

        [Required]
        public string Name { get; set; } = "";

        [Required]
        public string Email { get; set; } = "";

        public string Phone { get; set; } = "";

        public string Department { get; set; } = "";

        public decimal Salary { get; set; }
    }
}