using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using TD.OPDT.Data.Models;
using Z.Expressions;

namespace TD.OPDT.Data.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly DbContext _context;

        public Repository(DbContext context) 
        {
            _context = context;
        }

        public virtual T Add(T model)
        {
            if (model is ITrackableModel)
            {
                var trackable = (ITrackableModel)model;
                trackable.CreatedAt = DateTime.Now;
            }

            _context.Set<T>().Add(model);
            _context.SaveChanges();
            return model;
        }

        public int Count()
        {
            return _context.Set<T>().Count();
        }

        public virtual void Delete(T model)
        {
            _context.Set<T>().Remove(model);
            _context.SaveChanges();
        }

        public virtual List<T> GetAll()
        {
            return _context.Set<T>().ToList();
        }

        public virtual T GetById(int id)
        {
            return _context.Set<T>().Find(id);
        }

        public virtual IQueryable<T> IncludeMany(IQueryable<T> queryable, string include)
        {
            ICollection<string> includeCollection = null;
            if (!string.IsNullOrEmpty(include))
            {
                includeCollection = new Regex(@"\s*,\s*").Split(include);
            }

            if (includeCollection != null && includeCollection.Count > 0)
            {
                foreach (var item in includeCollection)
                {
                    queryable = queryable.Include(item);
                }
            }
            return queryable;
        }

        public virtual IQueryable<T> OrderByMany(IQueryable<T> queryable, string orderBy)
        {
            var splitChars = new char[] { '|' };

            ICollection<string> orderByCollection = null;
            if (!string.IsNullOrEmpty(orderBy))
            {
                orderByCollection = new Regex(@"\s*,\s*").Split(orderBy);
            }

            if (orderByCollection == null)
            {
                return queryable;
            }
            var checkOrdered = false;
            foreach (string str in orderByCollection)
            {
                if (string.IsNullOrEmpty(str))
                {
                    continue;
                }

                var spl = str.Split(splitChars);

                var field = spl[0];
                var desc = spl.Length > 1 && spl[1].ToUpper() == "DESC";

                if (!checkOrdered)
                {
                    if (desc)
                    {
                        queryable = queryable.OrderByDescendingDynamic(x => $"x.{field}");
                    }
                    else
                    {
                        queryable = queryable.OrderByDynamic(x => $"x.{field}");
                    }
                    checkOrdered = true;
                }
                else
                {
                    if (desc)
                    {
                        queryable = ((IOrderedQueryable<T>)queryable).ThenByDescendingDynamic(x => $"x.{field}");
                    }
                    else
                    {
                        queryable = ((IOrderedQueryable<T>)queryable).ThenByDynamic(x => $"x.{field}");
                    }
                }

            }

            return queryable;
        }

        public virtual T Update(T model)
        {
            _context.Set<T>().Attach(model);

            if (model is ITrackableModel)
            {
                var trackable = (ITrackableModel)model;
                trackable.ModifiedAt = DateTime.Now;
            }

            _context.Entry(model).State = EntityState.Modified;
            _context.SaveChanges();
            return model;
        }
    }
}
