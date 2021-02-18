using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TD.OPDT.Data.FilterModels
{
    public class BaseFilterModel
    {
        public int skip { get; set; } = 0;
        public int top { get; set; } = 100;
        public string q { get; set; } = null;
        public string orderBy { get; set; } = null;
        public bool count { get; set; } = false;
        public string include { get; set; } = null;
    }
}
