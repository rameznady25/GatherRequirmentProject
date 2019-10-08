using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Gather_Requirement_Project.Models
{
    public class ScreenComponent
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        [RegularExpression(@"^[A-Z][a-zA-Z0-9_]*", ErrorMessage = "Characters are not allowed.")]
        public string FieldNameEnglish { get; set; }
        public string FielNameArabic { get; set; }
        public Boolean Visible { get; set; }
        public Boolean Readonly { get; set; }
        public string DefaultValue { get; set; }
        public string ServiceNameForDropdown { get; set; }
        public string Values { get; set; }
        
        //------------------------ Validations -----------------------------------------
        public Boolean ValidationRequired  { get; set; }
        public string ValidationRequiredMessage { get; set; }
        public int? ValidationMax { get; set; }
        public string ValidationMaxMessage { get; set; }
        public int? ValidationMin { get; set; }
        public string ValidationMinMessage { get; set; }
        public int? ValidationEqualID { get; set; }
        public string ValidationEqualMessage { get; set; }
        public int? ValidationLessthanID { get; set; }
        public string ValidationLessthanMessage { get; set; }
        public int? ValidationGreaterthanID { get; set; }
        public string ValidationGreaterthanMessage { get; set; }



        public int? ScreenComponentID { get; set; }
        //[ForeignKey("ScreenComponentID")]
        //public virtual ScreenComponent ScreenComponentDepend { get; set; }




        //public virtual ICollection<ScreencomValidations> GetScreencomValidations { get; set; }
        //public IList<ScreencomValidations> ScreencomValidations { get; set; }

        [ForeignKey("FieldType")]
        public int FieldTypeID { get; set; }
        public virtual FieldType FieldType { get; set; }

        [ForeignKey("InputType")]
        public int InputtypeID { get; set; }
        public virtual InputType InputType { get; set; }

        [ForeignKey("Screen")]
        public int ScreenID { get; set; }
        public virtual Screen Screen { get; set; }
    }
}
