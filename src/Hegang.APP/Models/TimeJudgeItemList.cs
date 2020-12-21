using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hegang.APP
{
    class TimeJudgeItemList
    {
        private TimeJudgeItem[] mList;
        private TimeJudgeItem[] fList;

        public TimeJudgeItemList()
        {
            /*
             * 0        勾数                              1h 
             * 1        速度、高度、功率、功率因素        1s
             * 2        温度                              1s
             * 3        振动                              1s
             * 4        油压                              1s
             */
            this.MList = new TimeJudgeItem[5];
            this.FList = new TimeJudgeItem[5];

            for (int i = 0; i < 5; i++)
            {
                MList[i] = FList[i] = new TimeJudgeItem();
            }
        }

        internal TimeJudgeItem[] MList { get => mList; set => mList = value; }
        internal TimeJudgeItem[] FList { get => fList; set => fList = value; }

        /// <summary>
        /// 将主井与副井的每个元素的 flag 重置为 false 
        /// </summary>
        public void resetFlags()
        {
            for (int i = 0; i < 5; i++)
            {
                this.MList[i].Flag = this.FList[i].Flag = false;
            }
        }

        /// <summary>
        /// 将主井与副井的每个元素的 time 重置为 DateTime.Now
        /// </summary>
        public void resetTimeList()
        {
            //处理主井勾数和电度
            if ((int)(DateTime.Now - this.MList[0].Time).TotalMinutes >= 60)
                MList[0] = new TimeJudgeItem(DateTime.Now, true);
            //处理副井勾数和电度
            if ((int)(DateTime.Now - this.FList[0].Time).TotalMinutes >= 60)
                FList[0] = new TimeJudgeItem(DateTime.Now, true);

            //处理主井速度、高度、功率、功率因素
            if ((int)(DateTime.Now - this.MList[1].Time).TotalSeconds >= 1)
                MList[1] = new TimeJudgeItem(DateTime.Now, true);
            //处理副井速度、高度、功率、功率因素
            if ((int)(DateTime.Now - this.FList[1].Time).TotalSeconds >= 1)
                FList[1] = new TimeJudgeItem(DateTime.Now, true);

            //处理主井温度
            if ((int)(DateTime.Now - this.MList[2].Time).TotalSeconds >= 1)
                MList[2] = new TimeJudgeItem(DateTime.Now, true);
            //处理副井温度
            if ((int)(DateTime.Now - this.FList[2].Time).TotalSeconds >= 1)
                FList[2] = new TimeJudgeItem(DateTime.Now, true);

            //处理主井振动
            if ((int)(DateTime.Now - this.MList[3].Time).TotalSeconds >= 1)
                MList[3] = new TimeJudgeItem(DateTime.Now, true);
            //处理副井振动
            if ((int)(DateTime.Now - this.FList[3].Time).TotalSeconds >= 1)
                FList[3] = new TimeJudgeItem(DateTime.Now, true);

            //处理主井压力
            if ((int)(DateTime.Now - this.MList[4].Time).TotalSeconds >= 1)
                MList[4] = new TimeJudgeItem(DateTime.Now, true);
            //处理副井压力
            if ((int)(DateTime.Now - this.FList[4].Time).TotalSeconds >= 1)
                FList[4] = new TimeJudgeItem(DateTime.Now, true);
        }
    }
}
