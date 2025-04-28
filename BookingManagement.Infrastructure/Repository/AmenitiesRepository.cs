using BookingManagement.Application.Common.Infrastructure;
using BookingManagement.Domain.Entities;
using BookingManagement.Infrastructure.Data;
using BookingManagement.Infrastructure.Migrations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingManagement.Infrastructure.Repository
{
    public class AmenitiesRepository : Repository<Amenity>, IAmenitiesRepository
    {
        private readonly ApplicationDbContext _dbContext;
        public AmenitiesRepository(ApplicationDbContext dbContext) : base(dbContext) 
        {
            _dbContext = dbContext;
        }

        public void Update(Amenity amenity)
        {
            _dbContext.Amenity.Update(amenity);
        }
    }
}
