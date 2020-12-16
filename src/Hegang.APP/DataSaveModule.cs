using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hegang.APP
{
    class DataSaveModule
    {
        public static void savePLCData(ref Dictionary<string,string> dic,ref TimeJudgeItemList timeJudgeItemList, ref string[] gs_tmpBuf)
        {
            MySqlConnection conn = DBUtils.getDBConnection();

            // 保存主井提升机状态信息
            save_mtsjzt_live(conn, ref dic);
            // 保存副井提升机状态信息
            save_ftsjzt_live(conn, ref dic);

            // 保存主井勾数和电度信息
            if (timeJudgeItemList.MList[0].Flag)
                save_mtsjgd_live_s(conn, dic, ref gs_tmpBuf);
            // 保存副井勾数和电度信息
            if (timeJudgeItemList.FList[0].Flag)
                save_ftsjgd_live_s(conn, dic, ref gs_tmpBuf);

            // 保存主井提升机的速度、高度、功率和功率因素信息
            if (timeJudgeItemList.MList[1].Flag)
                save_mtsjsggy_live(conn, dic);
            // 保存副井提升机的速度、高度、功率和功率因素信息
            if (timeJudgeItemList.FList[1].Flag)
                save_ftsjsggy_live(conn, dic);

            // 保存主井温度信息
            if (timeJudgeItemList.MList[2].Flag)
                save_mwd(conn, dic);
            // 保存副井温度信息
            if (timeJudgeItemList.FList[2].Flag)
                save_fwd(conn, dic);
            // 保存主井振动信息
            if (timeJudgeItemList.MList[3].Flag)
                save_mzd(conn, dic);
            // 保存副井振动信息
            if (timeJudgeItemList.FList[3].Flag)
                save_fzd(conn, dic);
            // 保存主井油压信息
            if (timeJudgeItemList.MList[4].Flag)
                save_mzzyl(conn, dic);
            // 保存副井油压信息
            if (timeJudgeItemList.FList[4].Flag)
                save_fzzyl(conn, dic);

        }

        /// <summary>
        /// 保存主井温度
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="dic"></param>
        public static void save_mwd(MySqlConnection conn,Dictionary<string,string> dic)
        {
            conn.Open();
            string time = System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

            string str = string.Format("INSERT INTO `mwd` (`wd1`,`wd2`,`wd3`,`wd4`,`TimeStamp`) VALUES ( '{0}','{1}','{2}','{3}','{4}')", dic["主井测试.S7200Smart.温度1"], dic["主井测试.S7200Smart.温度2"], dic["主井测试.S7200Smart.温度3"], dic["主井测试.S7200Smart.温度4"], time);
            MySqlCommand cmd = new MySqlCommand(str, conn);
            cmd.ExecuteNonQuery();

            conn.Close();
        }

        /// <summary>
        /// 保存副井温度
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="dic"></param>
        public static void save_fwd(MySqlConnection conn, Dictionary<string, string> dic)
        {
            conn.Open();
            string time = System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

            string str = string.Format("INSERT INTO `fwd` (`wd1`,`wd2`,`wd3`,`wd4`,`TimeStamp`) VALUES ( '{0}','{1}','{2}','{3}','{4}')", dic["副井测试.S7200Smart.温度1"], dic["副井测试.S7200Smart.温度2"], dic["副井测试.S7200Smart.温度3"], dic["副井测试.S7200Smart.温度4"], time);
            MySqlCommand cmd = new MySqlCommand(str, conn);
            cmd.ExecuteNonQuery();

            conn.Close();
        }

        /// <summary>
        /// 保存主井振动
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="dic"></param>
        public static void save_mzd(MySqlConnection conn, Dictionary<string, string> dic)
        {
            conn.Open();
            string time = System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

            string str = string.Format("INSERT INTO `mzd` (`zd1`,`zd2`,`zd3`,`zd4`,`TimeStamp`) VALUES ( '{0}','{1}','{2}','{3}','{4}')", dic["主井测试.S7200Smart.振动1"], dic["主井测试.S7200Smart.振动2"], dic["主井测试.S7200Smart.振动3"], dic["主井测试.S7200Smart.振动4"], time);
            MySqlCommand cmd = new MySqlCommand(str, conn);
            cmd.ExecuteNonQuery();

            conn.Close();
        }

        /// <summary>
        /// 保存副井振动
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="dic"></param>
        public static void save_fzd(MySqlConnection conn, Dictionary<string, string> dic)
        {
            conn.Open();
            string time = System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

            string str = string.Format("INSERT INTO `fzd` (`zd1`,`zd2`,`zd3`,`zd4`,`TimeStamp`) VALUES ( '{0}','{1}','{2}','{3}','{4}')", dic["副井测试.S7200Smart.振动1"], dic["副井测试.S7200Smart.振动2"], dic["副井测试.S7200Smart.振动3"], dic["副井测试.S7200Smart.振动4"], time);
            MySqlCommand cmd = new MySqlCommand(str, conn);
            cmd.ExecuteNonQuery();

            conn.Close();
        }

        /// <summary>
        /// 保存主井油压
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="dic"></param>
        private static void save_mzzyl(MySqlConnection conn, Dictionary<string, string> dic)
        {
            conn.Open();
            string time = System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            string sqlStr = string.Format("INSERT INTO mzzyl (yl,timestamp) VALUES('{0}','{1}')", dic["主井测试.主提升电控.油压"], time);
            MySqlCommand mycmd = new MySqlCommand(sqlStr, conn);
            mycmd.ExecuteNonQuery();
            conn.Close();
        }

        /// <summary>
        /// 保存副井油压
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="dic"></param>
        private static void save_fzzyl(MySqlConnection conn, Dictionary<string, string> dic)
        {
            conn.Open();
            string time = System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            string sqlStr = string.Format("INSERT INTO fzzyl (yl,timestamp) VALUES('{0}','{1}')", dic["副井测试.副提升电控.油压"], time);
            MySqlCommand mycmd = new MySqlCommand(sqlStr, conn);
            mycmd.ExecuteNonQuery();
            conn.Close();
        }

        /// <summary>
        /// 保存主提升机状态信息
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="dic"></param>
        private static void save_mtsjzt_live(MySqlConnection conn, ref Dictionary<string, string> dic)
        {
            // 主井提升机的状态位：TM,YS,JX,JJKC,XZ,GZ,RWTZ,状态位变化标记
            int[] m_flags = { 0, 0, 0, 0, 0, 0, 0, 0 };

            if (dic["主井测试.主提升电控.提煤方式"] == "True" && dic["主井测试.主提升电控.开车信号"] == "True")
            {
                m_flags[0] = 1;
                m_flags[7] = 1;
            }

            if (dic["主井测试.主提升电控.开车信号"] == "True" && dic["主井测试.主提升电控.验绳方式"] == "True")
            {
                m_flags[1] = 1;
                m_flags[7] = 1;
            }

            if (dic["主井测试.主提升电控.检修方式"] == "True" && dic["主井测试.主提升电控.开车信号"] == "True")
            {
                m_flags[2] = 1;
                m_flags[7] = 1;
            }

            if (dic["主井测试.主提升电控.紧急开车方式"] == "True" && dic["主井测试.主提升电控.开车信号"] == "True")
            {
                m_flags[3] = 1;
                m_flags[7] = 1;
            }

            if (dic["主井测试.主提升电控.开车信号"] == "False" && dic["主井测试.主提升电控.安全回路"] == "True")
            {
                m_flags[4] = 1;
                m_flags[7] = 1;
            }

            if (dic["主井测试.主提升电控.安全回路"] == "False" && dic["主井测试.主提升电控.紧停"] == "True")
            {
                m_flags[5] = 1;
                m_flags[7] = 1;
            }

            if (dic["主井测试.主提升电控.安全回路"] == "False" && dic["主井测试.主提升电控.紧停"] == "False")
            {
                m_flags[6] = 1;
                m_flags[7] = 1;
            }

            if (m_flags[7] == 1)
            {
                // 存入数据表 `mtsjzt_live`
                conn.Open();
                string time = System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                string str = string.Format("" +
                "INSERT INTO " +
                    "`mtsjzt_live` (TM,YS,JX,JJKC,XZ,GZ,RWTZ,TimeStamp) " +
                "VALUES ( '{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}');",
                m_flags[0], m_flags[1], m_flags[2], m_flags[3], m_flags[4], m_flags[5], m_flags[6], time);
                MySqlCommand cmd = new MySqlCommand(str, conn);
                cmd.ExecuteNonQuery();
                conn.Close();

                // 清空数据
                dic["主井测试.主提升电控.开车信号"] = "0";
                dic["主井测试.主提升电控.提煤方式"] = "0";
                dic["主井测试.主提升电控.检修方式"] = "0";
                dic["主井测试.主提升电控.验绳方式"] = "0";
                dic["主井测试.主提升电控.紧急开车方式"] = "0";
                dic["主井测试.主提升电控.安全回路"] = "0";
                dic["主井测试.主提升电控.紧停"] = "0";
            }
        }

        /// <summary>
        /// 保存副提升机状态信息
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="dataBean"></param>
        /// <returns></returns>
        private static void save_ftsjzt_live(MySqlConnection conn, ref Dictionary<string, string> dic)
        {
            // 副井提升机的状态位： TR,TW,JX,JJKC,XZ,GZ,RWTZ,状态位变化标记
            int[] f_flags = { 0, 0, 0, 0, 0, 0, 0, 0 };

            if (dic["副井测试.副提升电控.提人方式"] == "True" && dic["副井测试.副提升电控.开车条件"] == "True")
            {
                f_flags[0] = 1;
                f_flags[7] = 1;
            }

            if (dic["副井测试.副提升电控.提物方式"] == "True" && dic["副井测试.副提升电控.开车条件"] == "True")
            {
                f_flags[1] = 1;
                f_flags[7] = 1;
            }

            if (dic["副井测试.副提升电控.检修方式"] == "True" && dic["副井测试.副提升电控.开车条件"] == "True")
            {
                f_flags[2] = 1;
                f_flags[7] = 1;
            }

            if (dic["副井测试.副提升电控.紧急开车方式"] == "True" && dic["副井测试.副提升电控.开车条件"] == "True")
            {
                f_flags[3] = 1;
                f_flags[7] = 1;
            }

            if (dic["副井测试.副提升电控.安全回路"] == "True" && dic["副井测试.副提升电控.开车条件"] == "False")
            {
                f_flags[4] = 1;
                f_flags[7] = 1;
            }

            if (dic["副井测试.副提升电控.安全回路"] == "False" && dic["副井测试.副提升电控.紧停"] == "True")
            {
                f_flags[5] = 1;
                f_flags[7] = 1;
            }

            if (dic["副井测试.副提升电控.安全回路"] == "False" && dic["副井测试.副提升电控.紧停"] == "False")
            {
                f_flags[6] = 1;
                f_flags[7] = 1;
            }

            if (f_flags[7] == 1)
            {
                // 存入数据表 `ftsjzt_live`
                conn.Open();
                string time = System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                string str = string.Format("" +
                "INSERT INTO " +
                    "`ftsjzt_live` (TR,TW,JX,JJKC,XZ,GZ,RWTZ,TimeStamp) " +
                "VALUES ( '{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}');",
                f_flags[0], f_flags[1], f_flags[2], f_flags[3], f_flags[4], f_flags[5], f_flags[6], time);
                MySqlCommand cmd = new MySqlCommand(str, conn);
                cmd.ExecuteNonQuery();
                conn.Close();

                // 清空数据
                dic["副井测试.副提升电控.提人方式"] = "0";
                dic["副井测试.副提升电控.开车条件"] = "0";
                dic["副井测试.副提升电控.提物方式"] = "0";
                dic["副井测试.副提升电控.检修方式"] = "0";
                dic["副井测试.副提升电控.紧急开车方式"] = "0";
                dic["副井测试.副提升电控.安全回路"] = "0";
                dic["副井测试.副提升电控.紧停"] = "0";
            }
        }

        /// <summary>
        /// 保存副井提升机的速度、高度、功率和功率因素信息
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="dataBean"></param>
        private static void save_ftsjsggy_live(MySqlConnection conn, Dictionary<string, string> dic)
        {
            conn.Open();
            string time = System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            string str = "SELECT COUNT(id) FROM `ftsjsggy_live`";
            MySqlCommand cmd = new MySqlCommand(str, conn);
            int row_num = Convert.ToInt32(cmd.ExecuteScalar());

            if (row_num == 10)
            {
                str = "DELETE FROM ftsjsggy_live WHERE id=(SELECT MIN(id) FROM `ftsjsggy_live`)";
                cmd = new MySqlCommand(str, conn);
                cmd.ExecuteNonQuery();

                str = string.Format("INSERT INTO `ftsjsggy_live` (`SD`,`GD`,`GL`,`GLYS`,`TimeStamp`) VALUES ( '{0}','{1}','{2}','{3}','{4}')", dic["副井测试.副提升电控.速度"], dic["副井测试.副提升电控.高度"], dic["副井测试.S7200Smart.功率"], dic["副井测试.S7200Smart.功率因素"], time);
                cmd = new MySqlCommand(str, conn);
                cmd.ExecuteNonQuery();
            }
            else if (row_num < 10)
            {
                str = string.Format("INSERT INTO `ftsjsggy_live` (`SD`,`GD`,`GL`,`GLYS`,`TimeStamp`) VALUES ( '{0}','{1}','{2}','{3}','{4}')", dic["副井测试.副提升电控.速度"], dic["副井测试.副提升电控.高度"], dic["副井测试.S7200Smart.功率"], dic["副井测试.S7200Smart.功率因素"], time);
                cmd = new MySqlCommand(str, conn);
                cmd.ExecuteNonQuery();
            }
            else
            {
                for (int i = 0; i < row_num - 10; i++)
                {
                    str = "DELETE FROM ftsjsggy_live WHERE id=(SELECT MIN(id) FROM `ftsjsggy_live`)";
                    cmd = new MySqlCommand(str, conn);
                    cmd.ExecuteNonQuery();
                }
            }

            conn.Close();
        }

        /// <summary>
        /// 保存主井提升机的速度、高度、功率和功率因素信息
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="dataBean"></param>
        private static void save_mtsjsggy_live(MySqlConnection conn, Dictionary<string, string> dic)
        {
            conn.Open();
            string time = System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            string str = "SELECT COUNT(id) FROM `mtsjsggy_live`";
            MySqlCommand cmd = new MySqlCommand(str, conn);
            int row_num = Convert.ToInt32(cmd.ExecuteScalar());

            if (row_num == 10)
            {
                str = "DELETE FROM mtsjsggy_live WHERE id=(SELECT MIN(id) FROM `mtsjsggy_live`)";
                cmd = new MySqlCommand(str, conn);
                cmd.ExecuteNonQuery();

                str = string.Format("INSERT INTO `mtsjsggy_live` (`SD`,`GD`,`GL`,`GLYS`,`TimeStamp`) VALUES ( '{0}','{1}','{2}','{3}','{4}')", dic["主井测试.主提升电控.速度"], dic["主井测试.主提升电控.高度"], dic["主井测试.S7200Smart.功率"], dic["主井测试.S7200Smart.功率因素"], time);
                cmd = new MySqlCommand(str, conn);
                cmd.ExecuteNonQuery();
            }
            else if (row_num < 10)
            {
                str = string.Format("INSERT INTO `mtsjsggy_live` (`SD`,`GD`,`GL`,`GLYS`,`TimeStamp`) VALUES ( '{0}','{1}','{2}','{3}','{4}')", dic["主井测试.主提升电控.速度"], dic["主井测试.主提升电控.高度"], dic["主井测试.S7200Smart.功率"], dic["主井测试.S7200Smart.功率因素"], time);
                cmd = new MySqlCommand(str, conn);
                cmd.ExecuteNonQuery();
            }
            else
            {
                for (int i = 0; i < row_num - 10; i++)
                {
                    str = "DELETE FROM mtsjsggy_live WHERE id=(SELECT MIN(id) FROM `mtsjsggy_live`)";
                    cmd = new MySqlCommand(str, conn);
                    cmd.ExecuteNonQuery();
                }
            }

            conn.Close();
        }

        /// <summary>
        /// 保存主提升机勾数和电度
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="dic"></param>
        /// <param name="gs_tmpBuf"></param>
        /// <returns></returns>
        public static void save_mtsjgd_live_s(MySqlConnection conn, Dictionary<string, string> dic, ref string[] gs_tmpBuf)
        {
            if (gs_tmpBuf[0] != "-1")
            {
                conn.Open();
                string time = System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                string GS = (int.Parse(dic["主井测试.主提升电控.勾数"]) - int.Parse(gs_tmpBuf[0])).ToString();
                String str = string.Format("INSERT INTO `mtsjgd_live` (`GS`,`DD`,`TimeStamp`) VALUES ( '{0}','{1}','{2}')", GS, dic["主井测试.S7200Smart.电度"], time);
                MySqlCommand cmd = new MySqlCommand(str, conn);
                cmd.ExecuteNonQuery();
                conn.Close();
            }
            gs_tmpBuf[0] = dic["主井测试.主提升电控.勾数"];
        }

        /// <summary>
        /// 保存副提升机勾数和电度
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="dic"></param>
        /// <param name="gs_tmpBuf"></param>
        /// <returns></returns>
        private static void save_ftsjgd_live_s(MySqlConnection conn, Dictionary<string, string> dic, ref string[] gs_tmpBuf)
        {
            if (gs_tmpBuf[1] != "-1")
            {
                conn.Open();
                string time = System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                string GS = (int.Parse(dic["副井测试.副提升电控.勾数"]) - int.Parse(gs_tmpBuf[1])).ToString();
                String str = string.Format("INSERT INTO `ftsjgd_live` (`GS`,`DD`,`TimeStamp`) VALUES ( '{0}','{1}','{2}')", GS, dic["副井测试.S7200Smart.电度"], time);
                MySqlCommand cmd = new MySqlCommand(str, conn);
                cmd.ExecuteNonQuery();
                conn.Close();
            }
            gs_tmpBuf[1] = dic["副井测试.副提升电控.勾数"];
        }

        /// <summary>
        /// 保存故障信息
        /// </summary>
        /// <param name="deviceName">设备名称</param>
        /// <param name="ItemName">PLC数据点名称</param>
        public static void save_gz(string deviceName, string ItemName)
        {
            MySqlConnection conn = DBUtils.getDBConnection();
            conn.Open();
            string time = System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            string sqlStr = string.Format("INSERT INTO gz (channel,gzname,timestamp) VALUES('{0}','{1}','{2}')", deviceName, ItemName, time);
            MySqlCommand mycmd = new MySqlCommand(sqlStr, conn);
            mycmd.ExecuteNonQuery();
            conn.Close();
        }
    }
}