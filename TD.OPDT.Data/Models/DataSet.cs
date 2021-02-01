using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TD.OPDT.Data.Models
{
    public class DataSet : BaseModel
    {
        public string Name { get; set; }
        public int OfficeId { get; set; }
        public int FieldId { get; set; }
        public string Description { get; set; }
        public string Code { get; set; }
        public int Status { get; set; }
        public DateTime? PublishDate { get; set; }
    }
}
