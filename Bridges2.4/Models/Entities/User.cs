﻿namespace Bridges2._4.Models.Entities
{
    public class User
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string PhoneNumber { get; set; }
        public string? ImagePath { get; set; }
        public ICollection<Booking> Bookings { get; set; }  
        

    }
}