using Gather_Requirement_Project.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gather_Requirement_Project.ViewModels
{
    public class ScreenComponentValidationVM
    {
        public ScreenComponent ScreenComponent { get; set; }

        public CheckBoxListItem[] Validations { get; set; }
    }
}
