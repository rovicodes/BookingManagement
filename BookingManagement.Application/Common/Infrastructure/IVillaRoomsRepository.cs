using BookingManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BookingManagement.Application.Common.Infrastructure
{
    public interface IVillaRoomsRepository : IRepository<VillaRooms>
    {
      
        void Update(VillaRooms villaRooms);

    }
}
