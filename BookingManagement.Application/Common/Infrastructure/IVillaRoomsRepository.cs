using BookingManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BookingManagement.Application.Common.Infrastructure
{
    public interface IVillaRoomsRepository
    {
        IEnumerable<VillaRooms> GetAll(Expression<Func<VillaRooms, bool>>? filter = null, String? includeProperties = null);

        VillaRooms Get(Expression<Func<VillaRooms, bool>> filter, String? includeProperties = null);

        void Add(VillaRooms villaRooms);
        void Update(VillaRooms villaRooms);
        void Delete(VillaRooms villaRooms);
        void Save();

    }
}
