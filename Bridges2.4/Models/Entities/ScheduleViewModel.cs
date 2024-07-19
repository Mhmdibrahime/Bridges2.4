namespace Bridges2._4.Models.Entities
{
    public class ScheduleViewModel
    {
        public int Id { get; set; }
        public string TrainName { get; set; }
        public string DepartureStation{ get; set; }
        public string ArrivalStation { get; set; }
        public DateOnly DepartureDate { get; set; }
        public string DepartureTime { get; set; }
        public decimal Fare {  get; set; }
        
    }
}
