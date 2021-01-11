using Hegang.APP.Models;
using Hegang.APP.Models.DataBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hegang.APP.Services.DbService.Impl
{
    class Fzzyl : DbServiceObject
    {
        public void save(DbObject o,ref DbServiceInput input)
        {
            string sql = string.Format("INSERT INTO fzzyl (yl,timestamp) VALUES('{0}','{1}')", input.Dic["副井.副提升电控.油压"], System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            o.cmmdNoReturn(sql);
        }
    }
}
