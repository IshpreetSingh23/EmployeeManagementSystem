using System.ComponentModel.DataAnnotations;

namespace EmployeeManagementSystem.Models
{
    public class Attendance
    {
        public int AttendanceId { get; set; }

        [Required]
        public int EmployeeId { get; set; }

        [Required]
        public string EmployeeName { get; set; } = "";

        [Required]
        public DateTime AttendanceDate { get; set; }

        [Required]
        public string Status { get; set; } = "";
    }
}