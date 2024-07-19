using System.ComponentModel.DataAnnotations;

namespace Bridges2._4.Models
{
    public class PaymentViewModel
    {
        public int bookingId { get; set; }
        public string CardNumber { get; set; }
        
        public string EpireDate { get; set; }
        public string CVV { get; set; }

       
    }
}
