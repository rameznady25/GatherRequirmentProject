using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gather_Requirement_Project.Models.NotificationClasses
{
    public class children
    {
        public string id { get; set; }

        public string title { get; set; }

        public string type { get; set; }

        public string icon { get; set; }

        public List<subchildren> subchildren { get; set; }

        public children()
        {
            id = " ";
            title = " ";
            type = "collapsable";
            icon = "app";
            subchildren = new List<subchildren>();
        }
    }
}
