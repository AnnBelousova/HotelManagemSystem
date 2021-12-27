using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using HotelManagemSystem.Data;
using HotelManagemSystem.Models;
using Microsoft.AspNetCore.Authorization;

namespace HotelManagemSystem.Controllers
{
/*    [Authorize]*/
    public class RoomsController : Controller
    {
        private readonly HotelContext _context;

        public RoomsController(HotelContext context)
        {
            _context = context;
        }

        // GET: Rooms

        public async Task<IActionResult> Index()
        {
            var hotelContext = _context.Rooms.Include(r => r.RoomType).Where(r => r.IsAvailable == 0);
            return View(await hotelContext.ToListAsync());
        }

        // GET: Rooms/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            var roomId = _context.Rooms.Find(id);
      
            string imageFilename = roomId.RoomTypeId + "_m.jpg";

            ViewBag.ImageFilename = imageFilename;
            if (id == null)
            {
                return NotFound();
            }

            var room = await _context.Rooms
                .Include(r => r.RoomType)
                .FirstOrDefaultAsync(m => m.RoomId == id);
            if (room == null)
            {
                return NotFound();
            }

            return View(room);
        }

        // GET: Rooms/Create
        [HttpGet]
        public IActionResult Create()
        {
            RoomType roomType = new RoomType();
            ViewBag.Action = "Create";
            ViewBag.RoomTypes = _context.RoomTypes.OrderBy(rt => rt.RoomTypeId).ToList();
            return View("Edit", new Room());
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            ViewBag.Action = "Edit";
            ViewBag.RoomTypes = _context.RoomTypes.OrderBy(rt => rt.RoomTypeId).ToList();
            var room = _context.Rooms.Find(id);
            return View(room);
        }

        [HttpPost]
        public IActionResult Edit(Room room)
        {
            if (ModelState.IsValid)
            {
                if (room.RoomId == 0)
                    _context.Rooms.Add(room);
                else
                    _context.Rooms.Update(room);
                _context.SaveChanges();
                return RedirectToAction("Index", "Rooms");
            }
            else
            {
                ViewBag.Action = (room.RoomId == 0) ? "Add" : "Edit";
                ViewBag.Rooms = _context.RoomTypes.OrderBy(rt => rt.RoomTypeId).ToList();
                return View(room);
            }
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var room = await _context.Rooms
                .Include(r => r.RoomType)
                .FirstOrDefaultAsync(m => m.RoomId == id);
            if (room == null)
            {
                return NotFound();
            }

            return View(room);
        }

        // POST: Rooms/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var room = await _context.Rooms.FindAsync(id);
            _context.Rooms.Remove(room);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult ReserveRoom(int id)
        {
            
            return RedirectToAction("Create", "Reservations", new { roomid = id});
        }

        private bool RoomExists(int id)
        {
            return _context.Rooms.Any(e => e.RoomId == id);
        }
    }
}
