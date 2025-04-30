using BookingManagement.Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingManagement.Infrastructure.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
             
        }

        public DbSet<Villa> Villas { get; set; }

        public DbSet<VillaRooms> VillaRooms { get; set; }

        public DbSet<Amenity> Amenity { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
             base.OnModelCreating(modelBuilder); // you need this if you are adding some custom stuff to table or if you are creating identity, because you are overriding onmodecreating method

            modelBuilder.Entity<Villa>().HasData(
            new Villa
            {
                Id = 1,
                Name = "Royal Villa",
                Description = "Fusce 11 tincidunt maximus leo, sed scelerisque massa auctor sit amet. Donec ex mauris, hendrerit quis nibh ac, efficitur fringilla enim.",
                ImageUrl = "https://placehold.co/600x400",
                Occupancy = 4,
                Price = 200,
                Sqft = 550,
            },
            new Villa
            {
                Id = 2,
                Name = "Premium Pool Villa",
                Description = "Fusce 11 tincidunt maximus leo, sed scelerisque massa auctor sit amet. Donec ex mauris, hendrerit quis nibh ac, efficitur fringilla enim.",
                ImageUrl = "https://placehold.co/600x401",
                Occupancy = 4,
                Price = 300,
                Sqft = 550,
            },
            new Villa
            {
                Id = 3,
                Name = "Luxury Pool Villa",
                Description = "Fusce 11 tincidunt maximus leo, sed scelerisque massa auctor sit amet. Donec ex mauris, hendrerit quis nibh ac, efficitur fringilla enim.",
                ImageUrl = "https://placehold.co/600x402",
                Occupancy = 4,
                Price = 400,
                Sqft = 750,
            }
            );

            modelBuilder.Entity<VillaRooms>().HasData(
               new VillaRooms
               {
                   Villa_RoomId = 101,
                   VillaId = 1,
               },
               new VillaRooms
               {
                   Villa_RoomId = 102,
                   VillaId = 1,
               },
               new VillaRooms
               {
                   Villa_RoomId = 103,
                   VillaId = 1,
               },
               new VillaRooms
               {
                   Villa_RoomId = 104,
                   VillaId = 1,
               },
               new VillaRooms
               {
                   Villa_RoomId = 201,
                   VillaId = 2,
               },
               new VillaRooms
               {
                   Villa_RoomId = 202,
                   VillaId = 2,
               },
               new VillaRooms
               {
                   Villa_RoomId = 203,
                   VillaId = 2,
               },
               new VillaRooms
               {
                   Villa_RoomId = 301,
                   VillaId = 3,
               },
               new VillaRooms
               {
                   Villa_RoomId = 302,
                   VillaId = 3,
               }
               );

            modelBuilder.Entity<Amenity>().HasData(
              new Amenity
              {
                  Id = 1,
                  villaId = 1,
                  Name = "Private Pool"
              }, new Amenity
              {
                  Id = 2,
                  villaId = 1,
                  Name = "Microwave"
              }, new Amenity
              {
                  Id = 3,
                  villaId = 1,
                  Name = "Private Balcony"
              }, new Amenity
              {
                  Id = 4,
                  villaId = 1,
                  Name = "1 king bed and 1 sofa bed"
              },

              new Amenity
              {
                  Id = 5,
                  villaId = 2,
                  Name = "Private Plunge Pool"
              }, new Amenity
              {
                  Id = 6,
                  villaId = 2,
                  Name = "Microwave and Mini Refrigerator"
              }, new Amenity
              {
                  Id = 7,
                  villaId = 2,
                  Name = "Private Balcony"
              }, new Amenity
              {
                  Id = 8,
                  villaId = 2,
                  Name = "king bed or 2 double beds"
              },

              new Amenity
              {
                  Id = 9,
                  villaId = 3,
                  Name = "Private Pool"
              }, new Amenity
              {
                  Id = 10,
                  villaId = 3,
                  Name = "Jacuzzi"
              }, new Amenity
              {
                  Id = 11,
                  villaId = 3,
                  Name = "Private Balcony"
              });
        }
    }
}
