using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gather_Requirement_Project.Models.NotificationClasses
{
    public class HeadFile
    {
       public List<ModuleFrontEnd> program { get; set; }

        public HeadFile()
        {
            program = new List<ModuleFrontEnd>();
        }
    }
}
