﻿using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hegang.APP
{
    class Utils
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

            return path + @"/conf/db_config.xml";
        }

        /// <summary>
        /// 获取plc_items_table.csv的相对路径
        /// </summary>
        /// <returns></returns>
        public static string get_csv_path()
        {
            string path = System.AppDomain.CurrentDomain.BaseDirectory;
            Directory.SetCurrentDirectory(Directory.GetParent(path).FullName);
            path = Directory.GetCurrentDirectory();
            Directory.SetCurrentDirectory(Directory.GetParent(path).FullName);
            path = Directory.GetCurrentDirectory();

            return path + @"/conf/";
        }

        /// <summary>
        /// 将plc_items_table.csv解析为DataTable
        /// </summary>
        /// <returns></returns>
        public static List<string> read_csv_file(string file_name)
        {
            StreamReader streamReader = new StreamReader(get_csv_path()+file_name, System.Text.Encoding.UTF8);
            string line = null;
            String[] array = null;
            List<string> dataItems = new List<string>();

            bool flag = false;
            while ((line = streamReader.ReadLine()) != null)
            {
                if (flag)
                {
                    array = line.Split(new char[] { ',' });
                    dataItems.Add(array[0]);
                }
                flag=true;
            }

            return dataItems;
        }
    }
}
