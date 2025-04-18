using BookingManagement.Application.Common.Infrastructure;
using BookingManagement.Domain.Entities;
using BookingManagement.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BookingManagement.Infrastructure.Repository
{
    public class VillaRoomsRepository : Repository<VillaRooms>, IVillaRoomsRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public VillaRoomsRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public void Save()
        {
            _dbContext.SaveChanges();
        }

        public void Update(VillaRooms villaRooms)
        {
            _dbContext.VillaRooms.Update(villaRooms);
        }
    }
}
