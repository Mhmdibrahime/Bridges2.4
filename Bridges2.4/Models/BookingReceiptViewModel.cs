namespace Bridges2._4.Models
{
    public class BookingReceiptViewModel
    {
        public int BookingId { get; set; }
        public string Name { get; set; }
        public DateOnly TodayDate { get; set; }
        public decimal Fare { get; set; }
        public int NumberOfSeats { get; set; }
        public string TravelClass { get; set; }
        public decimal TotalPrice { get; set; }
        public decimal LastTotal { get; set; }
        public string Percentage { get; set; }

    }
}
