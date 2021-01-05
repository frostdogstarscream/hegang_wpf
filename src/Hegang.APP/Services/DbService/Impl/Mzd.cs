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
        public void save(DbObject o, ref DbServiceInput input)
        {
            string sql;

            if (input.Zd_flag[0])
            {
                Random r = new Random();
                double[] simulate_zd = new double[4];
                for (int i = 0; i < 4; i++)
                    simulate_zd[i] = r.Next(0, 200) / 1000d;

                sql = string.Format("INSERT INTO `mzd` (`zd1`,`zd2`,`zd3`,`zd4`,`TimeStamp`) VALUES ( '{0}','{1}','{2}','{3}','{4}')",
                    simulate_zd[0].ToString(), simulate_zd[1].ToString(), simulate_zd[2].ToString(), simulate_zd[3].ToString(), System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            }
            else
                sql = string.Format("INSERT INTO `mzd` (`zd1`,`zd2`,`zd3`,`zd4`,`TimeStamp`) VALUES ( '{0}','{1}','{2}','{3}','{4}')",
                    "0", "0", "0", "0", System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            o.cmmdNoReturn(sql);
        }
        /*public void save(DbObject o,ref DbServiceInput input)
        {
            string sql= string.Format("INSERT INTO `mzd` (`zd1`,`zd2`,`zd3`,`zd4`,`TimeStamp`) VALUES ( '{0}','{1}','{2}','{3}','{4}')", input.Dic["主井测试.S7200Smart.振动1"], input.Dic["主井测试.S7200Smart.振动2"], input.Dic["主井测试.S7200Smart.振动3"], input.Dic["主井测试.S7200Smart.振动4"], System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            o.cmmdNoReturn(sql);
        }*/
    }
}
