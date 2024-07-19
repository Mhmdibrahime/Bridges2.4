using Bridges2._4.Models;
using Bridges2._4.Models.Data;
using Bridges2._4.Models.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace Bridges2._4.Controllers
{
    public class AdminController : Controller
    {
        private readonly AppDbContext context;
        private readonly UserManager<ApplicationUser> userManager;

        public AdminController(AppDbContext context,UserManager<ApplicationUser> userManager)
        {
            this.context = context;
            this.userManager = userManager;
        }
        public IActionResult Dashboard()
        {  
            DashBoardViewModel model = new DashBoardViewModel();
            model.trains= context.train.ToList();
            return View(model);
        }

        [HttpGet]
        public IActionResult AddTrain()
        {

            return View();
        }
        [HttpPost]
        public IActionResult AddTrain(DashBoardViewModel model)
        {
            Train train = new Train
            {
                TrainName = model.TrainName,
                TrainCapacity = model.TrainCapacity,
                TrainNumber = model.TrainNumber
            };
            context.train.Add(train);
            context.SaveChanges();
            return RedirectToAction("Dashboard");
        }

        [HttpGet]
        public IActionResult UpdateTrain(int id)
        {
            var train = context.train.FirstOrDefault(x=>x.TrainID==id);
            DashBoardViewModel model = new DashBoardViewModel
            {
                
                TrainName =train.TrainName,
                TrainNumber=train.TrainNumber,
                TrainCapacity=train.TrainCapacity

            };
            TempData["TrainId"] = train.TrainID;

            return View(model);
        }
        [HttpPost]
        public IActionResult Update(DashBoardViewModel model)
        {
            var train = context.train.FirstOrDefault(x => x.TrainID == (int)TempData["TrainId"]);
            train.TrainName= model.TrainName;
            train.TrainNumber= model.TrainNumber;
            train.TrainCapacity= model.TrainCapacity;
            context.SaveChanges();
            return RedirectToAction("Dashboard");
        }

        public IActionResult DeleteTrain(int id)
        {
            var train = context.train.FirstOrDefault(x => x.TrainID == id);
            context.train.Remove(train);
            context.SaveChanges();
            return RedirectToAction("Dashboard");

        }

        public IActionResult Station()
        { 
            DashBoardViewModel model = new DashBoardViewModel();
            model.Stations=context.stations.ToList();
            return View(model);
        }

        [HttpGet]
        public IActionResult AddStation()
        {

            return View();
        }
        [HttpPost]
        public IActionResult AddStation(DashBoardViewModel model)
        {
            Station station = new Station
            {
                StationName = model.StationName,
                Location = model.Location,
            };
            context.stations.Add(station);
            context.SaveChanges();
            return RedirectToAction("Station");
        }

        [HttpGet]
        public IActionResult UpdateStation(int id)
        {
            var station = context.stations.FirstOrDefault(x => x.StationID == id); 
            TempData["stationId"] = station.StationID;
            DashBoardViewModel model = new DashBoardViewModel();

            model.StationName = station.StationName;
            model.Location = station.Location;
               
            
            
            return View(model);
        }

        [HttpPost]
        public IActionResult UpdateStation(DashBoardViewModel model)
        {
            var station = context.stations.FirstOrDefault(x => x.StationID ==(int)TempData["stationId"]);
            station.StationName = model.StationName;
            station.Location = model.Location;
            context.SaveChanges();
            return RedirectToAction("station");
        }

        public IActionResult DeleteStation(int id)
        {
			var station = context.stations.FirstOrDefault(x => x.StationID == id);
            context.stations.Remove(station);
            context.SaveChanges();
            return RedirectToAction("Station");

		}

        public IActionResult User()
        {
            var users = context.users.ToList();
            return View(users);
        }
        public async Task<IActionResult> UserBooking(string id)
        {
            ApplicationUser user = await userManager.FindByIdAsync(id);
            ProfileViewModel model = new ProfileViewModel();
            model.FName = user.UserName;
            model.LName = user.LName;
            
            model.BookingDetails = context.bookings
         .Where(b => b.UserID == id)
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
        public IActionResult DeleteUser(string id)
        {
            var user = context.users.FirstOrDefault(x => x.Id == id);
            context.users.Remove(user);
            context.SaveChanges();
            return RedirectToAction("User");

        }

        public IActionResult Schedule()
        {
            BookingViewModel viewModel = new BookingViewModel();
            
            viewModel.Schedules = context.schedules
            
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
                Fare = s.Fare
            })
            .ToList();

            return View(viewModel);
        }

        [HttpGet]
        public IActionResult AddSchedule()
        {
            AddScheduleViewModel viewModel = new AddScheduleViewModel();
            viewModel.trains=context.train.ToList();
            viewModel.stations=context.stations.ToList();
            return View(viewModel);
        }

        [HttpPost]
        public IActionResult AddSchedule(AddScheduleViewModel model)
        {
            Schedule schedule = new Schedule();
            schedule.TrainID = model.TrainId;
            schedule.DepartureStationID=model.DepartureStation;
            schedule.ArrivalStationID=model.DestinationStation;
            schedule.DepartureDate= (DateOnly)model.DepartureDate ;
            schedule.DepartureTime = model.DepartureTime;
            schedule.Fare = model.Fare;
            context.schedules.Add(schedule);
            context.SaveChanges();
            return RedirectToAction("Schedule");
        }
        public IActionResult UpdateSchedule(int id)
        {
            TempData["ScheduleId"] = id;
            AddScheduleViewModel viewModel = new AddScheduleViewModel();
            viewModel.trains = context.train.ToList();
            viewModel.stations = context.stations.ToList();
            return View(viewModel);
        }

        [HttpPost]
        public IActionResult UpdateSchedule(AddScheduleViewModel model)
        {
            var schedule = context.schedules.FirstOrDefault(x => x.ScheduleID == (int)TempData["ScheduleId"]);
            schedule.TrainID = model.TrainId;
            schedule.DepartureStationID = model.DepartureStation;
            schedule.ArrivalStationID = model.DestinationStation;
            schedule.DepartureDate = (DateOnly)model.DepartureDate;
            schedule.DepartureTime = model.DepartureTime;
            schedule.Fare = model.Fare;
           
            context.SaveChanges();
            return RedirectToAction("Schedule");
        }
        public IActionResult DeleteSchedule(int id)
        {
            var schedule = context.schedules.FirstOrDefault(x => x.ScheduleID == id);
            context.schedules.Remove(schedule);
            context.SaveChanges();
            return RedirectToAction("Schedule");
        }
        }
    }
