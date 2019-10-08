using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Gather_Requirement_Project.Models
{
    public class CustomerProgram
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        public string EnglishNameInSingle { get; set; }
        public string EnglishNameInPlural { get; set; }
        public string ArabicNameInSingle { get; set; }
        public string ArabicNameInPlural { get; set; }
        public string DevName { get; set; }
        public DateTime Date { get; set; }


        //[Url(ErrorMessage = "Please insert a valid URL")]
        public string URLForService { get; set; }

        public ICollection<Module> Modules { get; set; }



        [ForeignKey("Section")]
        public int? SectionID { get; set; }
        public Section Section { get; set; }


    }
}
