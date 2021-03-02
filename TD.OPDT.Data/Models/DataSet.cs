using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TD.Core.Api.Common;

namespace TD.OPDT.Data.Models
{
    public class DataSet : CategoryBase, ITrackableModel
    {
        public int FieldId { get; set; }

        public Field Field { get; set; }

        public int OfficeId { get; set; }

        public Office Office { get; set; }

        [NotMapped]
        public FileAttachmentCollection Attachments { get; set; }

        [JsonIgnore]
        public string AttachmentsRaw { get; set;}

        public string LinkApi { get; set; }

        public DateTime? CreatedAt { get; set; }

        public DateTime? ModifiedAt { get; set; }
    }
}
