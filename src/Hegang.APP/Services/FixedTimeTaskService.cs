using Hegang.APP.Models.DataBase;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Hegang.APP
{
    class FixedTimeTaskService
    {
        private DbObject o;

        public FixedTimeTaskService()
        {
            string db_str = ConfigurationManager.AppSettings["DB"];
            this.o = (DbObject)Assembly.Load("Hegang.APP").CreateInstance(db_str);
        }

        /// <summary>
        /// 设置每天 00:00 对数据表 mtsjzt_live、ftsjzt_live、mtsjgd_live、ftsjgd_live 进行统计
        /// 参考：https://blog.csdn.net/zhouyingge1104/article/details/94211503
        /// </summary>
        public void setTaskAtZero()
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
            var t = new System.Threading.Timer(handle_event_at_zero);
            //设置线程参数 任务delay毫秒后启动
            t.Change(delay, Timeout.Infinite);
        }

        /// <summary>
        /// 每隔一小时统计一次温度、油压
        /// </summary>
        public void setTaskPerHour()
        {
            DateTime now = DateTime.Now;
            //设置任务启动时间  
            double hour = Convert.ToDouble(DateTime.Now.Hour + 1);
            double minute = 0.0;   //分钟
            DateTime startTime = DateTime.Today.AddHours(hour).AddMinutes(minute);
            int delay = (int)((startTime - now).TotalMilliseconds);
            var t = new System.Threading.Timer(handle_event_per_hour);
            //设置线程参数 任务delay毫秒后启动
            t.Change(delay, Timeout.Infinite);
        }

        /// <summary>
        /// 每隔一分钟预测一次温度、振动
        /// </summary>
        public void setTaskPerMinute()
        {
            DateTime now = DateTime.Now;
            //设置任务启动时间  
            double hour = Convert.ToDouble(DateTime.Now.Hour);
            double minute = Convert.ToDouble(DateTime.Now.Minute + 1);
            double second = Convert.ToDouble(DateTime.Now.Second);
            DateTime startTime = DateTime.Today.AddHours(hour).AddMinutes(minute).AddSeconds(second);
            int delay = (int)((startTime - now).TotalMilliseconds);
            var t = new System.Threading.Timer(handle_event_per_minute);
            //设置线程参数 任务delay毫秒后启动
            t.Change(delay, Timeout.Infinite);
        }

        /// <summary>
        /// setTaskAtZero 执行的任务
        /// </summary>
        /// <param name="state"></param>
        private void handle_event_at_zero(object state)
        {
            #region 执行的任务
            task1();
            task2();
            task3();
            #endregion
            
            // 再次设定任务执行时间
            setTaskAtZero();
        }

        /// <summary>
        /// setTaskPerHour 执行的任务
        /// </summary>
        /// <param name="state"></param>
        private void handle_event_per_hour(object state)
        {
            #region 执行的任务
            task4();
            task5();
            task6();
            task7();
            task8();
            task9();
            #endregion

            // 再次设定任务执行时间
            setTaskPerHour();
        }

        /// <summary>
        /// setTaskPerMinute 执行的任务
        /// </summary>
        /// <param name="state"></param>
        private void handle_event_per_minute(object state)
        {
            // 执行任务
            task10();
            task11();
            task12();
            task13();
            // 再次设定任务执行时间
            setTaskPerMinute();
        }

        /// <summary>
        /// 统计主井和副井的提升机状态表
        /// </summary>
        private void task1()
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

            o.cmmdNoReturn(mtsjgd_live_SQL);
            o.cmmdNoReturn(ftsjgd_live_SQL);
        }

        /// <summary>
        /// 每天统计主井的勾数
        /// </summary>
        public void task2()
        {
            string str1 = "SELECT SUM(GS) FROM mtsjgd_live";
            DataTable dt=o.GetDataTable(str1);
            int GS_sum = Convert.ToInt32(dt.Rows[0][0]);

            //将统计结果存入数据库
            string str2 = string.Format("" +
                "INSERT INTO " +
                    "mtsjgdtj(GS,TimeStamp) " +
                "VALUES" +
                    "('{0}','{1}');", GS_sum, System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            o.cmmdNoReturn(str2);

            //统计完后，清空mtsjgd_live
            string str3 = "TRUNCATE mtsjgd_live;";
            o.cmmdNoReturn(str3);
        }

        /// <summary>
        /// 每天统计副井的勾数
        /// </summary>
        public void task3()
        {
            string str1 = "SELECT SUM(GS) FROM ftsjgd_live";
            DataTable dt = o.GetDataTable(str1);
            int GS_sum = Convert.ToInt32(dt.Rows[0][0]);

            //将统计结果存入数据库
            string str2 = string.Format("" +
                "INSERT INTO " +
                    "ftsjgdtj(GS,TimeStamp) " +
                "VALUES" +
                    "('{0}','{1}');", GS_sum, System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            o.cmmdNoReturn(str2);

            //统计完后，清空ftsjgd_live
            string str3 = "TRUNCATE ftsjgd_live;";
            o.cmmdNoReturn(str3);
        }

        /// <summary>
        /// 每小时统计副井每个传感器的平均温度
        /// </summary>
        public void task4()
        {
            string str = "SELECT AVG(wd1),AVG(wd2),AVG(wd3),AVG(wd4) FROM fwd;";
            DataTable dt=o.GetDataTable(str);

            string timestamp = System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            
            str = string.Format("" +
            "INSERT INTO fwd_h(wd1, wd2, wd3, wd4, timestamp) VALUES('{0}','{1}','{2}','{3}','{4}');" +
            "TRUNCATE fwd;" +
            "", dt.Rows[0][0], dt.Rows[0][1], dt.Rows[0][2], dt.Rows[0][3], timestamp);
            
            o.cmmdNoReturn(str);
        }
        /// <summary>
        /// 每小时统计主井每个传感器的平均温度
        /// </summary>
        public void task5()
        {
            string str = "SELECT AVG(wd1),AVG(wd2),AVG(wd3),AVG(wd4) FROM mwd;";
            DataTable dt = o.GetDataTable(str);

            string timestamp = System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

            str = string.Format("" +
            "INSERT INTO mwd_h(wd1, wd2, wd3, wd4, timestamp) VALUES('{0}','{1}','{2}','{3}','{4}');" +
            "TRUNCATE mwd;" +
            "", dt.Rows[0][0], dt.Rows[0][1], dt.Rows[0][2], dt.Rows[0][3], timestamp);

            o.cmmdNoReturn(str);
        }

        /// <summary>
        /// 每小时统计主井的平均残压和平均正常压力
        /// </summary>
        public void task6()
        {
            string str = "SELECT AVG(yl) FROM mzzyl WHERE yl < 1;";
            DataTable dt = o.GetDataTable(str);

            string residual_pressure = "0";
            if (dt.Rows.Count != 0)
                residual_pressure = dt.Rows[0][0].ToString();

            str = "SELECT AVG(yl) FROM mzzyl WHERE yl > 11;";
            dt = o.GetDataTable(str);

            string normal_pressure = "0";
            if (dt.Rows.Count != 0)
                normal_pressure = dt.Rows[0][0].ToString();

            string timestamp = System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            str = string.Format("INSERT INTO mzzyl_h(residual_pressure, normal_pressure, timestamp) VALUES('{0}','{1}','{2}')", residual_pressure, normal_pressure, timestamp);
            o.cmmdNoReturn(str);

            str = "TRUNCATE mzzyl;";
            o.cmmdNoReturn(str);
        }
        /// <summary>
        /// 每小时统计副井的平均残压和平均正常压力
        /// </summary>
        public void task7()
        {
            string str = "SELECT AVG(yl) FROM fzzyl WHERE yl < 1;";
            DataTable dt = o.GetDataTable(str);

            string residual_pressure = "0";
            if (dt.Rows.Count != 0)
                residual_pressure = dt.Rows[0][0].ToString();
            
            str = "SELECT AVG(yl) FROM fzzyl WHERE yl > 11;";
            dt = o.GetDataTable(str);

            string normal_pressure = "0";
            if (dt.Rows.Count != 0)
                normal_pressure = dt.Rows[0][0].ToString();

            string timestamp = System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            str = string.Format("INSERT INTO fzzyl_h(residual_pressure, normal_pressure, timestamp) VALUES('{0}','{1}','{2}')", residual_pressure, normal_pressure, timestamp);
            o.cmmdNoReturn(str);

            str = "TRUNCATE fzzyl;";
            o.cmmdNoReturn(str);
        }

        /// <summary>
        /// 统计主井振动
        /// </summary>
        public void task8()
        {
            string str = "SELECT AVG(zd1),AVG(zd2),AVG(zd3),AVG(zd4) FROM mzd;";
            DataTable dt=o.GetDataTable(str);

            string timestamp = System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

            str = string.Format("" +
                "INSERT INTO mzd_h(zd1, zd2, zd3, zd4, timestamp) VALUES('{0}','{1}','{2}','{3}','{4}');" +
                "TRUNCATE mzd;" +
                "", dt.Rows[0][0], dt.Rows[0][1], dt.Rows[0][2], dt.Rows[0][3], timestamp);
            o.cmmdNoReturn(str);
        }
        /// <summary>
        /// 统计副井振动
        /// </summary>
        public void task9()
        {
            string str = "SELECT AVG(zd1),AVG(zd2),AVG(zd3),AVG(zd4) FROM fzd;";
            DataTable dt = o.GetDataTable(str);

            string timestamp = System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

            str = string.Format("" +
                "INSERT INTO fzd_h(zd1, zd2, zd3, zd4, timestamp) VALUES('{0}','{1}','{2}','{3}','{4}');" +
                "TRUNCATE fzd;" +
                "", dt.Rows[0][0], dt.Rows[0][1], dt.Rows[0][2], dt.Rows[0][3], timestamp);
            o.cmmdNoReturn(str);
        }

        /// <summary>
        /// 预测副井温度
        /// </summary>
        public void task10()
        {
            string str = "SELECT COUNT(id) FROM fwd;";
            DataTable dt = o.GetDataTable(str);
            int count = Convert.ToInt32(dt.Rows[0][0]);
            
            if (count <= 10)
                str = "SELECT AVG(wd1),AVG(wd2),AVG(wd3),AVG(wd4) FROM fwd;";
            else
                //在MySQL中要给子查询添加别名，否则会报错；在Oracle中不需要
                str = "SELECT AVG(wd1),AVG(wd2),AVG(wd3),AVG(wd4) FROM (SELECT * FROM fwd ORDER BY TIMESTAMP DESC LIMIT 10) a;";

            dt = o.GetDataTable(str);
            
            string timestamp = System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

            str = string.Format("" +
            "INSERT INTO fwd_forecast(wd1, wd2, wd3, wd4, timestamp) VALUES('{0}','{1}','{2}','{3}','{4}');",
            dt.Rows[0][0], dt.Rows[0][1], dt.Rows[0][2], dt.Rows[0][3], timestamp);

            o.cmmdNoReturn(str);
        }

        /// <summary>
        /// 预测主井温度
        /// </summary>
        public void task11()
        {
            string str = "SELECT COUNT(id) FROM mwd;";
            DataTable dt = o.GetDataTable(str);
            int count = Convert.ToInt32(dt.Rows[0][0]);

            if (count <= 10)
                str = "SELECT AVG(wd1),AVG(wd2),AVG(wd3),AVG(wd4) FROM mwd;";
            else
                //在MySQL中要给子查询添加别名，否则会报错；在Oracle中不需要
                str = "SELECT AVG(wd1),AVG(wd2),AVG(wd3),AVG(wd4) FROM (SELECT * FROM mwd ORDER BY TIMESTAMP DESC LIMIT 10) a;";

            dt = o.GetDataTable(str);

            string timestamp = System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

            str = string.Format("" +
            "INSERT INTO mwd_forecast(wd1, wd2, wd3, wd4, timestamp) VALUES('{0}','{1}','{2}','{3}','{4}');",
            dt.Rows[0][0], dt.Rows[0][1], dt.Rows[0][2], dt.Rows[0][3], timestamp);

            o.cmmdNoReturn(str);
        }

        /// <summary>
        /// 预测副井振动
        /// </summary>
        public void task12()
        {
            string str = "SELECT COUNT(id) FROM fzd;";
            DataTable dt = o.GetDataTable(str);
            int count = Convert.ToInt32(dt.Rows[0][0]);

            if (count <= 10)
                str = "SELECT AVG(zd1),AVG(zd2),AVG(zd3),AVG(zd4) FROM fzd;";
            else
                //在MySQL中要给子查询添加别名，否则会报错；在Oracle中不需要
                str = "SELECT AVG(zd1),AVG(zd2),AVG(zd3),AVG(zd4) FROM (SELECT * FROM fzd ORDER BY TIMESTAMP DESC LIMIT 10) a;";

            dt = o.GetDataTable(str);

            string timestamp = System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

            str = string.Format("" +
            "INSERT INTO fzd_forecast(zd1, zd2, zd3, zd4, timestamp) VALUES('{0}','{1}','{2}','{3}','{4}');",
            dt.Rows[0][0], dt.Rows[0][1], dt.Rows[0][2], dt.Rows[0][3], timestamp);

            o.cmmdNoReturn(str);
        }

        /// <summary>
        /// 预测主井振动
        /// </summary>
        public void task13()
        {
            #region 利用java代码预测
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
            #endregion

            string str = "SELECT COUNT(id) FROM mzd;";
            DataTable dt = o.GetDataTable(str);
            int count = Convert.ToInt32(dt.Rows[0][0]);

            if (count <= 10)
                str = "SELECT AVG(zd1),AVG(zd2),AVG(zd3),AVG(zd4) FROM mzd;";
            else
                //在MySQL中要给子查询添加别名，否则会报错；在Oracle中不需要
                str = "SELECT AVG(zd1),AVG(zd2),AVG(zd3),AVG(zd4) FROM (SELECT * FROM mzd ORDER BY TIMESTAMP DESC LIMIT 10) a;";

            dt = o.GetDataTable(str);

            string timestamp = System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

            str = string.Format("" +
            "INSERT INTO mzd_forecast(zd1, zd2, zd3, zd4, timestamp) VALUES('{0}','{1}','{2}','{3}','{4}');",
            dt.Rows[0][0], dt.Rows[0][1], dt.Rows[0][2], dt.Rows[0][3], timestamp);

            o.cmmdNoReturn(str);
        }
    }
}
