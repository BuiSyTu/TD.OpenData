using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TD.OPDT.Data.Repositories
{
    public interface IRepository<T>
    {
        List<T> GetAll();
        T Add(T model);
        T GetById(int id);
        T Update(T model);
        void Delete(T model);
        IQueryable<T> IncludeMany(IQueryable<T> query, string include);
        IQueryable<T> OrderByMany(IQueryable<T> query, string orderBy);
    }
}
