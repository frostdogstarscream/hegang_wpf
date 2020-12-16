using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Hegang.APP
{
    class FixedTimeTaskModule
    {
        /// <summary>
        /// 设置每天 00:00 对数据表 mtsjzt_live、ftsjzt_live、mtsjgd_live、ftsjgd_live 进行统计
        /// 参考：https://blog.csdn.net/zhouyingge1104/article/details/94211503
        /// </summary>
        public static void setTaskAtFixedTime_day()
        {
            DateTime now = DateTime.Now;
            //设置任务启动时间
            double hour = 00.0;     //小时   
            double minute = 00.0;   //分钟
            DateTime startTime = DateTime.Today.AddHours(hour).AddMinutes(minute);
            if (now > startTime)
            {
                startTime = startTime.AddDays(1.0);
            }
            int delay = (int)((startTime - now).TotalMilliseconds);
            var t = new System.Threading.Timer(handle_event_day);
            //设置线程参数 任务delay毫秒后启动
            t.Change(delay, Timeout.Infinite);
        }

        /// <summary>
        /// 每隔一小时统计一次温度、油压
        /// </summary>
        public static void setTaskAtFixedTime_hour()
        {
            DateTime now = DateTime.Now;
            //设置任务启动时间  
            double hour = Convert.ToDouble(DateTime.Now.Hour + 1);
            double minute = 0.0;   //分钟

            DateTime startTime = DateTime.Today.AddHours(hour).AddMinutes(minute);

            int delay = (int)((startTime - now).TotalMilliseconds);
            var t = new System.Threading.Timer(handle_event_hour);
            //设置线程参数 任务delay毫秒后启动
            t.Change(delay, Timeout.Infinite);
        }

        /// <summary>
        /// 每隔一分钟预测一次温度、振动
        /// </summary>
        public static void setTaskAtFixedTime_minute()
        {
            DateTime now = DateTime.Now;
            //设置任务启动时间  
            double hour = Convert.ToDouble(DateTime.Now.Hour);
            double minute = Convert.ToDouble(DateTime.Now.Minute + 1);
            double second = Convert.ToDouble(DateTime.Now.Second);

            DateTime startTime = DateTime.Today.AddHours(hour).AddMinutes(minute).AddSeconds(second);

            int delay = (int)((startTime - now).TotalMilliseconds);
            var t = new System.Threading.Timer(handle_event_minute);
            //设置线程参数 任务delay毫秒后启动
            t.Change(delay, Timeout.Infinite);
        }

        /// <summary>
        /// setTaskAtFixedTime_day 执行的任务
        /// </summary>
        /// <param name="state"></param>
        private static void handle_event_day(object state)
        {
            // 执行任务
            task1();
            task2();
            task3();

            // 再次设定任务执行时间
            setTaskAtFixedTime_day();
        }

        /// <summary>
        /// setTaskAtFixedTime_hour 执行的任务
        /// </summary>
        /// <param name="state"></param>
        private static void handle_event_hour(object state)
        {
            // 执行任务
            task4();
            task5();
            task6();
            task7();
            task8();
            task9();
            // 再次设定任务执行时间
            setTaskAtFixedTime_hour();
        }

        /// <summary>
        /// setTaskAtFixedTime_minute 执行的任务
        /// </summary>
        /// <param name="state"></param>
        private static void handle_event_minute(object state)
        {
            // 执行任务
            task10();
            task11();
            task12();
            task13();
            // 再次设定任务执行时间
            setTaskAtFixedTime_minute();
        }

        /// <summary>
        /// 统计主井和副井的提升机状态表
        /// </summary>
        private static void task1()
        {
            string mtsjgd_live_SQL = string.Format("" +
            "CREATE TABLE mtsjzt_live_tmp LIKE mtsjzt_live;" +
            "INSERT INTO mtsjzt_live_tmp SELECT * FROM mtsjzt_live;" +
            "INSERT INTO " +
                "mtsjtj(TM,YS,JX,JJKC,XZ,GZ,RWTZ,TimeStamp)" +
            "VALUES(" +
                "(SELECT COUNT(id) FROM mtsjzt_live_tmp WHERE TM = '1')," +
                "(SELECT COUNT(id) FROM mtsjzt_live_tmp WHERE YS = '1')," +
                "(SELECT COUNT(id) FROM mtsjzt_live_tmp WHERE JX = '1')," +
                "(SELECT COUNT(id) FROM mtsjzt_live_tmp WHERE JJKC = '1')," +
                "(SELECT COUNT(id) FROM mtsjzt_live_tmp WHERE XZ = '1')," +
                "(SELECT COUNT(id) FROM mtsjzt_live_tmp WHERE GZ = '1')," +
                "(SELECT COUNT(id) FROM mtsjzt_live_tmp WHERE RWTZ = '1')," +
                "'{0}'" +
            ");" +
            "DROP TABLE mtsjzt_live_tmp ;" +
            "TRUNCATE mtsjzt_live;", System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));

            string ftsjgd_live_SQL = string.Format("" +
            "CREATE TABLE ftsjzt_live_tmp LIKE ftsjzt_live;" +
            "INSERT INTO ftsjzt_live_tmp SELECT * FROM ftsjzt_live;" +
            "INSERT INTO " +
                "ftsjtj(TR,TW,JX,JJKC,XZ,GZ,RWTZ,TimeStamp)" +
            "VALUES(" +
                "(SELECT COUNT(id) FROM ftsjzt_live_tmp WHERE TR = '1')," +
                "(SELECT COUNT(id) FROM ftsjzt_live_tmp WHERE TW = '1')," +
                "(SELECT COUNT(id) FROM ftsjzt_live_tmp WHERE JX = '1')," +
                "(SELECT COUNT(id) FROM ftsjzt_live_tmp WHERE JJKC = '1')," +
                "(SELECT COUNT(id) FROM ftsjzt_live_tmp WHERE XZ = '1')," +
                "(SELECT COUNT(id) FROM ftsjzt_live_tmp WHERE GZ = '1')," +
                "(SELECT COUNT(id) FROM ftsjzt_live_tmp WHERE RWTZ = '1')," +
                "'{0}'" +
            ");" +
            "DROP TABLE ftsjzt_live_tmp ;" +
            "TRUNCATE ftsjzt_live;", System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));


            MySqlConnection conn = DBUtils.getDBConnection();
            MySqlCommand comm = conn.CreateCommand();
            conn.Open();
            comm.CommandText = mtsjgd_live_SQL;
            comm.ExecuteNonQuery();
            comm.CommandText = ftsjgd_live_SQL;
            comm.ExecuteNonQuery();
            conn.Close();
        }

        /// <summary>
        /// 统计主井和副井的提升机状态表
        /// </summary>
        private static void task1_2()
        {
            string mtsjgd_live_SQL = string.Format("" +
            "CREATE TABLE mtsjzt_live_s_tmp LIKE mtsjzt_live_s;" +
            "INSERT INTO mtsjzt_live_s_tmp SELECT * FROM mtsjzt_live_s;" +
            "INSERT INTO " +
                "mtsjtj(TM,YS,JX,JJKC,XZ,GZ,RWTZ,TimeStamp)" +
            "VALUES(" +
                "(SELECT COUNT(id) FROM mtsjzt_live_s_tmp WHERE itemName = 'TM')," +
                "(SELECT COUNT(id) FROM mtsjzt_live_s_tmp WHERE itemName = 'YS')," +
                "(SELECT COUNT(id) FROM mtsjzt_live_s_tmp WHERE itemName = 'JX')," +
                "(SELECT COUNT(id) FROM mtsjzt_live_s_tmp WHERE itemName = 'JJKC')," +
                "(SELECT COUNT(id) FROM mtsjzt_live_s_tmp WHERE itemName = 'XZ')," +
                "(SELECT COUNT(id) FROM mtsjzt_live_s_tmp WHERE itemName = 'GZ')," +
                "(SELECT COUNT(id) FROM mtsjzt_live_s_tmp WHERE itemName = 'RWTZ')," +
                "'{0}'" +
            ");" +
            "DROP TABLE mtsjzt_live_s_tmp ;" +
            "TRUNCATE mtsjzt_live_s;", System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));

            string ftsjgd_live_SQL = string.Format("" +
            "CREATE TABLE ftsjzt_live_s_tmp LIKE ftsjzt_live_s;" +
            "INSERT INTO ftsjzt_live_s_tmp SELECT * FROM ftsjzt_live_s;" +
            "INSERT INTO " +
                "ftsjtj(TR,TW,JX,JJKC,XZ,GZ,RWTZ,TimeStamp)" +
            "VALUES(" +
                "(SELECT COUNT(id) FROM ftsjzt_live_s_tmp WHERE itemName = 'TR')," +
                "(SELECT COUNT(id) FROM ftsjzt_live_s_tmp WHERE itemName = 'TW')," +
                "(SELECT COUNT(id) FROM ftsjzt_live_s_tmp WHERE itemName = 'JX')," +
                "(SELECT COUNT(id) FROM ftsjzt_live_s_tmp WHERE itemName = 'JJKC')," +
                "(SELECT COUNT(id) FROM ftsjzt_live_s_tmp WHERE itemName = 'XZ')," +
                "(SELECT COUNT(id) FROM ftsjzt_live_s_tmp WHERE itemName = 'GZ')," +
                "(SELECT COUNT(id) FROM ftsjzt_live_s_tmp WHERE itemName = 'RWTZ')," +
                "'{0}'" +
            ");" +
            "DROP TABLE ftsjzt_live_s_tmp ;" +
            "TRUNCATE ftsjzt_live_s;", System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));


            MySqlConnection conn = DBUtils.getDBConnection();
            MySqlCommand comm = conn.CreateCommand();
            conn.Open();
            comm.CommandText = mtsjgd_live_SQL;
            comm.ExecuteNonQuery();
            comm.CommandText = ftsjgd_live_SQL;
            comm.ExecuteNonQuery();
            conn.Close();
        }

        /// <summary>
        /// 每天统计主井的勾数
        /// </summary>
        public static void task2()
        {
            MySqlConnection conn = DBUtils.getDBConnection();
            MySqlCommand comm = conn.CreateCommand();
            conn.Open();

            string str1 = "SELECT GS FROM mtsjgd_live";
            comm.CommandText = str1;
            MySqlDataReader reader = comm.ExecuteReader();
            int GS_sum = 0;
            while (reader.Read())
            {
                GS_sum += reader.GetInt32(0);
            }
            reader.Close();

            string str2 = string.Format("" +
                "INSERT INTO " +
                    "mtsjgdtj(GS,TimeStamp) " +
                "VALUES" +
                    "('{0}','{1}');", GS_sum, System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            comm.CommandText = str2;
            comm.ExecuteNonQuery();

            //统计完后，清空mtsjgd_live
            string str3 = "TRUNCATE mtsjgd_live;";
            comm.CommandText = str3;
            comm.ExecuteNonQuery();

            conn.Close();
        }

        /// <summary>
        /// 每天统计副井的勾数
        /// </summary>
        public static void task3()
        {
            MySqlConnection conn = DBUtils.getDBConnection();
            MySqlCommand comm = conn.CreateCommand();
            conn.Open();

            string str1 = "SELECT GS FROM ftsjgd_live";
            comm.CommandText = str1;
            MySqlDataReader reader = comm.ExecuteReader();
            int GS_sum = 0;
            while (reader.Read())
            {
                GS_sum += reader.GetInt32(0);
            }
            reader.Close();

            string str2 = string.Format("" +
                "INSERT INTO " +
                    "ftsjgdtj(GS,TimeStamp) " +
                "VALUES" +
                    "('{0}','{1}');", GS_sum, System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            comm.CommandText = str2;
            comm.ExecuteNonQuery();

            //统计完后，清空ftsjgd_live
            string str3 = "TRUNCATE ftsjgd_live;";
            comm.CommandText = str3;
            comm.ExecuteNonQuery();

            conn.Close();
        }

        /// <summary>
        /// 每小时统计副井每个传感器的平均温度
        /// </summary>
        public static void task4()
        {
            MySqlConnection conn = DBUtils.getDBConnection();
            conn.Open();
            MySqlCommand comm = conn.CreateCommand();
            string str = "SELECT AVG(wd1),AVG(wd2),AVG(wd3),AVG(wd4) FROM fwd;";
            comm.CommandText = str;
            MySqlDataReader reader = comm.ExecuteReader();

            string timestamp = System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

            if (reader.Read())
            {
                comm.CommandText = string.Format("" +
                "INSERT INTO fwd_h(wd1, wd2, wd3, wd4, timestamp) VALUES('{0}','{1}','{2}','{3}','{4}');" +
                "TRUNCATE fwd;" +
                "", reader[0], reader[1], reader[2], reader[3], timestamp);
                reader.Close();
                comm.ExecuteNonQuery();
            }
            else
            {
                reader.Close();
            }

            conn.Close();
        }
        /// <summary>
        /// 每小时统计主井每个传感器的平均温度
        /// </summary>
        public static void task5()
        {
            MySqlConnection conn = DBUtils.getDBConnection();
            conn.Open();
            MySqlCommand comm = conn.CreateCommand();
            string str = "SELECT AVG(wd1),AVG(wd2),AVG(wd3),AVG(wd4) FROM mwd;";
            comm.CommandText = str;
            MySqlDataReader reader = comm.ExecuteReader();

            string timestamp = System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

            if (reader.Read())
            {
                comm.CommandText = string.Format("" +
                "INSERT INTO mwd_h(wd1, wd2, wd3, wd4, timestamp) VALUES('{0}','{1}','{2}','{3}','{4}');" +
                "TRUNCATE mwd;" +
                "", reader[0], reader[1], reader[2], reader[3], timestamp);
                reader.Close();
                comm.ExecuteNonQuery();
            }
            else
            {
                reader.Close();
            }

            conn.Close();
        }

        /// <summary>
        /// 每小时统计主井的平均残压和平均正常压力
        /// </summary>
        public static void task6()
        {
            MySqlConnection conn = DBUtils.getDBConnection();
            conn.Open();
            MySqlCommand comm = conn.CreateCommand();
            MySqlDataReader reader;

            comm.CommandText = "SELECT AVG(yl) FROM mzzyl WHERE yl < 1;";
            reader = comm.ExecuteReader();
            string residual_pressure = null;
            if (reader.Read())
                residual_pressure = reader[0].ToString();
            if ( null==residual_pressure || ""==residual_pressure )
                residual_pressure = "0";
            reader.Close();

            comm.CommandText = "SELECT AVG(yl) FROM mzzyl WHERE yl > 11;";
            reader = comm.ExecuteReader();
            string normal_pressure = "0";
            if (reader.Read())
                normal_pressure = reader[0].ToString();
            if ( null==normal_pressure || ""==normal_pressure )
                normal_pressure = "0";
            reader.Close();


            string timestamp = System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            comm.CommandText = string.Format("INSERT INTO mzzyl_h(residual_pressure, normal_pressure, timestamp) VALUES('{0}','{1}','{2}')", residual_pressure, normal_pressure, timestamp);
            comm.ExecuteNonQuery();

            comm.CommandText = "TRUNCATE mzzyl;";
            comm.ExecuteNonQuery();

            conn.Close();
        }
        /// <summary>
        /// 每小时统计副井的平均残压和平均正常压力
        /// </summary>
        public static void task7()
        {
            MySqlConnection conn = DBUtils.getDBConnection();
            conn.Open();
            MySqlCommand comm = conn.CreateCommand();
            MySqlDataReader reader;

            comm.CommandText = "SELECT AVG(yl) FROM fzzyl WHERE yl < 1;";
            reader = comm.ExecuteReader();
            string residual_pressure = "0";
            if (reader.Read())
                residual_pressure = reader[0].ToString();
            if (null == residual_pressure || "" == residual_pressure)
                residual_pressure = "0";
            reader.Close();

            comm.CommandText = "SELECT AVG(yl) FROM fzzyl WHERE yl > 11;";
            reader = comm.ExecuteReader();
            string normal_pressure = "0";
            if (reader.Read())
                normal_pressure = reader[0].ToString();
            if (null == normal_pressure || "" == normal_pressure)
                normal_pressure = "0";
            reader.Close();

            string timestamp = System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            comm.CommandText = string.Format("INSERT INTO fzzyl_h(residual_pressure, normal_pressure, timestamp) VALUES('{0}','{1}','{2}')", residual_pressure, normal_pressure, timestamp);
            comm.ExecuteNonQuery();

            comm.CommandText = "TRUNCATE fzzyl;";
            comm.ExecuteNonQuery();

            conn.Close();
        }

        /// <summary>
        /// 统计主井振动
        /// </summary>
        public static void task8()
        {
            MySqlConnection conn = DBUtils.getDBConnection();
            conn.Open();
            MySqlCommand comm = conn.CreateCommand();
            string str = "SELECT AVG(zd1),AVG(zd2),AVG(zd3),AVG(zd4) FROM mzd;";
            comm.CommandText = str;
            MySqlDataReader reader = comm.ExecuteReader();

            string timestamp = System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

            if (reader.Read())
            {
                comm.CommandText = string.Format("" +
                "INSERT INTO mzd_h(zd1, zd2, zd3, zd4, timestamp) VALUES('{0}','{1}','{2}','{3}','{4}');" +
                "TRUNCATE mzd;" +
                "", reader[0], reader[1], reader[2], reader[3], timestamp);
                reader.Close();
                comm.ExecuteNonQuery();
            }
            else
            {
                reader.Close();
            }

            conn.Close();
        }
        /// <summary>
        /// 统计副井振动
        /// </summary>
        public static void task9()
        {
            MySqlConnection conn = DBUtils.getDBConnection();
            conn.Open();
            MySqlCommand comm = conn.CreateCommand();
            string str = "SELECT AVG(zd1),AVG(zd2),AVG(zd3),AVG(zd4) FROM fzd;";
            comm.CommandText = str;
            MySqlDataReader reader = comm.ExecuteReader();

            string timestamp = System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

            if (reader.Read())
            {
                comm.CommandText = string.Format("" +
                "INSERT INTO fzd_h(zd1, zd2, zd3, zd4, timestamp) VALUES('{0}','{1}','{2}','{3}','{4}');" +
                "TRUNCATE fzd;" +
                "", reader[0], reader[1], reader[2], reader[3], timestamp);
                reader.Close();
                comm.ExecuteNonQuery();
            }
            else
            {
                reader.Close();
            }

            conn.Close();
        }

        /// <summary>
        /// 预测副井温度
        /// </summary>
        public static void task10()
        {
            MySqlConnection conn = DBUtils.getDBConnection();
            conn.Open();
            MySqlCommand comm = conn.CreateCommand();
            string str = "SELECT AVG(wd1),AVG(wd2),AVG(wd3),AVG(wd4) FROM fwd;";
            comm.CommandText = str;
            MySqlDataReader reader = comm.ExecuteReader();

            string timestamp = System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

            if (reader.Read())
            {
                comm.CommandText = string.Format("" +
                "INSERT INTO fwd_forecast(wd1, wd2, wd3, wd4, timestamp) VALUES('{0}','{1}','{2}','{3}','{4}');",
                reader[0], reader[1], reader[2], reader[3], timestamp);
                reader.Close();
                comm.ExecuteNonQuery();
            }
            else
            {
                reader.Close();
            }

            conn.Close();
        }

        /// <summary>
        /// 预测主井温度
        /// </summary>
        public static void task11()
        {
            MySqlConnection conn = DBUtils.getDBConnection();
            conn.Open();
            MySqlCommand comm = conn.CreateCommand();
            string str = "SELECT AVG(wd1),AVG(wd2),AVG(wd3),AVG(wd4) FROM mwd;";
            comm.CommandText = str;
            MySqlDataReader reader = comm.ExecuteReader();

            string timestamp = System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

            if (reader.Read())
            {
                comm.CommandText = string.Format("" +
                "INSERT INTO mwd_forecast(wd1, wd2, wd3, wd4, timestamp) VALUES('{0}','{1}','{2}','{3}','{4}');",
                reader[0], reader[1], reader[2], reader[3], timestamp);
                reader.Close();
                comm.ExecuteNonQuery();
            }
            else
            {
                reader.Close();
            }

            conn.Close();
        }

        /// <summary>
        /// 预测副井振动
        /// </summary>
        public static void task12()
        {
            MySqlConnection conn = DBUtils.getDBConnection();
            conn.Open();
            MySqlCommand comm = conn.CreateCommand();
            string str = "SELECT AVG(zd1),AVG(zd2),AVG(zd3),AVG(zd4) FROM fzd;";
            comm.CommandText = str;
            MySqlDataReader reader = comm.ExecuteReader();

            string timestamp = System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

            if (reader.Read())
            {
                comm.CommandText = string.Format("" +
                "INSERT INTO fzd_forecast(zd1, zd2, zd3, zd4, timestamp) VALUES('{0}','{1}','{2}','{3}','{4}');",
                reader[0], reader[1], reader[2], reader[3], timestamp);
                reader.Close();
                comm.ExecuteNonQuery();
            }
            else
            {
                reader.Close();
            }

            conn.Close();
        }

        /// <summary>
        /// 预测主井振动
        /// </summary>
        public static void task13()
        {
            /*Dictionary<string, string> dic = new Dictionary<string, string>();
            dic.Add("sqlStr", "SELECT zdl FROM mzd;");
            string zd1_result = HttpUtils.sendPost("http://localhost:8888/forecast-http/request_API", dic);

            dic = new Dictionary<string, string>();
            dic.Add("sqlStr", "SELECT zd2 FROM mzd;");
            string zd2_result = HttpUtils.sendPost("http://localhost:8888/forecast-http/request_API", dic);

            dic = new Dictionary<string, string>();
            dic.Add("sqlStr", "SELECT zd3 FROM mzd;");
            string zd3_result = HttpUtils.sendPost("http://localhost:8888/forecast-http/request_API", dic);

            dic = new Dictionary<string, string>();
            dic.Add("sqlStr", "SELECT zd4 FROM mzd;");
            string zd4_result = HttpUtils.sendPost("http://localhost:8888/forecast-http/request_API", dic);

            string timestamp = System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            MySqlConnection conn = mysqlcn.DBConnection();
            conn.Open();
            MySqlCommand comm = conn.CreateCommand();
            string str = string.Format("INSERT INTO mzd_statistics(zd1, zd2, zd3, zd4, timestamp) VALUES('{0}','{1}','{2}','{3}','{4}');",
                zd1_result, zd2_result, zd3_result, zd4_result, timestamp);
            comm.CommandText = str;*/

            MySqlConnection conn = DBUtils.getDBConnection();
            conn.Open();
            MySqlCommand comm = conn.CreateCommand();
            string str = "SELECT AVG(zd1),AVG(zd2),AVG(zd3),AVG(zd4) FROM mzd;";
            comm.CommandText = str;
            MySqlDataReader reader = comm.ExecuteReader();

            string timestamp = System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

            if (reader.Read())
            {
                comm.CommandText = string.Format("" +
                "INSERT INTO mzd_forecast(zd1, zd2, zd3, zd4, timestamp) VALUES('{0}','{1}','{2}','{3}','{4}');",
                reader[0], reader[1], reader[2], reader[3], timestamp);
                reader.Close();
                comm.ExecuteNonQuery();
            }
            else
            {
                reader.Close();
            }

            conn.Close();
        }
    }
}
