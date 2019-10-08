using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gather_Requirement_Project.Models.NotificationClasses
{
    public class ModuleFrontEnd
    {
        public string id { get; set; }

        public string title { get; set; }

        public string type { get; set; }

        public string icon { get; set; }

        public List<children> children { get; set; }

        public ModuleFrontEnd()
        {
            children = new List<children>();
            id = " ";
            title = " ";
            type = "group";
            icon = "app";
        }
    }
}
