using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TD.Core.Api.Common;
using TD.Core.Api.Mvc.Extensions;
using TD.Core.Api.Mvc.Generic;
using TD.Example.Library.Data;
using TD.Example.Library.Models;
using Z.Expressions;

namespace TD.Example.Library.Repositories
{

    public abstract class GenericRepository<T> : MvcApiControler<T, int>, IGenericController<T, int> where T : ModelBaseExt, new()
    {
        private readonly UserContext _dbContext;

        public GenericRepository(UserContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public override List<T> Get(
            int skip = 0, int take = 100,
            string search = null, bool searchIsQuery = false,
            ICollection<string> orderBy = null, IEnumerable<string> viewFields = null)
        {
            if (orderBy == null || orderBy.Count == 0)
            {
                orderBy = new string[] { "ID" };
            }
            return base.Get(skip, take, search, false, orderBy, null);
        }

        public List<T> Get(
            int skip = 0, int take = 100,
            string search = null,
            ICollection<string> orderBy = null, ICollection<string> include = null, string field = null, string value = null)
        {
            if (orderBy == null || orderBy.Count == 0)
            {
                orderBy = new string[] { "ID" };
            }
            try
            {
                var query = CreateSearchQuery(_dbContext.Set<T>(), search, false);
                if (include != null)
                {
                    var ids = query.Select(x => x.ID).ToList();
                    query = _dbContext.Set<T>().AsQueryable();

                    foreach (var item in include)
                    {
                        query = query.Include(item);

                    }

                    query = query.Where(x => ids.Contains(x.ID));
                }
                if (!string.IsNullOrEmpty(field))
                {
                    value = string.IsNullOrEmpty(value) ? string.Empty : value;
                    query = query.Where(x => $"x.{field} == v", new { v = value });
                }
                query = query.OrderBySQL(orderBy);
                if (skip > 0)
                    query = query.Skip(skip);
                if (take > 0)
                    query = query.Take(take);
                return query.ToList();
            }
            catch (Exception ex)
            {
                throw new ApiException("Error getting items", ex);
            }
        }

        public List<T> Get(
           int skip = 0, int take = 100,
           string search = null,
           ICollection<string> orderBy = null, ICollection<string> include = null, string field = null, int value = 0)
        {
            if (orderBy == null || orderBy.Count == 0)
            {
                orderBy = new string[] { "ID" };
            }
            try
            {
                var query = CreateSearchQuery(_dbContext.Set<T>(), search, false);
                if (include != null)
                {
                    var ids = query.Select(x => x.ID).ToList();
                    query = _dbContext.Set<T>().AsQueryable();

                    foreach (var item in include)
                    {
                        query = query.Include(item);
                    }

                    query = query.Where(x => ids.Contains(x.ID));
                }
                if (!string.IsNullOrEmpty(field))
                {
                    query = query.Where(x => $"x.{field} == v", new { v = value });
                }
                query = query.OrderBySQL(orderBy);
                if (skip > 0)
                    query = query.Skip(skip);
                if (take > 0)
                    query = query.Take(take);
                return query.ToList();
            }
            catch (Exception ex)
            {
                throw new ApiException("Error getting items", ex);
            }
        }

        public List<T> Get(
            int skip = 0, int take = 100,
            string search = null,
            ICollection<string> orderBy = null, ICollection<string> include = null, string field = null, bool value = true)
        {
            if (orderBy == null || orderBy.Count == 0)
            {
                orderBy = new string[] { "ID" };
            }
            try
            {
                var query = CreateSearchQuery(_dbContext.Set<T>(), search, false);
                if (include != null)
                {
                    var ids = query.Select(x => x.ID).ToList();
                    query = _dbContext.Set<T>().AsQueryable();

                    foreach (var item in include)
                    {
                        query = query.Include(item);
                    }

                    query = query.Where(x => ids.Contains(x.ID));
                }
                if (!string.IsNullOrEmpty(field))
                {
                    query = query.Where(x => $"x.{field} == v", new { v = value });
                }
                query = query.OrderBySQL(orderBy);
                if (skip > 0)
                    query = query.Skip(skip);
                if (take > 0)
                    query = query.Take(take);
                return query.ToList();
            }
            catch (Exception ex)
            {
                throw new ApiException("Error getting items", ex);
            }
        }

        public override T Add(T entity)
        {
            entity.Created = DateTime.Now;
            return base.Add(entity);
        }

        public virtual IEnumerable<T> AddRange(IEnumerable<T> entities)
        {
            foreach (var entity in entities)
            {
                entity.Created = DateTime.Now;
            }
            var listTs = _dbContext.Set<T>().AddRange(entities);
            _dbContext.SaveChanges();
            return listTs;
        }

        public virtual int CountQuery(string search = null, string field = null, string value = null)
        {
            try
            {
                var query = CreateSearchQuery(_dbContext.Set<T>(), search, false);
                if (!string.IsNullOrEmpty(field))
                {
                    value = string.IsNullOrEmpty(value) ? string.Empty : value;
                    query = query.Where(x => $"x.{field} == v", new { v = value });
                }
                return query.Count();
            }
            catch (Exception ex)
            {
                throw new ApiException("Error getting count", ex);
            }
        }

        public virtual int CountQuery(string search = null, string field = null, int value = 0)
        {
            try
            {
                var query = CreateSearchQuery(_dbContext.Set<T>(), search, false);
                if (!string.IsNullOrEmpty(field))
                {

                    query = query.Where(x => $"x.{field} == v", new { v = value });
                }
                return query.Count();
            }
            catch (Exception ex)
            {
                throw new ApiException("Error getting count", ex);
            }
        }

        public virtual int CountQuery(string search = null, string field = null, bool value = false)
        {
            try
            {
                var query = CreateSearchQuery(_dbContext.Set<T>(), search, false);
                if (!string.IsNullOrEmpty(field))
                {
                    query = query.Where(x => $"x.{field} == v", new { v = value });
                }
                return query.Count();
            }
            catch (Exception ex)
            {
                throw new ApiException("Error getting count", ex);

            }
        }
    }
}
