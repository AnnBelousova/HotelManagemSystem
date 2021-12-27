using HotelManagemSystem.Data;
using HotelManagemSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace HotelManagemSystem.Controllers
{
    public class HomeController : Controller
    {

        private HotelContext context { get; set; }

        public HomeController(HotelContext ctx)
        {
            context = ctx;
        }

        public IActionResult Index()
        {
            var rooms = context.Rooms
                .Include(r => r.RoomType)
                .OrderBy(r => r.RoomNo)
                .ToList();
            return View(rooms);
        }

        [Route("[action]")]
        public IActionResult Contacts()
        {
            return View();
        }


        [Route("[action]")]
        public IActionResult About()
        {
            return View();
        }

    }
}
