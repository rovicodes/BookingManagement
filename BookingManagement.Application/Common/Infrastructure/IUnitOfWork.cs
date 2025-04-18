using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingManagement.Application.Common.Infrastructure
{
    public interface IUnitOfWork
    {
        IVillaRepository Villa { get; }

        IVillaRoomsRepository VillaRooms { get; }

        void Save();
    }
}
