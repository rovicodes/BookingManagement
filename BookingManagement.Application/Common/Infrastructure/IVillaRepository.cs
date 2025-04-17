using BookingManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BookingManagement.Application.Common.Infrastructure
{
    public interface IVillaRepository
    {

        IEnumerable<Villa> GetAll(Expression<Func<Villa, bool>>? filter = null, string? includeProperties =  null);

        Villa Get(Expression<Func<Villa, bool>> filter , string? includeProperties = null);
        void Add(Villa villa);
        void Update(Villa villa);
        void Delete(Villa villa);

        void Save();

        
    }
}
