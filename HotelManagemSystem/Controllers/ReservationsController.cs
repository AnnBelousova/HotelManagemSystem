using HotelManagemSystem.Data;
using HotelManagemSystem.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelManagemSystem.Controllers
{
    public class ReservationsController : Controller
    {
        HotelContext _context { get; set; }

        public ReservationsController(HotelContext context)
        {
            _context = context;
        }
        // GET: ReservationsController
        public ActionResult Index()
        {

            /*ViewBag.RoomType= _context.RoomTypes.OrderBy(rt => rt.Name).ToList();*/

            return View(_context.Reservation.Include(r => r.Room)
                .Select<Reservation, ReservationViewModel>(reservation => new ReservationViewModel()
                {
                    Id = reservation.Id,
                    Name = reservation.Name,
                    Email = reservation.Email,
                    Room = reservation.Room,
                    ArrivalDate = reservation.ArrivalDate,
                    DepartureDate = reservation.DepartureDate,
                    NumberOfGuests = reservation.NumberOfGuests
                }).ToList());
        }

        // GET: ReservationsController/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reservation = await _context.Reservation.Include(r => r.Room).Include(r => r.Room.RoomType)
                .FirstOrDefaultAsync(r => r.Id == id);
            if (reservation == null)
            {
                return NotFound();
            }

            return View(new ReservationViewModel()
            {
                Id = reservation.Id,
                Name = reservation.Name,
                Email = reservation.Email,
                Room = reservation.Room,
                ArrivalDate = reservation.ArrivalDate,
                DepartureDate = reservation.DepartureDate,
                NumberOfGuests = reservation.NumberOfGuests
            });
        }

        // GET: ReservationsController/Create
        public ActionResult Create(int? roomid)
        {
            ViewBag.RoomType = _context.RoomTypes.Include(rt => rt.Rooms)
                .Where(rt => rt.Rooms.Count != 0 && rt.Rooms.Where(r => r.IsAvailable == 0).Count() != 0)
                .OrderBy(rt => rt.RoomTypeId).ToList();

            List<SelectListItem> guests = new List<SelectListItem>();
            guests.Add(new SelectListItem() { Text = "Persons", Value = "0" });
            guests.Add(new SelectListItem() { Text = "1 Persons", Value = "1" });
            guests.Add(new SelectListItem() { Text = "2 Persons", Value = "2" });
            guests.Add(new SelectListItem() { Text = "3 Persons", Value = "3" });
            guests.Add(new SelectListItem() { Text = "4 Persons", Value = "5" });
            guests.Add(new SelectListItem() { Text = "More", Value = "6" });
            ViewBag.Guests = guests;

            return View(new ReservationViewModel());
        }

        // POST: ReservationsController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(ReservationViewModel model)
        {
            if (ModelState.IsValid)
            {
                Room room = null;
                if (model.RoomId != 0)
                {
                    room = _context.Rooms.Find(model.RoomId);
                }
                else
                {
                    room = _context.Rooms.Where(r => r.RoomTypeId == model.RoomTypeId && r.IsAvailable == 0).OrderBy(o => o.RoomNo).FirstOrDefault();
                }

                if (room == null)
                {
                    return View("");
                }

                Reservation obj = new Reservation()
                {
                    Name = model.Name,
                    Email = model.Email,
                    ArrivalDateTime = model.ArrivalDateTime,
                    DepartureDateTime = model.DepartureDateTime,
                    NumberOfGuests = model.NumberOfGuests,
                    RoomId = room.RoomId,
                    Price = model.Price
                };
                _context.Reservation.Add(obj);
                room.IsAvailable = 1;
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            List<SelectListItem> guests = new List<SelectListItem>();
            guests.Add(new SelectListItem() { Text = "Persons", Value = "0" });
            guests.Add(new SelectListItem() { Text = "1 Persons", Value = "1" });
            guests.Add(new SelectListItem() { Text = "2 Persons", Value = "2" });
            guests.Add(new SelectListItem() { Text = "3 Persons", Value = "3" });
            guests.Add(new SelectListItem() { Text = "5 4 Persons", Value = "5" });
            guests.Add(new SelectListItem() { Text = "More", Value = "6" });
            ViewBag.Guests = guests;
            return View(model);
        }

        // GET: ReservationsController/Edit/5
        public ActionResult Edit(int id)
        {
            /*List<SelectListItem> roomtype = _context.RoomTypes.Select<RoomType,SelectListItem>(type => new SelectListItem() { Text=type.Name, Value=type.Name} ).ToList();*/
            ViewBag.Action = "Edit";
            ViewBag.RoomType = _context.RoomTypes.OrderBy(rt => rt.RoomTypeId).ToList();
            /*ViewBag.RoomType = roomtype;*/
            List<SelectListItem> guests = new List<SelectListItem>();
            guests.Add(new SelectListItem() { Text = "Persons", Value = "0" });
            guests.Add(new SelectListItem() { Text = "1 Persons", Value = "1" });
            guests.Add(new SelectListItem() { Text = "2 Persons", Value = "2" });
            guests.Add(new SelectListItem() { Text = "3 Persons", Value = "3" });
            guests.Add(new SelectListItem() { Text = "5 4 Persons", Value = "5" });
            guests.Add(new SelectListItem() { Text = "More", Value = "6" });
            ViewBag.Guests = guests;

            ViewBag.Action = "Index";
            var model = _context.Reservation.Find(id);
            ReservationViewModel reservationView = new ReservationViewModel()
            {
                Id = model.Id,
                Name = model.Name,
                Email = model.Email,
                ArrivalDate = model.ArrivalDate,
                DepartureDate = model.DepartureDate,
                NumberOfGuests = model.NumberOfGuests
            };
            return View(reservationView);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ReservationViewModel model)
        {
            if (ModelState.IsValid)
            {
                Reservation reservation = _context.Reservation.Find(model.Id);
                Room reservedRoom = _context.Rooms.Find(reservation.RoomId);
                reservedRoom.IsAvailable = 0;
                _context.SaveChanges();
                Room room = _context.Rooms.Where(r => r.RoomTypeId == model.RoomTypeId && r.IsAvailable == 0).OrderBy(o => o.RoomNo).FirstOrDefault();
                if (room == null)
                {
                    return View("");
                }
                reservation.Name = model.Name;
                reservation.Email = model.Email;
                reservation.ArrivalDateTime = model.ArrivalDateTime;
                reservation.DepartureDateTime = model.DepartureDateTime;
                reservation.NumberOfGuests = model.NumberOfGuests;
                reservation.RoomId = room.RoomId;

                room.IsAvailable = 1;

                if (model.Id == 0)
                    _context.Reservation.Add(reservation);
                else
                    _context.Reservation.Update(reservation);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                ViewBag.Action = (model.Id == 0) ? "Create" : "Edit";
                return View(model);
            }
        }

        public async Task<IActionResult> Delete(int? id)
        {

            if (id == null)
            {
                return NotFound();
            }

            var reservation = await _context.Reservation
                .FirstOrDefaultAsync(m => m.Id == id);
            if (reservation == null)
            {
                return NotFound();
            }

            return View(reservation);
        }

        // POST: ReservationsController/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var reservation = await _context.Reservation.FindAsync(id);
            _context.Reservation.Remove(reservation);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RoomExists(int id)
        {
            return _context.Reservation.Any(e => e.Id == id);
        }
    }
}
