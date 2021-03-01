using System.Collections.Generic;
using TD.OPDT.Data.FilterModels;
using TD.OPDT.Data.Models;

namespace TD.OPDT.Data.Repositories
{
    public interface IFieldRepository : IRepository<Field>
    {
        List<Field> Get(FieldFilterModel filterModel);
    }
}
