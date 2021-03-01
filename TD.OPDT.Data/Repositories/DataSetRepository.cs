using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TD.OPDT.Data.DataContext;
using TD.OPDT.Data.Models;

namespace TD.OPDT.Data.Repositories
{
    public class DataSetRepository: Repository<DataSet>, IDataSetRepository
    {
        private OpenDataContext _context;

        public DataSetRepository(OpenDataContext context) : base(context)
        {
            _context = context;
        }

        public override DataSet Add(DataSet dataSet)
        {
            dataSet.AttachmentsRaw = dataSet.Attachments.ToString();
            _context.DataSets.Add(dataSet);
            _context.SaveChanges();
            return dataSet;
        }
    }
}
