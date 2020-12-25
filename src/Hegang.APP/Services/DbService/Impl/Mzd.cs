using Hegang.APP.Models;
using Hegang.APP.Models.DataBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hegang.APP.Services.DbService.Impl
{
    class Mzd : DbServiceObject
    {
        public void save(DbObject o,ref DbServiceInput input)
        {
            string sql= string.Format("INSERT INTO `mzd` (`zd1`,`zd2`,`zd3`,`zd4`,`TimeStamp`) VALUES ( '{0}','{1}','{2}','{3}','{4}')", input.Dic["主井测试.S7200Smart.振动1"], input.Dic["主井测试.S7200Smart.振动2"], input.Dic["主井测试.S7200Smart.振动3"], input.Dic["主井测试.S7200Smart.振动4"], System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            o.cmmdNoReturn(sql);
        }
    }
}
