using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hegang.APP.Models.DataBase
{
    interface DbObject
    {
        void cmmdNoReturn(string sql);
        DataTable GetDataTable(string sql);
    }
}
