using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Hegang.APP
{
    class XMLUtils
    {
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

            return path+@"/conf/db_config.xml";
        }
        /// <summary>
        /// 获取elec_usage.xml的相对路径
        /// </summary>
        /// <returns></returns>
        public static string get_elec_usage_path()
        {
            string path = System.AppDomain.CurrentDomain.BaseDirectory;
            Directory.SetCurrentDirectory(Directory.GetParent(path).FullName);
            path = Directory.GetCurrentDirectory();
            Directory.SetCurrentDirectory(Directory.GetParent(path).FullName);
            path = Directory.GetCurrentDirectory();

            return path + @"/conf/elec_usage.xml";
        }
    }
}
