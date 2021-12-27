using HotelManagemSystem.Data;
using HotelManagemSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelManagementSystem.Data
{
    public static class DbInitializer
    {
        public static void Initialize(HotelContext context)
        {
            context.Database.EnsureCreated();

            //Room
            if (context.Rooms.Any())
            {
                return;   
            }

            var rooms = new Room[]
            {
            };
            foreach (Room r in rooms)
            {
                context.Rooms.Add(r);
            }
            context.SaveChanges();
        
            //RoomType
            if (context.RoomTypes.Any())
            {
                return;   
            }

            var roomTypes = new RoomType[]
            {
            };

            foreach (RoomType rt in roomTypes)
            {
                context.RoomTypes.Add(rt);
            }
            context.SaveChanges();

            //Reservation
            if (context.Reservation.Any())
            {
                return;   
            }

            var reservation = new Reservation[]
            {
            };

            foreach (Reservation res in reservation)
            {
                context.Reservation.Add(res);
            }
            context.SaveChanges();


        }
    }
}