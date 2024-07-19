using Bridges2._4.Models.Entities;

namespace Bridges2._4.Models
{
    public class BookingViewModel
    {
        public int DepartureStation { get; set; }
        public int DestinationStation { get; set; }
        public List<string> cities { get; set; }
        public DateOnly? DepartureDate { get; set; }  

        public int NumberOfSeats { get; set; }
        public string SeatPreference { get; set; }
        public string TravelClass { get; set; }
         public List<ScheduleViewModel> Schedules { get; set; } 
        public List<Station> stations { get; set; }
    }
}
