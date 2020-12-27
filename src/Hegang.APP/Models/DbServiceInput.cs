using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hegang.APP.Models
{
    class DbServiceInput
    {
        Dictionary<string, string> dic;
        private string[] gs_tmpBuf;
        /**
         * 1--主井是否开车
         * 2--副井是否开车
         */
        private bool[] zd_flag = new bool[2];

        public DbServiceInput()
        {
            dic = new Dictionary<string, string>();
            /**
            * 用来寄存临时变量
            * 0--主井勾数
            * 1--副井勾数
            */
            gs_tmpBuf = new string[2] { "-1", "-1" };
        }
        
        public Dictionary<string, string> Dic { get => dic; set => dic = value; }
        public string[] Gs_tmpBuf { get => gs_tmpBuf; set => gs_tmpBuf = value; }
        public bool[] Zd_flag { get => zd_flag; set => zd_flag = value; }
    }
}
