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
        // 静态变量在类被第一次使用时，就存在于内存当中,直到它所在类的程序运行结束时才消亡；
        private static string server;
        private static string port;
        private static string user;
        private static string password;
        private static string database;

        /// <summary>
        /// 静态构造函数，只在初始化时执行一次，保证只读取一次xml文件
        /// </summary>
        static DBUtils()
        {
            Console.WriteLine("解析数据库配置文件。");
            getDBConfig();
        }

        /// <summary>
        /// 解析xml文件，获取数据库配置信息
        /// </summary>
        private static void getDBConfig()
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(@"../conf/db_config.xml");

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
