using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace HFD.KEPWare
{
    /// <summary>
    /// PLC数据包
    /// </summary>
    public class PLCDataList : List<PLCDataPack>
    {
        public string PLCName
        {
            get { return _name; }
            set { _name = value; }
        }
        private string _name = "";

        public void ToFile(string FileName,bool truncate)
        {
            lock (this)
            {
                using (StreamWriter sw = new StreamWriter(FileName, false))
                {
                    if (this.Count > 0)
                        sw.WriteLine(this[0].DeviceName);
                    foreach (PLCDataPack pack in this)
                    {
                        sw.WriteLine(pack.ToString());
                    }
                    sw.Flush();
                    sw.Close();
                }
                if (truncate)
                    this.Clear();
            }         
        }
    }
}
