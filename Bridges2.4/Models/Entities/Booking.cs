namespace Bridges2._4.Models.Entities
{
    public class Booking
    {
        public int BookingID { get; set; }
        public string UserID { get; set; }
        public int ScheduleID { get; set; }
        public int NumberOfSeats { get; set; }
        public string SeatPreference { get; set; }
        public string Class { get; set; }

        // Navigation properties
        public User User { get; set; }
        public Schedule Schedule { get; set; }
        public Payment Payment { get; set; }
    }
}
