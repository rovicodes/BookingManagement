using BookingManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingManagement.Application.Common.Infrastructure
{
    public interface IAmenitiesRepository: IRepository<Amenity>
    {
        void Update(Amenity amenity);
    }
}
