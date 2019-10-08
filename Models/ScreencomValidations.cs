using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Gather_Requirement_Project.Models
{
    public class ScreencomValidations
    {

        [ForeignKey("FieldValidation")]
        public int FieldValidationID { get; set; }
        public FieldValidation FieldValidation { get; set; }


        [ForeignKey("ScreenComponent")]
        public int ScreenComponentID { get; set; }
        public ScreenComponent ScreenComponent  { get; set; }

    }
}
