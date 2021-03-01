using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TD.OPDT.Data.DataContext;
using TD.OPDT.Data.Models;

namespace TD.OPDT.Data.Repositories
{
    public class OfficeRepository : Repository<Office>, IOfficeRepostitory
    {
        private OpenDataContext _context;

        public OfficeRepository(OpenDataContext context) : base(context)
        {
            _context = context;
        }
    }
}
