using System;
using System.Collections.Generic;
using System.Text;

namespace Gather_Requirement_Project.Models.JsonClasses
{
   public class BaseViewModel
    {
        public string Label { get; set; }
        public string Field { get; set; }
        public string Default { get; set; }
        public bool ReadOnly { get; set; }
        public string Dependant { get; set; }
        public bool Visible { get; set; }

        public List<ValidationViewModel> Validators { get; set; }



        //public bool ShowInList { get; set; }
        //public bool ShowInSearch { get; set; }
        //public bool ShowInCreate { get; set; }
        //public bool ShowInEdit { get; set; }  
        //public bool ShowInView { get; set; }
    }
}
