using Bridges2._4.Models.Entities;

namespace Bridges2._4.Models
{
    public class DashBoardViewModel
    {
        public List<Train>  trains { get; set; } = new List<Train>();
        public int TrainId { get; set; }
        public string TrainName { get; set; }
        public string TrainNumber { get; set; }
        public int TrainCapacity { get; set; }

        public List<Station> Stations { get; set; } = new List<Station>();
        public int StationId { get; set; }
        public string StationName { get; set; }
        public string Location { get; set; }

        

    }
}
