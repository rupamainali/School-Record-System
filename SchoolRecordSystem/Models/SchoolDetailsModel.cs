using System.ComponentModel.DataAnnotations;
using SchoolRecordSystem.Extensions;

namespace SchoolRecordSystem.Models
{
    public class SchoolDetailsModel
    {

        /*
            Required- This validation attributes makes any property as required or mandatory.
            StringLength- This validation attributes validate any string property along with itslength
            Compare -This validation attribute basically compares the two property values in a model match
            EmailAddress- This validation attribute validates the email address format
            Phone- This validation attribute validates the phone no format
            CreditCard- This attribute validates a credit card format
            Range -This validation attributes any property to check it is exist within the given range or not.
            Url- This validation attributes validates the property contains an URL format or not
            RegularExpression- This validation attributes normally match the data which specified the regular expression format.
            Remote -Validates input on the client by calling an action method on the server.
        */

        public string SchoolId { get; set; }
        [Required(ErrorMessage = "Please enter the school name")]
        public string SchoolName { get; set; }
        [Required]
        public string SchoolAddress { get; set; }
        [Required]
        public string SchoolRegistrationNumber { get; set; }
        [Required]
        public string SchoolPrincipal { get; set; }
        [Required]
        [ValidateNumber]
        // [ValidateNpPhoneNumber]
        public string NumberOfTeachers { get; set; }
    }
}
