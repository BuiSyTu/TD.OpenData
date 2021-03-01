using System;
using System.Collections.Generic;
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

        public FieldRepository(OpenDataContext context) : base(context)
        {
            _context = context;
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
    }
}
