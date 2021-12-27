using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelManagemSystem.Models
{
    public class RoomType
    {
        public int RoomTypeId { get; set; }
        public string Name { get; set; }
        public List<Room> Rooms { get; set; }
    }
}
