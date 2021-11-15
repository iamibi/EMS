using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Employee_Management_System.Models
{
    public class ManagerViewModel
    {
        [Required]
        [Display(Name = "Employees")]
        public List<EMSUser> Employees { get; set; }

        [Required]
        [Display(Name = "Tasks")]
        public List<EMSTask> EmployeeTasks { get; set; }

        [Required]
        [Display(Name = "New Employee Request")]
        public EMSUser NewEmployeeRequest { get; set; }
    }
}
