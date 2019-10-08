using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Gather_Requirement_Project.Models
{
    public class Screen
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        [Required]
        public string EmpName { get; set; }

        //[Required]
        public string DevName { get; set; }

        //[Required]
        public DateTime Date { get; set; }

        [Required]
        public string ScreenName { get; set; }


        [Required]
        public string ScreenNameArabic { get; set; }


        [Required]
        public string EmpJob { get; set; }

        //[Required]
        //public string Criteria { get; set; }

        //[Required]
        //public string PreCondition { get; set; }

        //[Required]
        //public string DoSceniro { get; set; }

        //[Required]
        //public string ExpectedResult { get; set; }

        //[Required]
        //public string ScreenGoal { get; set; }

        
        public string ImagePathPhysical { get; set; }
        public string ImageName { get; set; }

        [NotMapped]
        public IFormFile ImageFile { get; set; }




        [ForeignKey("ScreenTypes")]
        public int screenTypeID { get; set; }
        public virtual ScreenType ScreenTypes { get; set; }


        [ForeignKey("Module")]
        public int ModuleID { get; set; }
        public virtual Module Module { get; set; }


        public virtual ICollection<ScreenComponent> ScreenComponents { get; set; }
        public virtual ICollection<UserStories> UserStories { get; set; }
    }
}
