using Hegang.APP.Models;
using Hegang.APP.Models.DataBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hegang.APP.Services.DbService.Impl
{
    class Ftsjzt_live : DbServiceObject
    {
        public void save(DbObject o,ref DbServiceInput input)
        {
            // 副井提升机的状态位： TR,TW,JX,JJKC,XZ,GZ,RWTZ,状态位变化标记
            int[] f_flags = { 0, 0, 0, 0, 0, 0, 0, 0 };

            if (input.Dic["副井测试.副提升电控.提人方式"] == "True" && input.Dic["副井测试.副提升电控.开车条件"] == "True")
            {
                f_flags[0] = 1;
                f_flags[7] = 1;
            }

            if (input.Dic["副井测试.副提升电控.提物方式"] == "True" && input.Dic["副井测试.副提升电控.开车条件"] == "True")
            {
                f_flags[1] = 1;
                f_flags[7] = 1;
            }

            if (input.Dic["副井测试.副提升电控.检修方式"] == "True" && input.Dic["副井测试.副提升电控.开车条件"] == "True")
            {
                f_flags[2] = 1;
                f_flags[7] = 1;
            }

            if (input.Dic["副井测试.副提升电控.紧急开车方式"] == "True" && input.Dic["副井测试.副提升电控.开车条件"] == "True")
            {
                f_flags[3] = 1;
                f_flags[7] = 1;
            }

            if (input.Dic["副井测试.副提升电控.安全回路"] == "True" && input.Dic["副井测试.副提升电控.开车条件"] == "False")
            {
                f_flags[4] = 1;
                f_flags[7] = 1;
            }

            if (input.Dic["副井测试.副提升电控.安全回路"] == "False" && input.Dic["副井测试.副提升电控.紧停"] == "True")
            {
                f_flags[5] = 1;
                f_flags[7] = 1;
            }

            if (input.Dic["副井测试.副提升电控.安全回路"] == "False" && input.Dic["副井测试.副提升电控.紧停"] == "False")
            {
                f_flags[6] = 1;
                f_flags[7] = 1;
            }

            if (f_flags[7] == 1)
            {
                // 将提升机状态信息存入数据表 `ftsjzt_live`
                string str = string.Format("" +
                "INSERT INTO " +
                    "`ftsjzt_live` (TR,TW,JX,JJKC,XZ,GZ,RWTZ,TimeStamp) " +
                "VALUES ( '{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}');",
                f_flags[0], f_flags[1], f_flags[2], f_flags[3], f_flags[4], f_flags[5], f_flags[6], System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                o.cmmdNoReturn(str);

                #region 清空数据
                input.Dic["副井测试.副提升电控.提人方式"] = "0";
                input.Dic["副井测试.副提升电控.开车条件"] = "0";
                input.Dic["副井测试.副提升电控.提物方式"] = "0";
                input.Dic["副井测试.副提升电控.检修方式"] = "0";
                input.Dic["副井测试.副提升电控.紧急开车方式"] = "0";
                input.Dic["副井测试.副提升电控.安全回路"] = "0";
                input.Dic["副井测试.副提升电控.紧停"] = "0";
                #endregion
            }
        }
    }
}
