using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TD.OPDT.Data.Models
{
    public class DataSet : CategoryBase, ITrackableModel
    {
        public int FieldId { get; set; }
        public Field Field { get; set; }
        public int OfficeId { get; set; }
        public Office Office { get; set; }
        [NotMapped]
        public List<FileAttachment> Attachments { get; set; }
        public string AttachmentsRaw { get; set; }
        public string LinkApi { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? ModifiedAt { get; set; }
    }
}
