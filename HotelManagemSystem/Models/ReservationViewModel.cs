using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace HotelManagemSystem.Models
{
    public class ReservationViewModel
    {
        public int Id { get; set; }
        [StringLength(100)]

        public string Name { get; set; }
        [StringLength(100)]

        public string Email { get; set; }

        public int RoomTypeId { get; set; }

        public double Price => (DepartureDateTime - ArrivalDateTime).TotalDays * Room.Price;

        public int NumberOfGuests { get; set; }
        [Required]

        public string ArrivalDate { get; set; }

        [Required]
        public string DepartureDate { get; set; }

        public DateTime ArrivalDateTime => DateTime.ParseExact(ArrivalDate, "MM/dd/yyyy", CultureInfo.GetCultureInfo("en-US"));

        public DateTime DepartureDateTime => DateTime.ParseExact(DepartureDate, "MM/dd/yyyy", CultureInfo.GetCultureInfo("en-US"));

        public int RoomId { get; set; }
        public Room Room { get; set; }

    }
}
