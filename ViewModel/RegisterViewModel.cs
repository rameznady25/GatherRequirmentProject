using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Gather_Requirement_Project.ViewModel
{
    public class RegisterViewModel
    {
        [Required]
        [Display(Name = "Developer Name")]
        [Remote(action: "IsDeveloperFound", controller: "Account")]
        public string DeveloperName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm Password")]
        [Compare("Password", ErrorMessage = "Password and Confirm Password don't match")]
        public string ConfirmPassword { get; set; }

    }
}
