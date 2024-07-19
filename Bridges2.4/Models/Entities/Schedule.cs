namespace Bridges2._4.Models.Entities
{
    public class Schedule
    {
        public int ScheduleID { get; set; }
        public int TrainID { get; set; }
        public int DepartureStationID { get; set; }
        public int ArrivalStationID { get; set; }
        public DateOnly DepartureDate { get; set; }
        public string DepartureTime { get; set; }
        public decimal Fare { get; set; }

        // Navigation properties
        public Train Train { get; set; }
        public Station DepartureStation { get; set; }
        public Station ArrivalStation { get; set; }
        public ICollection<Booking> Bookings { get; set; }
    }
}
