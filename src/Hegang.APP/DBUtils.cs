using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Hegang.APP
{
    class DBUtils
    {
        private static string server;
        private static string port;
        private static string user;
        private static string password;
        private static string database;

        /// <summary>
        /// 静态构造函数，只在初始化时执行一次
        /// </summary>
        static DBUtils()
        {
            getDBConfig();
        }

        /// <summary>
        /// 获取数据库配置信息
        /// </summary>
        private static void getDBConfig()
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(get_db_config_path());

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

        /// <summary>
        /// 获取数据库连接对象
        /// </summary>
        /// <returns></returns>
        public static MySqlConnection getDBConnection()
        {
            string str = @"server=" + server + "; port=" + port + ";user=" + user + "; password=" + password + "; database=" + database + ";";
            MySqlConnection conn = new MySqlConnection(str);
            return conn;
        }

        /// <summary>
        /// 获取db_config.xml的相对路径
        /// </summary>
        /// <returns></returns>
        public static string get_db_config_path()
        {
            string path = System.AppDomain.CurrentDomain.BaseDirectory;
            Directory.SetCurrentDirectory(Directory.GetParent(path).FullName);
            path = Directory.GetCurrentDirectory();
            Directory.SetCurrentDirectory(Directory.GetParent(path).FullName);
            path = Directory.GetCurrentDirectory();

            return path + @"/conf/db_config.xml";
        }
    }
}
