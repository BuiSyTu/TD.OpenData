using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using TD.OPDT.Data.DataContext;
using TD.OPDT.Data.FilterModels;
using TD.OPDT.Data.Models;
using Z.Expressions;

namespace TD.OPDT.Data.Repositories
{
    public class FieldRepository : Repository<Field>, IFieldRepository
    {
        private OpenDataContext _context;

        public FieldRepository(OpenDataContext context)
        {
            _context = context;
        }

        public Field Create(Field field)
        {
            field.CreatedAt = DateTime.Now;

            _context.Fields.Add(field);
            _context.SaveChanges();
            return field;
        }

        public List<Field> Get(FieldFilterModel filterModel)
        {
            var query = _context.Fields.AsQueryable();

            // include
            query = IncludeMany(query, filterModel.include);

            // order by
            query = OrderByMany(query, filterModel.orderBy);

            // FIXME : process q

            return query
                .Skip(filterModel.skip)
                .Take(filterModel.top)
                .ToList();
        }

        public List<Field> GetAll()
        {
            return _context.Fields.ToList();
        }

        public Field GetById(int id)
        {
            return _context.Fields.Find(id);
        }

        public void Remove(Field field)
        {
            _context.Fields.Remove(field);
            _context.SaveChanges();
        }

        public Field Update(Field field)
        {
            field.ModifiedAt = DateTime.Now;

            _context.Entry(field).CurrentValues.SetValues(field);
            _context.SaveChanges();
            return field;
        }
    }
}
