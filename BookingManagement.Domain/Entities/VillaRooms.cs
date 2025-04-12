using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingManagement.Domain.Entities
{
    public class VillaRooms
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        [DisplayName("Room Number")]
        public int Villa_RoomId { get; set; }

        [ForeignKey("Villa")]
        [DisplayName("Villa Number")]
        public int VillaId { get; set; }
        public Villa? Villa { get; set; }
        
        [DisplayName("Special Details")]
        public string? SpecialDetails { get; set; }
    }
}
