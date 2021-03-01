using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TD.OPDT.Data.Models
{
    public class FileAttachment
    {
        public string Name { get; set; }
        public string Url { get; set; }
        public int Size { get; set; }
        public string ToRaw(List<FileAttachment> attachments)
        {
            var result = string.Empty;
            foreach (var item in attachments)
            {
                result += item.Url + " ";
            }

            return result;
        }
    }
}
