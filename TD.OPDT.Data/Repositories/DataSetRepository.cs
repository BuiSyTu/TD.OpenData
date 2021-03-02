using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TD.Core.Api.Common;
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

        //public override DataSet GetById(int id)
        //{
        //    var dataSet = base.GetById(id);
        //    dataSet.Attachments = FileAttachmentCollection.Parse(dataSet.AttachmentsRaw);
        //    return dataSet;
        //}

        //public override DataSet Add(DataSet dataSet)
        //{
        //    dataSet.AttachmentsRaw = dataSet.Attachments.ToString();
        //    _context.DataSets.Add(dataSet);
        //    _context.SaveChanges();
        //    return dataSet;
        //}

        public int Count(int? fieldId, int? officeId)
        {
            var query = _context.DataSets.AsQueryable();

            if (fieldId.HasValue) query = query.Where(x => x.FieldId == fieldId.Value);
            if (officeId.HasValue) query = query.Where(x => x.OfficeId == officeId.Value);

            return query.Count();
        }
    }
}
