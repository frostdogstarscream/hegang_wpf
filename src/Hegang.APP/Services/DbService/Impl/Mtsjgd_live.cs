using Hegang.APP.Models;
using Hegang.APP.Models.DataBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hegang.APP.Services.DbService.Impl
{
    class Mtsjgd_live : DbServiceObject
    {
        public void save(DbObject o,ref DbServiceInput input)
        {
            if (input.Gs_tmpBuf[0] != "-1")
            {
                string time = System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                string GS = (int.Parse(input.Dic["主井测试.主提升电控.勾数"]) - int.Parse(input.Gs_tmpBuf[0])).ToString();
                String str = string.Format("INSERT INTO `mtsjgd_live` (`GS`,`DD`,`TimeStamp`) VALUES ( '{0}','{1}','{2}')", GS, input.Dic["主井测试.S7200Smart.电度"], time);
                o.cmmdNoReturn(str);
            }
            input.Gs_tmpBuf[0] = input.Dic["主井测试.主提升电控.勾数"];
        }
    }
}
