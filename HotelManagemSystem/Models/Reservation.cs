using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace HotelManagemSystem.Models
{
    public class Reservation
    {
        public int Id { get; set; }
        [StringLength(100)]
        [Display(Name = "Guest name")]
        public string Name { get; set; }
        [StringLength(100)]
        [Display(Name = "Guest email")]
        public string Email { get; set; }
        [Display(Name = "Room type")]
        public Room Room { get; set; }
        public int RoomId { get; set; }
        [Display(Name = "Price")]
        public double Price { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime ArrivalDateTime { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime DepartureDateTime { get; set; }
        [Display(Name = "Number of guests")]
        public int NumberOfGuests { get; set; }
        [NotMapped]
        [Display(Name = "Arrival date")]
        public string ArrivalDate => ArrivalDateTime.ToString("MM/dd/yyyy");

        [Display(Name = "Departure date")]
        [NotMapped]
        public string DepartureDate => DepartureDateTime.ToString("MM/dd/yyyy");

    }
}
