using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Gather_Requirement_Project.Models
{
    public class Section
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        public string EnglishName { get; set; }
        public string ArabicName { get; set; }


        public ICollection<CustomerProgram> CustomerPrograms { get; set; }

        [ForeignKey("Portal")]
        public int? PortalID { get; set; }
        public Portal Portal { get; set; }


    }
}
