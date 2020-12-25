using Hegang.APP.Models.DataBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hegang.APP.Services.DbService
{
    class Gz
    {
        public static void save(DbObject o,string deviceName, string ItemName)
        {
            string str = string.Format("INSERT INTO gz (channel,gzname,timestamp) VALUES('{0}','{1}','{2}')", deviceName, ItemName, System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            o.cmmdNoReturn(str);
        }
    }
}
