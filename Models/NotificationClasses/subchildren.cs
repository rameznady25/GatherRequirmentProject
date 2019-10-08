using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gather_Requirement_Project.Models.NotificationClasses { 

    public class subchildren
    {
        public string id { get; set; }

        public string title { get; set; }

        public string type { get; set; }

        public string url { get; set; }

        public bool exactMatch { get; set; } = true;

        public subchildren()
        {
            id = " ";
            title = " ";
            type = "item";
            url = " ";
            exactMatch = true;
        }
    }
}
