using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TD.OPDT.Data.Models
{
    public class Office: CategoryBase, ITrackableModel
    {
        public int? ParentId { get; set; }

        public Office Parent { get; set; }

        public List<Office> Children { get; set; }

        public DateTime? CreatedAt { get; set; }

        public DateTime? ModifiedAt { get; set; }
    }
}
