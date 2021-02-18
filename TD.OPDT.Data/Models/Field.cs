using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TD.OPDT.Data.Models
{
    public class Field : BaseModel
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public int Order { get; set; }
        public bool Active { get; set; }
    }
}
