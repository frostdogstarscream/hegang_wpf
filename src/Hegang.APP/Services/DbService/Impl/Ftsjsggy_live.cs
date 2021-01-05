using Hegang.APP.Models;
using Hegang.APP.Models.DataBase;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hegang.APP.Services.DbService.Impl
{
    class Ftsjsggy_live : DbServiceObject
    {
        public void save(DbObject o,ref DbServiceInput input)
        {
            string str = "SELECT COUNT(id) FROM `ftsjsggy_live`;";
            DataTable dt=o.GetDataTable(str);
            int row_num = Convert.ToInt32(dt.Rows[0][0]);

            string time = System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            string str_save= string.Format("INSERT INTO `ftsjsggy_live` (`SD`,`GD`,`GL`,`GLYS`,`TimeStamp`) VALUES ( '{0}','{1}','{2}','{3}','{4}');", input.Dic["副井测试.副提升电控.速度"], input.Dic["副井测试.副提升电控.高度"], input.Dic["副井测试.S7200Smart.功率"], input.Dic["副井测试.S7200Smart.功率因素"], time);
            string str_del= "DELETE FROM ftsjsggy_live WHERE id=(SELECT MIN(id) FROM `ftsjsggy_live`);";
            
            if (row_num == 10)
            {
                o.cmmdNoReturn(str_del);
                o.cmmdNoReturn(str_save);
            }
            else if (row_num < 10)
            {
                o.cmmdNoReturn(str_save);
            }
            else
            {
                for (int i = 0; i < row_num - 10; i++)
                    o.cmmdNoReturn(str_del);
            }
        }
    }
}
