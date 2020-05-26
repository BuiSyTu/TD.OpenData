using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TD.Core.Api.Common;

namespace TD.Example.Library.Models
{
    public abstract class ModelBaseExt : ModelBase<int>
    {
        public ModelBaseExt()
        {
            Created = DateTime.Now;
        }

        private string name;
        private string code;
        private string createdBy;
        private DateTime? created;
        private string modifiedBy;
        private DateTime? modified;

        /// <summary>
        /// Được tạo bởi
        /// </summary>
        public string CreatedBy
        {
            get
            {
                return createdBy;
            }
            set
            {
                createdBy = value;
                NotifyPropertyChanged();
            }
        }
        /// <summary>
        /// Được tạo tại thời điểm
        /// </summary>
        public DateTime? Created
        {
            get
            {
                return created;
            }
            set
            {
                created = value;
                NotifyPropertyChanged();
            }
        }
        /// <summary>
        /// Được chỉnh sửa bởi
        /// </summary>
        public string ModifiedBy
        {
            get
            {
                return modifiedBy;
            }
            set
            {
                modifiedBy = value;
                NotifyPropertyChanged();
            }
        }
        /// <summary>
        /// Được chỉnh sửa tại thời điểm
        /// </summary>
        public DateTime? Modified
        {
            get
            {
                return modified;
            }
            set
            {
                modified = value;
                NotifyPropertyChanged();
            }
        }
        /// <summary>
        /// Name
        /// </summary>
        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                name = value;
                NotifyPropertyChanged();
            }
        }

        /// <summary>
        /// Code
        /// </summary>
        public string Code
        {
            get
            {
                return code;
            }
            set
            {
                code = value;
                NotifyPropertyChanged();
            }
        }
    }
}
