using System.ComponentModel.DataAnnotations;
using Employee_Management_System.Constants;

namespace Employee_Management_System.Models
{
    public class EmployeeViewModel
    {
        [Required]
        [Display(Name = "Task Status")]
        public string TaskStatus { get; set; }

        [Required]
        [Display(Name = "Task Description")]
        public string TaskDescription { get; set; }

        [Required]
        [Display(Name = "Task Id")]
        public string TaskId { get; set; }
    }
}
