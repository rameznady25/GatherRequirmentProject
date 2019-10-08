using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Gather_Requirement_Project.Models
{
    public class Module
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        public string EnglishNameInSingle { get; set; }
        public string EnglishNameInPlural { get; set; }
        public string ArabicNameInSingle { get; set; }
        public string ArabicNameInPlural { get; set; }
        //[Url(ErrorMessage = "Please insert a valid URL")]
        public string URLForService { get; set; }


        [ForeignKey("CustomerProgram")]
        public int CustomerProgramID { get; set; }
        public CustomerProgram CustomerProgram { get; set; }

        public virtual ICollection<Screen> Screens { get; set; }

    }
}
