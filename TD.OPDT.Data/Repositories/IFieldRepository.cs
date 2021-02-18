using System.Collections.Generic;
using TD.OPDT.Data.FilterModels;
using TD.OPDT.Data.Models;

namespace TD.OPDT.Data.Repositories
{
    public interface IFieldRepository
    {
        Field GetById(int id);
        List<Field> GetAll();
        List<Field> Get(FieldFilterModel filterModel);
        Field Create(Field field);
        Field Update(Field field);
        void Remove(Field field);
    }
}
