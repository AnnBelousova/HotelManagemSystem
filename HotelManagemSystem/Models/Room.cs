using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace HotelManagemSystem.Models
{

    public class Room
    {
        public int RoomId { get; set; }
        [Display(Name = "Room No.")]
        public int RoomNo { get; set; }
        [Display(Name = "Room Description")]
        public string Description { get; set; }
        [Display(Name = "Room Price")]
        public double Price { get; set; }
        [Display(Name = "Room Capacity")]
        public int RoomCapacity { get; set; }
        public string RoomImage { get; set; }
        public int RoomTypeId { get; set; }
        [Display(Name = "Room Type")]
        public RoomType RoomType { get; set; }
        [Display(Name = "Room Availability")]
        public int IsAvailable { get; set; }
    }
}
