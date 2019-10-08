using System;
using System.Collections.Generic;
using System.Text;

namespace Gather_Requirement_Project.Models.JsonClasses
{
   public class JsonFileViewModel
    {
        public List<InputObjectViewModel> InputObjects { get; set; }
        public List<SelectObjectViewModel> SelectObjects { get; set; }
        public List<ValidationViewModel> CustomValidators { get; set; }
        public string PageTitle { get; set; }
        public string PageTitles { get; set; }
        public string ComponentName { get; set; }
        public string ComponentPlural { get; set; }
        public string Url { get; set; }
    }
}
