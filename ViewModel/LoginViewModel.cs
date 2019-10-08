using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Gather_Requirement_Project.ViewModel
{
    public class LoginViewModel
    {
        [Required]
        [Display(Name = "Developer Name")]
        public string DeveloperName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "Remeber Me")]
        public bool RememberMe { get; set; }
    }
}
