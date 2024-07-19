using Bridges2._4.Models.Data;
using Bridges2._4.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Bridges2._4.Models.Entities;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;

namespace Bridges2._4.Controllers
{
    public class UserController : Controller
    {

        private readonly AppDbContext context;
        private readonly UserManager<ApplicationUser> userManager;

        public UserController(AppDbContext context,UserManager<ApplicationUser> userManager)
        {
            this.context = context;
            this.userManager = userManager;
        }

        public async Task<IActionResult> Profile()
        {

            string userId = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value; 
            //Booking booking = context.bookings.FirstOrDefault(x=>x.UserID==userId);
            //int bookingId = booking.BookingID;
            ApplicationUser user= await userManager.FindByIdAsync(userId);
            var userr = context.users.FirstOrDefault(x=>x.Id == userId);
            ProfileViewModel model = new ProfileViewModel();
            model.FName = user.UserName;
            model.LName=user.LName;
            model.Phone = user.PhoneNumber;
            model.Email = user.Email;
            model.ImagePath = userr.ImagePath;
           model.BookingDetails  = context.bookings
        .Where(b => b.UserID == userId)
        .Select(b => new BookingDetailsViewModel
        {
            BookingId = b.BookingID,
            TrainName = b.Schedule.Train.TrainName,
            DepartureStation = b.Schedule.DepartureStation.StationName,
            ArrivalStation = b.Schedule.ArrivalStation.StationName, // Fetching arrival station name
            DepartureDate = b.Schedule.DepartureDate,
            DepartureTime = b.Schedule.DepartureTime, // Formatting DateTime to only show time
            NumberOfSeats = b.NumberOfSeats,
            SeatPreference = b.SeatPreference,
            TravelClass = b.Class
        }).ToList();
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UploadImage(ProfileViewModel model)
        {
            string id = User.Claims.FirstOrDefault(x=>x.Type==ClaimTypes.NameIdentifier).Value;
            if (model.ImageFile != null && model.ImageFile.Length > 0)
            {
                var fileName = Guid.NewGuid().ToString() + Path.GetExtension(model.ImageFile.FileName);
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "UserImages", fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await model.ImageFile.CopyToAsync(stream);
                }

                // Save file path in ViewModel
                model.ImagePath = "/UserImages/" + fileName;
            }
            var user = context.users.FirstOrDefault(x => x.Id == id);
            user.ImagePath= model.ImagePath;
            context.SaveChanges();
            return RedirectToAction("Profile");
        }



        //public async Task<IActionResult> ChangeInformation(ProfileViewModel model)
        //{
        //    string id = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;
        //    if (string.IsNullOrEmpty(id)) return RedirectToAction("Profile");

        //    ApplicationUser user = await userManager.FindByIdAsync(id);
        //    if (user == null) return RedirectToAction("Profile");

        //    IdentityResult emailResult = IdentityResult.Success; // Placeholder for actual email change logic
        //    IdentityResult phoneResult = IdentityResult.Success; // Placeholder for actual phone change logic

        //    // Assuming you have verified the new email and phone number elsewhere and have the tokens
        //    emailResult = await userManager.ChangeEmailAsync(user, model.NewEmail, model.NewEmail);
        //    phoneResult = await userManager.ChangePhoneNumberAsync(user, model.NewPhone, model.NewPhone);

        //    if (emailResult.Succeeded && phoneResult.Succeeded)
        //    {
        //        user.Email = model.NewEmail; // This should ideally be done inside email verification logic
        //        user.PhoneNumber = model.NewPhone; // Same here for phone number

        //        var setNameResult = await userManager.SetUserNameAsync(user, model.NewFName + " " + model.NewLName);
        //        if (!setNameResult.Succeeded) return View("Error"); // Handle errors appropriately

        //        if (!string.IsNullOrEmpty(model.NewPassword))
        //        {
        //            var changePasswordResult = await userManager.ChangePasswordAsync(user, model.CurrentPassword, model.NewPassword);
        //            if (!changePasswordResult.Succeeded) return View("Error"); // Handle errors appropriately
        //        }

        //        await context.SaveChangesAsync();
        //    }

        //    return RedirectToAction("Profile");
        //}


        public async Task<IActionResult> ChangePassword(ProfileViewModel model)
        { 
            string id = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value;
            ApplicationUser user = await userManager.FindByIdAsync(id);
            if (user != null)
            {
                bool checkpassword = await userManager.CheckPasswordAsync(user,model.CurrentPassword);
                if(checkpassword)
                {
                    await userManager.ChangePasswordAsync(user,model.CurrentPassword,model.NewPassword);
                    User userr = context.users.FirstOrDefault(x => x.Id==id);
                    userr.Password = model.NewPassword;
                    context.SaveChanges();
                    return RedirectToAction("Logout","Account");
                }
            }
            return RedirectToAction("Profile");
        }



