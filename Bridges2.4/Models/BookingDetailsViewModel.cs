public class BookingDetailsViewModel
{
    public int BookingId { get; set; }
    public string TrainName { get; set; }
    public string DepartureStation { get; set; }
    public string ArrivalStation { get; set; }  // Added for arrival station
    public DateOnly DepartureDate { get; set; }
    public string DepartureTime { get; set; } // Assuming DepartureTime is part of DateTime
    public int NumberOfSeats { get; set; }
    public string SeatPreference { get; set; }
    public string TravelClass { get; set; }
}