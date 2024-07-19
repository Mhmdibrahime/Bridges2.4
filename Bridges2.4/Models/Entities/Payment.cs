namespace Bridges2._4.Models.Entities
{
    public class Payment
    {
        public int PaymentID { get; set; }
        public int BookingID { get; set; }
        public decimal AmountPaid { get; set; }
        public string PaidStatus { get; set; }

        public string CardNumber { get; set; }
        public string ExpireDate { get; set; }
        public string CVV { get; set; } 

        // Navigation property
        public Booking Booking { get; set; }
        
    }
}
