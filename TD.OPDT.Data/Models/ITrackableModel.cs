using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TD.OPDT.Data.Models
{
    public interface ITrackableModel
    {
        DateTime? CreatedAt { get; set; }
        DateTime? ModifiedAt { get; set; }
    }
}
