using Hegang.APP.Models;
using Hegang.APP.Models.DataBase;
using Hegang.APP.Services.DbService;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Hegang.APP
{
    class DataSaveService
    {
        private List<DbServiceObject> list;
        private DbObject o;
        

        public DataSaveService()
        {
            string db_str = ConfigurationManager.AppSettings["DB"];
            o = (DbObject)Assembly.Load("Hegang.APP").CreateInstance(db_str);

            list = new List<DbServiceObject>();
            for(int i = 0; i < 12; i++)
            {
                string str = ConfigurationManager.AppSettings["DbService_" + i.ToString()];
                list.Add((DbServiceObject)Assembly.Load("Hegang.APP").CreateInstance(str));
            }
        }

        internal DbObject O { get => o; set => o = value; }

        private void resetZd_flag(ref DbServiceInput input)
        {
            if ("True" == input.Dic["主井.主提升电控.开车信号"])
                input.Zd_flag[0] = true;
            if ("False" == input.Dic["主井.主提升电控.开车信号"])
                input.Zd_flag[0] = false;
            if ("True" == input.Dic["副井.副提升电控.开车条件"])
                input.Zd_flag[1] = true;
            if ("False" == input.Dic["副井.副提升电控.开车条件"])
                input.Zd_flag[1] = false;
        }

        public void savePLCData(ref DbServiceInput input,TimeJudgeItemList timeJudgeItemList)
        {
            resetZd_flag(ref input);
            
            if (null == o)
                Console.WriteLine("o为null");
            if (null == input)
                Console.WriteLine("input为null");
            if (null == list[8])
                Console.WriteLine("list[8]为null");
            // 保存主井提升机状态信息
            list[8].save(o, ref input);
            // 保存副井提升机状态信息
            list[2].save(o, ref input);

            // 保存主井勾数和电度信息
            if (timeJudgeItemList.MList[0].Flag)
                list[6].save(o, ref input);
            // 保存副井勾数和电度信息
            if (timeJudgeItemList.FList[0].Flag)
                list[0].save(o,ref input);

            // 保存主井提升机的速度、高度、功率和功率因素信息
            if (timeJudgeItemList.MList[1].Flag)
                list[7].save(o, ref input);
            // 保存副井提升机的速度、高度、功率和功率因素信息
            if (timeJudgeItemList.FList[1].Flag)
                list[1].save(o, ref input);

            // 保存主井温度信息
            if (timeJudgeItemList.MList[2].Flag)
                list[9].save(o, ref input);
            // 保存副井温度信息
            if (timeJudgeItemList.FList[2].Flag)
                list[3].save(o, ref input);

            // 保存主井振动信息
            if (timeJudgeItemList.MList[3].Flag)
                list[10].save(o, ref input);
            // 保存副井振动信息
            if (timeJudgeItemList.FList[3].Flag)
                list[4].save(o, ref input);

            // 保存主井油压信息
            if (timeJudgeItemList.MList[4].Flag)
                list[11].save(o, ref input);
            // 保存副井油压信息
            if (timeJudgeItemList.FList[4].Flag)
                list[5].save(o, ref input);
        }
    }
}