        [HttpGet]
        [HttpGet]
        public IActionResult Booking()
        {
            if (User.Identity.IsAuthenticated)
            {
                BookingViewModel model = new BookingViewModel
                {
                    stations = context.stations.ToList(),
                    cities = context.stations.Select(s => s.Location).Distinct().ToList(),
                    Schedules = new List<ScheduleViewModel>()
                };
                return View(model);
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Booking(BookingViewModel model)
        {
            BookingViewModel viewModel = new BookingViewModel();
            TempData["NumberOfSeats"] = model.NumberOfSeats;
            TempData["TravelClass"] = model.TravelClass;
            TempData["SeatPreference"] = model.SeatPreference;
            viewModel.Schedules = context.schedules
                .Where(s => s.DepartureStationID == model.DepartureStation && s.ArrivalStationID == model.DestinationStation && s.DepartureDate == model.DepartureDate)
                .Include(s => s.Train)
                .Include(s => s.DepartureStation)
                .Include(s => s.ArrivalStation)
                .Select(s => new ScheduleViewModel
                {
                    Id = s.ScheduleID,
                    TrainName = s.Train.TrainName,
                    DepartureStation = s.DepartureStation.StationName,
                    ArrivalStation = s.ArrivalStation.StationName,
                    DepartureDate = s.DepartureDate,
                    DepartureTime = s.DepartureTime,
                    Fare = s.Fare,
                })
                .ToList();
            viewModel.stations = context.stations.ToList();
            viewModel.cities = context.stations.Select(s => s.Location).Distinct().ToList();

            return View(viewModel);
        }



        public IActionResult Book(int id)
        {
            
            Booking booking = new Booking();
            TempData["BookingId"] = booking.BookingID;
            booking.UserID = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value;
            booking.ScheduleID= id;
            booking.NumberOfSeats = (int)TempData["NumberOfSeats"];
            booking.SeatPreference = (string)TempData["SeatPreference"];
            booking.Class = (string)TempData["TravelClass"];
            context.bookings.Add(booking);
            
            context.SaveChanges();
            return RedirectToAction("BookingReceipt", new {id = booking.BookingID});
        }

        public async Task<IActionResult> BookingReceipt(int id)
        {
            var userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value;
            var user = await userManager.FindByIdAsync(userId);
            var booking = context.bookings
        .Include(b => b.Schedule)
            .Include(b => b.User) // Assuming you want user details as well
        .FirstOrDefault(b => b.BookingID == id );


            // Pricing logic (simplified example)
            var percentage = "";
            var totalPrice = booking.NumberOfSeats * booking.Schedule.Fare;
            totalPrice = booking.NumberOfSeats*booking.Schedule.Fare;
            var lastTotal = totalPrice;
            if (booking.Class == "First class")
            {
                lastTotal *= 1.3M; // 50% more expensive
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
        [HttpGet]
        public IActionResult Payment(int id)
        {
            PaymentViewModel model = new PaymentViewModel()
            {
                bookingId = id
            };
            return View(model);
        }

        [HttpPost]
        public IActionResult payment(PaymentViewModel model) 
        {
            Payment payment = new Payment()
            {
                CardNumber= model.CardNumber,
                ExpireDate= model.EpireDate,
                CVV=model.CVV,
                BookingID=model.bookingId
            };
            context.payment.Add(payment);
            context.SaveChanges();
            return RedirectToAction("Ticket", new {id=model.bookingId});
        }
        [HttpGet]
        public IActionResult Ticket(int id)
        {
            var booking = context.bookings
       .Include(b => b.Schedule)
           .ThenInclude(s => s.DepartureStation)
       .Include(b => b.Schedule)
           .ThenInclude(s => s.ArrivalStation)
       .Include(b => b.Schedule)
           .ThenInclude(s => s.Train)
       .FirstOrDefault(b => b.BookingID == id);

            var viewModel = new TicketViewModel
            {
                BookingId = booking.BookingID,
                DepartureStation = booking.Schedule.DepartureStation.StationName,
                ArrivalStation = booking.Schedule.ArrivalStation.StationName,
                DepartureTime = booking.Schedule.DepartureDate,
                TrainName = booking.Schedule.Train.TrainName,
                TravelClass = booking.Class,
                NumberOfSeats = booking.NumberOfSeats
            };

            return View(viewModel);
        }
    }
}

    


