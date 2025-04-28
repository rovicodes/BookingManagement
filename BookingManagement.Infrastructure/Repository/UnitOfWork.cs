using BookingManagement.Application.Common.Infrastructure;
using BookingManagement.Domain.Entities;
using BookingManagement.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingManagement.Infrastructure.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _dbContext;

        public IVillaRepository Villa { get; private set; }

        public IVillaRoomsRepository VillaRooms { get; private set; }

        public IAmenitiesRepository Amenities { get; private set; }

        public UnitOfWork(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
            Villa = new VillaRepository(_dbContext);
            VillaRooms = new VillaRoomsRepository(_dbContext);
            Amenities = new AmenitiesRepository(_dbContext);
        }

        public void Save()
        {
            _dbContext.SaveChanges();
        }
    }
}
