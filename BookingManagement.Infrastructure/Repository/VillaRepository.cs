using BookingManagement.Application.Common.Infrastructure;
using BookingManagement.Domain.Entities;
using BookingManagement.Infrastructure.Data;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BookingManagement.Infrastructure.Repository
{
    public class VillaRepository : IVillaRepository
    {
        private readonly ApplicationDbContext _dbContext;
        public VillaRepository( ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;  
        }
        public void Add(Villa villa)
        {
            _dbContext.Villas.Add(villa);
        }

        public void Delete(Villa villa)
        {
            _dbContext.Villas.Remove(villa);
        }

        public Villa Get(Expression<Func<Villa, bool>> filter, string? includeProperties = null)
        {
            IQueryable<Villa> query = _dbContext.Villas;
            query = query.Where(filter);

            if(includeProperties != null)
            {
                foreach(var inclProp in includeProperties.Split(',', StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(inclProp);
                }
            }
            return query.FirstOrDefault();
        }

        public IEnumerable<Villa> GetAll(Expression<Func<Villa, bool>>? filter = null, string? includeProperties = null)
        {
            IQueryable<Villa> query = _dbContext.Villas;

            if(filter != null)
            {
                query = query.Where(filter);
            }
            
            if(includeProperties != null)
            {
                foreach(var inclProp in includeProperties.Split(',', StringSplitOptions.RemoveEmptyEntries))
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

        public void Update(Villa villa)
        {
            _dbContext.Villas.Update(villa);
        }
    }
}
