using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Gather_Requirement_Project.Models
{
    public class UserStories
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        [Display(Name = "المتطلبات")]
        public string Requirement { get; set; }
        public string UserStory { get; set; }


        [ForeignKey("Screen")]
        public int ScreenID { get; set; }
        public virtual Screen Screen { get; set; }

    }
}
