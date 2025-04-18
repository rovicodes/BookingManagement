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
    public class VillaRoomsRepository : IVillaRoomsRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public VillaRoomsRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public void Add(VillaRooms villaRooms)
        {
            _dbContext.VillaRooms.Add(villaRooms);
        }

        public void Delete(VillaRooms villaRooms)
        {
            _dbContext.VillaRooms.Remove(villaRooms);
        }

        public VillaRooms Get(Expression<Func<VillaRooms, bool>> filter, string? includeProperties = null)
        {
            IQueryable<VillaRooms> query = _dbContext.VillaRooms;

            query = query.Where(filter);

            if(includeProperties != null)
            {
                foreach (var inclProp in includeProperties.Split(',', StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(inclProp);
                }
            }

            return query.FirstOrDefault();
        }

        public IEnumerable<VillaRooms> GetAll(Expression<Func<VillaRooms, bool>>? filter = null, string? includeProperties = null)
        {
            IQueryable<VillaRooms> query = _dbContext.VillaRooms;

            if(filter != null)
            {
                query = query.Where(filter);
            }

            if(includeProperties != null)
            {
                foreach(var inclProp  in includeProperties.Split(',', StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(inclProp);
                }
            }

            return query.ToList();
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
