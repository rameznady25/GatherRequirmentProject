using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Gather_Requirement_Project.Models
{
    public class Portal
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        public string EnglishName { get; set; }
        public string ArabicName { get; set; }


        public ICollection<Section> Sections { get; set; }

        [ForeignKey("MainPortal")]
        public int? MainPortalID { get; set; }
        public MainPortal MainPortal { get; set; }


    }
}
