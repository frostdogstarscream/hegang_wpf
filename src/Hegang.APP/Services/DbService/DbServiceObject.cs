using Hegang.APP.Models;
using Hegang.APP.Models.DataBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hegang.APP.Services.DbService
{
    interface DbServiceObject
    {
        void save(DbObject o,ref DbServiceInput input);
    }
}
