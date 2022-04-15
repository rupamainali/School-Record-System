using System.ComponentModel.DataAnnotations;

namespace SchoolRecordSystem.Models
{
    public class RegistrationDetailsModel
    {
        public string Id { get; set; }
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        [Compare("Password")]
        public string ConfirmPassword { get; set; }
        public string CreatedBy { get; set; }
    }
}
