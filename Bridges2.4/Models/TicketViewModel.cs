namespace Bridges2._4.Models
{
    public class TicketViewModel
    {
        public int BookingId { get; set; }
        public string DepartureStation { get; set; }
        public string ArrivalStation { get; set; }
        public DateOnly DepartureTime { get; set; }
        public string TrainName { get; set; }
        public string TravelClass { get; set; }
        public int NumberOfSeats { get; set; }
    }

}
