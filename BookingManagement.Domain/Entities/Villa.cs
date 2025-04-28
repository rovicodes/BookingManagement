using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingManagement.Domain.Entities
{
    public class Villa
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public string? Description { get; set; }

        [Display(Name = "Price per Night")]
        public double Price { get; set; }
        public int Sqft { get; set; }
        public int Occupancy { get; set; }

        [Display(Name = "Image URL")]
        public string? ImageUrl { get; set; }

        [NotMapped]
        public IFormFile? Image { get; set; }
        public DateTime? Created_Date { get; set; }
        public DateTime? Updated_Date { get; set; }

        public IEnumerable<VillaRooms>? VillaRooms { get; set; }

        public IEnumerable<Amenity>? Amenities { get; set; }
    }
}
