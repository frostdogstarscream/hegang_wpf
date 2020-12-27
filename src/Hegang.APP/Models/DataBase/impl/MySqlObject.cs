using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Hegang.APP.Models.DataBase.impl
{
    class MySqlObject : DbObject
    {
        private string server;
        private string port;
        private string user;
        private string password;
        private string database;

        public MySqlObject()
        {
            getDBConfig();
        }

        /// <summary>
        /// 解析db_config.xml文件，获取数据库配置信息
        /// </summary>
        private void getDBConfig()
        {
            XmlDocument doc = new XmlDocument();
            try
            {
                doc.Load(DBUtils.get_db_config_path());

                XmlElement element = (XmlElement)doc.SelectSingleNode("property/server");
                server = element.GetAttribute("value");

                element = (XmlElement)doc.SelectSingleNode("property/port");
                port = element.GetAttribute("value");

                element = (XmlElement)doc.SelectSingleNode("property/user");
                user = element.GetAttribute("value");

                element = (XmlElement)doc.SelectSingleNode("property/password");
                password = element.GetAttribute("value");

                element = (XmlElement)doc.SelectSingleNode("property/database");
                database = element.GetAttribute("value");
            }
            catch (Exception ex)
            {
                Console.WriteLine("数据库配置文件解析失败。" + ex);
            }
        }

        /// <summary>
        /// 获取MySql数据库连接对象
        /// </summary>
        /// <returns></returns>
        public MySqlConnection getDBConnection()
        {
            Console.WriteLine("获取数据库连接对象");
            string str = @"server=" + server + "; port=" + port + ";user=" + user + "; password=" + password + "; database=" + database + ";";
            Console.WriteLine(str);
            MySqlConnection conn = new MySqlConnection(str);
            if (null != conn)
                Console.WriteLine("数据库连接对象获取成功");
            return conn;
        }

        public void cmmdNoReturn(string sql)
        {
            MySqlConnection conn = getDBConnection();
            conn.Open();
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            cmd.ExecuteNonQuery();
            conn.Close();
        }

        public DataTable GetDataTable(string sql)
        {
            MySqlConnection conn = getDBConnection();
            conn.Open();
            MySqlDataAdapter da = new MySqlDataAdapter(sql,conn);
            conn.Close();
            DataSet ds = new DataSet();
            da.Fill(ds);
            return ds.Tables[0];
        }
    }
}
