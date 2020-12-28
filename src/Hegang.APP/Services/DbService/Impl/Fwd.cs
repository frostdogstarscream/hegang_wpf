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
    class Fwd : DbServiceObject
    {
        public void save(DbObject o, ref DbServiceInput input)
        {
            double seed = Convert.ToDouble(input.Dic["主井测试.S7200Smart.温度1"]);
            Random r = new Random();
            double[] ran = new double[4];
            for (int i = 0; i < 4; i++)
                ran[i] = r.Next(0, 1);

            string sql = string.Format("INSERT INTO `fwd` (`wd1`,`wd2`,`wd3`,`wd4`,`TimeStamp`) VALUES ( '{0}','{1}','{2}','{3}','{4}')", (seed+ran[0]).ToString(), (seed + ran[1]).ToString(), (seed + ran[2]).ToString(), (seed + ran[3]).ToString(), System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            o.cmmdNoReturn(sql);
        }
        /*public void save(DbObject o,ref DbServiceInput input)
        {
            string sql = string.Format("INSERT INTO `fwd` (`wd1`,`wd2`,`wd3`,`wd4`,`TimeStamp`) VALUES ( '{0}','{1}','{2}','{3}','{4}')", input.Dic["副井测试.S7200Smart.温度1"], input.Dic["副井测试.S7200Smart.温度2"], input.Dic["副井测试.S7200Smart.温度3"], input.Dic["副井测试.S7200Smart.温度4"], System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            o.cmmdNoReturn(sql);
        }*/
    }
}
