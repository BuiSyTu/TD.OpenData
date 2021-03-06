﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TD.OPDT.Data.Models;

namespace TD.OPDT.Data.Repositories
{
    public interface IDataSetRepository: IRepository<DataSet>
    {
        int Count(int? fieldId, int? officeId);
    }
}
