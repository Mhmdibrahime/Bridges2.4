using System.ComponentModel.DataAnnotations;

namespace Bridges2._4.Models
{
    public class ProfileViewModel
    {
        public string FName { get; set; }
        public string LName { get; set; }
        public string Email { get; set; }

        [MaxLength(11)]
        public string Phone { get; set; }  

        public List<BookingDetailsViewModel> BookingDetails { get; set; }

        public string CurrentPassword { get; set; }
        public string NewPassword { get; set; }

        [Compare("NewPassword")]
        public string ConfirmNewPassword { get; set;}

        public IFormFile? ImageFile { get; set; }
        public string ImagePath { get; set; }
    }
}
