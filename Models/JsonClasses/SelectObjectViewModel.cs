using System;
using System.Collections.Generic;
using System.Text;

namespace Gather_Requirement_Project.Models.JsonClasses
{
   public class SelectObjectViewModel:BaseViewModel
    {
        public string ServiceName { get; set; }
        public List<DependantViewModel> Dependants { get; set; }
    }
}
