using BookingManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingManagement.Domain.ViewModels
{
    public class HomeVM
    {
        public IEnumerable<Villa>? Villa { get; set; }

        public DateOnly CheckInDate { get; set; }

        public int NoOfDays  { get; set; }
        
    }
}
