using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace BookingManagement.Domain.Entities
{
    public class Amenity
    {
        public int Id { get; set; }

        [Required]
        public string? Name { get; set; }

        public string ? Description { get; set; }

        [ForeignKey("Villa")]
        [DisplayName("Villa")]
        public int villaId { get; set; }
        public Villa? Villa { get; set; }


    }
}
