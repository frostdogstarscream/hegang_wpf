using Hegang.APP.Models;
using Hegang.APP.Models.DataBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hegang.APP.Services.DbService.Impl
{
    class Mtsjzt_live : DbServiceObject
    {
        public void save(DbObject o,ref DbServiceInput input)
        {
            // 主井提升机的状态位：TM,YS,JX,JJKC,XZ,GZ,RWTZ,状态位变化标记
            int[] m_flags = { 0, 0, 0, 0, 0, 0, 0, 0 };

            if (input.Dic["主井测试.主提升电控.提煤方式"] == "True" && input.Dic["主井测试.主提升电控.开车信号"] == "True")
            {
                m_flags[0] = 1;
                m_flags[7] = 1;
            }

            if (input.Dic["主井测试.主提升电控.开车信号"] == "True" && input.Dic["主井测试.主提升电控.验绳方式"] == "True")
            {
                m_flags[1] = 1;
                m_flags[7] = 1;
            }

            if (input.Dic["主井测试.主提升电控.检修方式"] == "True" && input.Dic["主井测试.主提升电控.开车信号"] == "True")
            {
                m_flags[2] = 1;
                m_flags[7] = 1;
            }

            if (input.Dic["主井测试.主提升电控.紧急开车方式"] == "True" && input.Dic["主井测试.主提升电控.开车信号"] == "True")
            {
                m_flags[3] = 1;
                m_flags[7] = 1;
            }

            if (input.Dic["主井测试.主提升电控.开车信号"] == "False" && input.Dic["主井测试.主提升电控.安全回路"] == "True")
            {
                m_flags[4] = 1;
                m_flags[7] = 1;
            }

            if (input.Dic["主井测试.主提升电控.安全回路"] == "False" && input.Dic["主井测试.主提升电控.紧停"] == "True")
            {
                m_flags[5] = 1;
                m_flags[7] = 1;
            }

            if (input.Dic["主井测试.主提升电控.安全回路"] == "False" && input.Dic["主井测试.主提升电控.紧停"] == "False")
            {
                m_flags[6] = 1;
                m_flags[7] = 1;
            }

            if (m_flags[7] == 1)
            {
                // 将提升机状态信息存入数据表 `mtsjzt_live`
                string str = string.Format("" +
                "INSERT INTO " +
                    "`mtsjzt_live` (TM,YS,JX,JJKC,XZ,GZ,RWTZ,TimeStamp) " +
                "VALUES ( '{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}');",
                m_flags[0], m_flags[1], m_flags[2], m_flags[3], m_flags[4], m_flags[5], m_flags[6], System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));

                o.cmmdNoReturn(str);

                #region 清空数据
                input.Dic["主井测试.主提升电控.开车信号"] = "0";
                input.Dic["主井测试.主提升电控.提煤方式"] = "0";
                input.Dic["主井测试.主提升电控.检修方式"] = "0";
                input.Dic["主井测试.主提升电控.验绳方式"] = "0";
                input.Dic["主井测试.主提升电控.紧急开车方式"] = "0";
                input.Dic["主井测试.主提升电控.安全回路"] = "0";
                input.Dic["主井测试.主提升电控.紧停"] = "0";
                #endregion
            }
        }
    }
}
