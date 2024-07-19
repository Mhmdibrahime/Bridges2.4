using Bridges2._4.Models.Entities;

namespace Bridges2._4.Models
{
    public class AddScheduleViewModel
    {
        public List<Train> trains {  get; set; }
        public List<Station> stations {  get; set; }

        public int TrainId { get; set; }
        public int DepartureStation { get; set; }
        public int DestinationStation { get; set; }
        public DateOnly? DepartureDate { get; set; }

        public string DepartureTime { get; set; }
        public decimal Fare { get; set; }
    }
}
