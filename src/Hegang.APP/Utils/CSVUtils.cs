using Hegang.APP.ViewModels;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Hegang.APP
{
    class CSVUtils
    {
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
        public static List<string> getTpNames(string file_name)
        {
            StreamReader streamReader = new StreamReader(get_csv_path()+file_name+".csv", System.Text.Encoding.UTF8);
            string line = null;
            String[] array = null;
            List<string> dataItems = new List<string>();

            bool flag = false;
            while ((line = streamReader.ReadLine()) != null)
            {
                if (flag)
                {
                    array = line.Split(new char[] { ',' });
                    //去掉 ""
                    array[0]=array[0].Substring(1, array[0].Length - 2);
                    dataItems.Add(array[0]);
                }
                flag=true;
            }

            return dataItems;
        }

        public static ObservableCollection<TestPoint> getTestPoints(string fileName)
        {
            StreamReader streamReader = new StreamReader(get_csv_path() + fileName, System.Text.Encoding.UTF8);
            string line = null;
            String[] array = null;
            ObservableCollection<TestPoint> testPoints = new ObservableCollection<TestPoint>();

            int i = 0;      
            while ((line = streamReader.ReadLine()) != null)
            {
                //跳过csv文件的第一行。
                if (i!=0)
                {
                    array = CSVstrToArry(line);
                    //去掉 ""
                    //array[0] = array[0].Substring(1, array[0].Length - 2);
                    TestPoint testPoint = new TestPoint();
                    testPoint.Id = i.ToString();
                    testPoint.Name= array[0];
                    testPoint.Address= array[1];
                    testPoint.DataType = array[2];
                    //跳过点表中的Respect Data Type
                    testPoint.ClientAccess = array[4];
                    testPoint.ScanRate = array[5];
                    testPoints.Add(testPoint);
                }
                i++;
            }
            return testPoints;
        }

        /// <summary>
        /// 跳过引号中的逗号,进行逗号分隔(字段内容中的逗号不参与分隔)
        /// 参考：https://www.cnblogs.com/PrintY/p/14076948.html
        /// </summary>
        /// <param name="strLine"></param>
        /// <returns></returns>
        public static string[] CSVstrToArry(string splitStr)
        {
            var newstr = string.Empty;
            List<string> sList = new List<string>();

            bool isSplice = false;
            string[] array = splitStr.Split(new char[]{','});
            
            foreach (var str in array)
            {
                //字符串包含 '"'
                if (!string.IsNullOrEmpty(str) && str.IndexOf('"') > -1)
                {
                    //获取字符串的第一个和最后一个字符
                    var firstchar = str.Substring(0, 1);
                    var lastchar = string.Empty;

                    if (str.Length > 0)
                        lastchar = str.Substring(str.Length - 1, 1);

                    //字符串的第一个字符为 '"'
                    if (firstchar.Equals("\"") && !lastchar.Equals("\""))
                        isSplice = true;

                    if (lastchar.Equals("\""))
                    {
                        if (!isSplice)
                            newstr += str;
                        else
                            newstr = newstr + "," + str;

                        isSplice = false;
                    }
                }
                else
                {
                    if (string.IsNullOrEmpty(newstr))
                        newstr += str;
                }

                if (isSplice)
                {
                    //添加因拆分时丢失的逗号
                    if (string.IsNullOrEmpty(newstr))
                        newstr += str;
                    else
                        newstr = newstr + "," + str;
                }
                else
                {
                    sList.Add(newstr.Replace("\"", "").Trim());//去除字符中的双引号和首尾空格
                    newstr = string.Empty;
                }
            }

            return sList.ToArray();
        }

        public static void saveTestPointsToCSV(ObservableCollection<TestPoint> testPoints, string fileName)
        {
            FileStream fs = new FileStream(CSVUtils.get_csv_path() + fileName, FileMode.Create, FileAccess.Write);
            System.IO.StreamWriter sw = new System.IO.StreamWriter(fs, System.Text.Encoding.UTF8);
            string line = "Tag Name,Address,Data Type,Respect Data Type,Client Access,Scan Rate," +
                "Scaling,Raw Low,Raw High,Scaled Low,Scaled High,Scaled Data Type,Clamp Low,Clamp High,Eng Units,Description,Negate Value";
            sw.WriteLine(line);

            for (int i = 0; i < testPoints.Count; i++)
            {
                TestPoint t = testPoints[i];
                line = string.Format("\"{0}\"", t.Name) + "," + string.Format("\"{0}\"", t.Address) + "," + t.DataType + ",1," + t.ClientAccess + "," + t.ScanRate+ ",,,,,,,,,,\"\"";
                sw.WriteLine(line);
            }

            sw.Close();
            fs.Close();
        }
    }
}
