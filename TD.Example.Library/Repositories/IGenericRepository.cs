using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TD.Core.Api.Common;

namespace TD.Example.Library.Repositories
{
    public interface IGenericRepository<T> : IGenericController<T, int>
    {
        IEnumerable<T> AddRange(IEnumerable<T> entities);

        List<T> Get(
            int skip = 0, int take = 100,
            string search = null,
            ICollection<string> orderBy = null, ICollection<string> include = null, string field = null, string value = null);

        List<T> Get(
           int skip = 0, int take = 100,
           string search = null,
           ICollection<string> orderBy = null, ICollection<string> include = null, string field = null, int value = 0);

        List<T> Get(
            int skip = 0, int take = 100,
            string search = null,
            ICollection<string> orderBy = null, ICollection<string> include = null, string field = null, bool value = true);

        int CountQuery(string search = null, string field = null, string value = null);

        int CountQuery(string search = null, string field = null, bool value = false);

        int CountQuery(string search = null, string field = null, int value = 0);
    }
}