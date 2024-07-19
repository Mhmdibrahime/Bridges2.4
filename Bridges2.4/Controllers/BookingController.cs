using Bridges2._4.Models;
using Bridges2._4.Models.Data;
using Bridges2._4.Models.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace Bridges2._4.Controllers
{
    public class BookingController : Controller
    {
        private readonly AppDbContext context;
        private readonly UserManager<ApplicationUser> userManager;

        public BookingController(AppDbContext context,UserManager<ApplicationUser> userManager)
        {
            this.context = context;
            this.userManager = userManager;
        }
        public IActionResult Booking()
        {
            BookingViewModel model=new BookingViewModel();
            model.stations = context.stations.ToList();
            return View(model);
        }

        public async Task<IActionResult> BookingReceipt(int id)
        {
            var userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value;
            var user = await userManager.FindByIdAsync(userId); 
            var booking = context.bookings
        .Include(b => b.Schedule)
            .Include(b => b.User) // Assuming you want user details as well
        .FirstOrDefault(b => b.BookingID == id);


            // Pricing logic (simplified example)
            var percentage = "";
            var totalPrice = booking.NumberOfSeats * booking.Schedule.Fare;
            totalPrice = booking.NumberOfSeats * booking.Schedule.Fare;
            var lastTotal = totalPrice;
            if (booking.Class == "First class")
            {
                lastTotal *= 1.3M; 
                percentage = "30%";
            }
            else if (booking.Class == "Economy class")
            {
                lastTotal *= 1M;
                percentage = "0%";
            }
            else
            {
                lastTotal *= 1.5M;
                percentage = "50%";
            }

            BookingReceiptViewModel model = new BookingReceiptViewModel()
            {
                Name = user.UserName + " " + user.LName,
                Fare = booking.Schedule.Fare,
                NumberOfSeats = booking.NumberOfSeats,
                TravelClass = booking.Class,
                TodayDate = DateOnly.FromDateTime(DateTime.Today),
                TotalPrice = totalPrice,
                BookingId = booking.BookingID,
            LastTotal = lastTotal,
            Percentage = percentage

        };

            return View(model);
        }
    }
}
