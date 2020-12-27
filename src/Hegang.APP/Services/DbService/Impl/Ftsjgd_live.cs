using Hegang.APP.Models;
using Hegang.APP.Models.DataBase;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hegang.APP.Services.DbService.Impl
{
    class Ftsjgd_live : DbServiceObject
    {
        public void save(DbObject o,ref DbServiceInput input)
        {
            if (input.Gs_tmpBuf[1] != "-1")
            {
                string str = "SELECT DD FROM ftsjgd_live WHERE id = (SELECT MAX(id) FROM ftsjgd_live);";
                DataTable dt = o.GetDataTable(str);
                double DD = Convert.ToDouble(dt.Rows[0][0]);
                Random r = new Random();
                DD+=r.Next(0, 50);

                string time = System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                string GS = (int.Parse(input.Dic["副井测试.副提升电控.勾数"]) - int.Parse(input.Gs_tmpBuf[1])).ToString();
                str = string.Format("INSERT INTO `ftsjgd_live` (`GS`,`DD`,`TimeStamp`) VALUES ( '{0}','{1}','{2}')", GS, DD.ToString(), time);
                o.cmmdNoReturn(str);
            }
            input.Gs_tmpBuf[1] = input.Dic["副井测试.副提升电控.勾数"];
        }

        /*public void save(DbObject o, ref DbServiceInput input)
        {
            if (input.Gs_tmpBuf[1] != "-1")
            {
                string time = System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                string GS = (int.Parse(input.Dic["副井测试.副提升电控.勾数"]) - int.Parse(input.Gs_tmpBuf[0])).ToString();
                String str = string.Format("INSERT INTO `ftsjgd_live` (`GS`,`DD`,`TimeStamp`) VALUES ( '{0}','{1}','{2}')", GS, input.Dic["副井测试.S7200Smart.电度"], time);
                o.cmmdNoReturn(str);
            }
            input.Gs_tmpBuf[0] = input.Dic["副井测试.副提升电控.勾数"];
        }*/
    }
}
