using BookingManagement.Domain.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingManagement.Domain.ViewModels
{
    public class VillaRoomsVM
    {
        public VillaRooms? VillaRooms { get; set; }

        public IEnumerable<SelectListItem>? VillaList { get; set; }

    }
}
