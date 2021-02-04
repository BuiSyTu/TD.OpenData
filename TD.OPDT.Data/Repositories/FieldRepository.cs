using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TD.OPDT.Data.DataContext;
using TD.OPDT.Data.Models;

namespace TD.OPDT.Data.Repositories
{
    public class FieldRepository : IFieldRepository
    {
        private OpenDataContext _context;

        public FieldRepository(OpenDataContext context)
        {
            _context = context;
        }

        public Field Create(Field field)
        {
            _context.Fields.Add(field);
            _context.SaveChanges();
            return field;
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
            _context.Entry(field).CurrentValues.SetValues(field);
            _context.SaveChanges();
            return field;
        }
    }
}
