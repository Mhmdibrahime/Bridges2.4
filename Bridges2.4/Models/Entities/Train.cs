namespace Bridges2._4.Models.Entities
{
    public class Train
    {
        public int TrainID { get; set; }
        public string TrainName { get; set; }
        public string TrainNumber { get; set; }
        public int TrainCapacity { get; set; }
        public ICollection<Schedule> Schedules { get; set; }=new List<Schedule>();
    }
}
