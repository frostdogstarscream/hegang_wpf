using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hegang.APP
{
    class TimeJudgeItem
    {
        private DateTime time;
        private bool flag;

        public TimeJudgeItem()
        {
            this.time = DateTime.Now;
            this.flag = false;
        }

        public TimeJudgeItem(DateTime time, bool flag)
        {
            this.time = time;
            this.flag = flag;
        }

        public DateTime Time { get => time; set => time = value; }
        public bool Flag { get => flag; set => flag = value; }
    }
}
