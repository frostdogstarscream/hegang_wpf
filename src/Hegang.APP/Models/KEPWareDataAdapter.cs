using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Diagnostics;
using System.Net;
using System.Linq;
using OPCAutomation;

namespace HFD.KEPWare
{
    public class KEPWareDataAdapter
    {
        //AxDbCommOcx _dbCommOcx = null;
        OPCServer KEPServer = null;
        /// <summary>
        /// OPCGroups Object
        /// </summary>
        OPCGroups OpcGroups;

        /// <summary>
        /// OPCGroup Object
        /// </summary>
        public OPCGroup[] MyGroups;

        /// <summary>
        /// OPCItems Object
        /// </summary>
        OPCItems [] MyItems;

        /// <summary>
        /// OPCItem Object
        /// </summary>
        OPCItem[] MyItem;

        /// <summary>
        /// 获取Item项目
        /// </summary>
        List<string> opcItemsArray;

        /// <summary>
        /// 获取设备名称
        /// </summary>
        List<string> opcDeviceArray;

        /// <summary>
        /// PLC项目分组
        /// </summary>
        OPCBrowser opcBrowser = null;

        /// <summary>
        /// PLC数据包
        /// </summary>
        PLCDataPack[] pack;

        int itmHandleClient = 0;

        OPCItem opcItem;

        public Hashtable htOpcItems = new Hashtable();

        private bool _ConnectionState = false;

        /// <summary>
        /// 初始化连接
        /// </summary>
        public KEPWareDataAdapter()
        {
            Trace.WriteLine("KEPWareDataAdapter 构造函数开始");
            if (KEPServer == null)
            {
                this.KEPServer =new OPCServer();
            }
        }

        public void InitialConnection(string RemoteHostName)
        {
            MyItem = new OPCItem[4];
            if (ConnectRemoteServer(RemoteHostName))//本机
            {
                if (true)//(CreateGroup())
                {
                    //暂停线程，给DataChange事件反应时间，否则下面的同步读可能读不到
                    Thread.Sleep(500);
                    /*
                    //以下同步读
                    object ItemValue; object Quality; object TimeStamp;//同步读的临时变量：值、质量、时间戳
                    MyItem[0].Read(1, out ItemValue, out Quality, out TimeStamp);//同步读，第一个参数只能为1或2
                    int q0 = Convert.ToInt32(ItemValue);//转换后获取item值
                    MyItem[1].Read(1, out ItemValue, out Quality, out TimeStamp);//同步读，第一个参数只能为1或2
                    int q1 = Convert.ToInt32(ItemValue);//转换后获取item值
                    MyItem[2].Read(1, out ItemValue, out Quality, out TimeStamp);//同步读，第一个参数只能为1或2
                    bool q2 = Convert.ToBoolean(ItemValue);//转换后获取item值
                    MyItem[3].Read(1, out ItemValue, out Quality, out TimeStamp);//同步读，第一个参数只能为1或2
                    string q3 = Convert.ToString(ItemValue);//转换后获取item值，为防止读到的值为空，不用ItemValues.ToString()

                    Console.WriteLine("~~~~~~~~~~~~~~~~~~~~~~~~~~~~");
                    Console.WriteLine("0-{0},1-{1},2-{2},3-{3}", q0, q1, q2, q3);

                    //以下为异步写
                    //异步写时，Array数组从下标1开始而非0！
                    int[] temp = new int[] { 0, MyItem[0].ServerHandle, MyItem[1].ServerHandle, MyItem[2].ServerHandle, MyItem[3].ServerHandle };
                    Array serverHandles = (Array)temp;
                    object[] valueTemp = new object[5] { "", 255, 520, true, "Love" };
                    Array values = (Array)valueTemp;
                    Array Errors;
                    int cancelID;
                    MyGroup.AsyncWrite(4, ref serverHandles, ref values, out Errors, 1, out cancelID);//第一参数为item数量
                                                                                                      //由于MyGroup2没有订阅，所以以下这句运行时将会出错！
                                                                                                      //MyGroup2.AsyncWrite(4, ref serverHandles, ref values, out Errors, 1, out cancelID);

                    //以下异步读
                    MyGroup.AsyncRead(4, ref serverHandles, out Errors, 1, out cancelID);//第一参数为item数量

                    */
                    /*MyItem[0] = MyItems.AddItem("BPJ.Db1.dbb96", 0);//byte
                    MyItem[1] = MyItems.AddItem("BPJ.Db1.dbw10", 1);//short
                    MyItem[2] = MyItems.AddItem("BPJ.Db16.dbx0", 2);//bool
                    MyItem[3] = MyItems.AddItem("BPJ.Db11.S0", 3);//string*/

                }
            }
        }

        public void Close()
        {
            //释放所有组资源
            KEPServer.OPCGroups.RemoveAll();
            //断开服务器
            KEPServer.Disconnect();
        }

        /// <summary>
        /// 创建数据组
        /// </summary>
        /// <returns></returns>
        public bool CreateGroup(List<string> device)
        {

            if (this.opcBrowser == null)
                throw new Exception("服务器没有连接……");
            if (this.opcDeviceArray == null)
                throw new Exception("没有找到需要监测的PLC设备……");
            if (device.Count == 0)
                throw new Exception("请先选择需要监测的PLC设备……");
            if (this.opcItemsArray == null)
            {
                opcItemsArray = new List<string>();
            }
            try
            {
                //设置组属性                                        
                KEPServer.OPCGroups.DefaultGroupIsActive = true;//激活组。
                KEPServer.OPCGroups.DefaultGroupDeadband = 0;// 死区值，设为0时，服务器端该组内任何数据变化都通知组。
                KEPServer.OPCGroups.DefaultGroupUpdateRate = 200;//默认组群的刷新频率为200ms

                OpcGroups = KEPServer.OPCGroups;
                MyGroups = new OPCGroup[device.Count];
                MyItems = new OPCItems[device.Count];
                pack = new PLCDataPack[device.Count];
                for (int i = 0; i < device.Count; i++)
                {
                    MyGroups[i] = OpcGroups.Add(device[i]);//测试删除组
                    MyGroups[i].UpdateRate = 100;//刷新频率为0.1秒。
                    MyGroups[i].IsActive = true;
                    MyGroups[i].IsSubscribed = true;//使用订阅功能，即可以异步，默认false
                    MyItems[i] = MyGroups[i].OPCItems;

                    foreach (var item in opcBrowser)
                    {
                        if (item.ToString().IndexOf(device[i]) >= 0)
                        {
                            opcItemsArray.Add(item.ToString());
                            htOpcItems.Add(itmHandleClient++, item);
                            opcItem = MyItems[i].AddItem(item.ToString(), itmHandleClient);
                        }
                    }
                }

                //MyGroup.DataChange += new DIOPCGroupEvent_DataChangeEventHandler(GroupDataChange);
                //MyGroup.AsyncWriteComplete += new DIOPCGroupEvent_AsyncWriteCompleteEventHandler(GroupAsyncWriteComplete);
                //MyGroup.AsyncReadComplete += new DIOPCGroupEvent_AsyncReadCompleteEventHandler(GroupAsyncReadComplete);

                //由于MyGroup2.IsSubscribed是false，即没有订阅，所以以下的DataChange回调事件不会发生！
                //MyGroup.AsyncWriteComplete += new DIOPCGroupEvent_AsyncWriteCompleteEventHandler(GroupAsyncWriteComplete);
                
                //KEPServer.OPCGroups.Remove("测试3");//移除组
                //AddGroupItems();//设置组内items         
            }
            catch (Exception err)
            {
                Console.WriteLine("创建组出现错误：{0}", err.Message);
                return false;
            }
            return true;
        }

        public void AddGroupItems(List<string> device)//添加组
        {
            /*
            if (this.opcBrowser == null)
                throw new Exception("服务器没有连接……");
            if (this.opcDeviceArray == null)
                throw new Exception("没有找到需要监测的PLC设备……");
            if (device.Count == 0)
                throw new Exception("请先选择需要监测的PLC设备……");
            if (this.opcItemsArray == null)
            {
                opcItemsArray = new List<string>();
            }
            //逐项绑定句柄值
            if (this.opcItemsArray == null)
            {
                opcItemsArray = new List<string>();
            }
            foreach (var item in opcBrowser)
            {
                var result = from string s in device
                             where item.ToString().IndexOf(s) >= 0
                             select s;
                if (result.Count() > 0)
                {
                    opcItemsArray.Add(item.ToString());
                    htOpcItems.Add(itmHandleClient++, item);
                    opcItem = MyItems.AddItem(item.ToString(), itmHandleClient);
                }
            }*/
        }

        /// <summary>
        /// 每当项数据有变化时执行的事件
        /// </summary>
        /// <param name="TransactionID">处理ID</param>
        /// <param name="NumItems">项个数</param>
        /// <param name="ClientHandles">项客户端句柄</param>
        /// <param name="ItemValues">TAG值</param>
        /// <param name="Qualities">品质</param>
        /// <param name="TimeStamps">时间戳</param>1    `
        void GroupDataChange(int TransactionID, int NumItems, ref Array ClientHandles, ref Array ItemValues, ref Array Qualities, ref Array TimeStamps)
        {
            Console.WriteLine("++++++++++++++++DataChanged+++++++++++++++++++++++");
            for (int i = 1; i <= NumItems; i++)
            {
                Console.WriteLine("item值：{0}", ItemValues.GetValue(i).ToString());
                //Console.WriteLine("item句柄：{0}", ClientHandles.GetValue(i).ToString());
                //Console.WriteLine("item质量：{0}", Qualities.GetValue(i).ToString());
                //Console.WriteLine("item时间戳：{0}", TimeStamps.GetValue(i).ToString());
                //Console.WriteLine("item类型：{0}", ItemValues.GetValue(i).GetType().FullName);
            }
        }

        /// <summary>
        /// 异步写完成
        /// 运行时，Array数组从下标1开始而非0！
        /// </summary>
        /// <param name="TransactionID"></param>
        /// <param name="NumItems"></param>
        /// <param name="ClientHandles"></param>
        /// <param name="Errors"></param>
        void GroupAsyncWriteComplete(int TransactionID, int NumItems, ref Array ClientHandles, ref Array Errors)
        {
            Trace.WriteLine("%%%%%%%%%%%%%%%%AsyncWriteComplete%%%%%%%%%%%%%%%%%%%");
            /*for (int i = 1; i <= NumItems; i++)
            {
                Console.WriteLine("Tran：{0}   ClientHandles：{1}   Error：{2}", TransactionID.ToString(), ClientHandles.GetValue(i).ToString(), Errors.GetValue(i).ToString());
            }*/
        }

        /// <summary>
        /// 异步读完成
        /// 运行时，Array数组从下标1开始而非0！
        /// </summary>
        /// <param name="TransactionID"></param>
        /// <param name="NumItems"></param>
        /// <param name="ClientHandles"></param>
        /// <param name="ItemValues"></param>
        /// <param name="Qualities"></param>
        /// <param name="TimeStamps"></param>
        /// <param name="Errors"></param>
        void GroupAsyncReadComplete(int TransactionID, int NumItems, ref System.Array ClientHandles, ref System.Array ItemValues, ref System.Array Qualities, ref System.Array TimeStamps, ref System.Array Errors)
        {
            Trace.WriteLine("****************GroupAsyncReadComplete*******************");
            for (int i = 1; i <= NumItems; i++)
            {
                //Console.WriteLine("Tran：{0}   ClientHandles：{1}   Error：{2}", TransactionID.ToString(), ClientHandles.GetValue(i).ToString(), Errors.GetValue(i).ToString());
                Console.WriteLine("Vaule：{0}", Convert.ToString(ItemValues.GetValue(i)));
            }
        }

        #region 属性
        /// <summary>
        /// 更新周期(单位ms)，默认值100ms
        /// </summary>
        public int UpdateRate
        {
            get
            {
                if (MyGroups == null || MyGroups.Length < 1)
                    return -1;
                return MyGroups[0].UpdateRate;
            }
            set
            {
                if (value <= 0)
                {
                    throw new ArgumentOutOfRangeException("UpdateTime", "更新周期必需为正整数");
                }
                if (MyGroups == null)
                    throw new NullReferenceException("OPCGroup is null");
                for(int i=0;i<MyGroups.Length;i++)
                    MyGroups[i].UpdateRate = value;
            }
        }
        /// <summary>
        /// 连接状态
        /// </summary>
        public bool ConnectionState
        {
            get
            {
                _ConnectionState = KEPServer.ServerState == 1? true : false;
                return _ConnectionState;
            }
        }
        #endregion

        #region 打开和关闭连接

        /// <summary>
        /// 枚举本地OPC服务器
        /// </summary>
        public object GetLocalServer()
        {
            try
            {
                if(KEPServer == null)
                    KEPServer = new OPCServer();
                object serverList = KEPServer.GetOPCServers(Environment.MachineName);
                return serverList;
            }
            catch (Exception err)
            {
                Console.WriteLine("枚举本地OPC服务器出错：{0}", err.Message);
            }
            return null;
        }

        /// <summary>
        /// 枚举本地OPC服务器
        /// </summary>
        public List<string> GetPLCDevices()
        {
            if (KEPServer == null)
                throw new Exception("OPC服务器没有连接");
            try
            {
                opcBrowser = KEPServer.CreateBrowser();
                opcBrowser.ShowBranches();
                opcBrowser.ShowLeafs(true);
                int count = opcBrowser.Count;
                if (this.opcDeviceArray == null)
                {
                    opcDeviceArray = new List<string>();
                }
                int nPoint1 = -1, nPoint2 = -1;
                foreach (var item in opcBrowser)
                {
                    if (item.ToString().IndexOf(".") == -1 || item.ToString().IndexOf("_") != -1)
                        continue;
                    //获取第二个.的位置
                    nPoint1 = item.ToString().IndexOf(".");
                    nPoint2 = item.ToString().IndexOf(".", nPoint1 + 1);
                    if (!opcDeviceArray.Contains(item.ToString().Substring(0, nPoint2)))
                        opcDeviceArray.Add(item.ToString().Substring(0, nPoint2));
                }
            }
            catch (Exception err)
            {
                Console.WriteLine("获取PLC设备连接出错：{0}", err.Message);
            }
            return opcDeviceArray;
        }

        /// <summary>
        /// 创建服务器（远程模式）
        /// </summary>
        /// <param name="RemoteAddr">要连接远程服务器IP</param>
        /// <returns>执行连接是否成功</returns>
        public bool ConnectRemoteServer(string remoteHostName)
        {
            if (_ConnectionState)
            {
                return true;
            }
            try
            {
                KEPServer.Connect(remoteHostName);//连接本地服务器：服务器名+主机名或IP, remoteAddr

                if (KEPServer.ServerState == (int)OPCServerState.OPCRunning)
                {
                    Console.WriteLine("已连接到：{0}", KEPServer.ServerName);
                }
                else
                {
                    //这里你可以根据返回的状态来自定义显示信息，请查看自动化接口API文档
                    Console.WriteLine("{0}状态：{1}", KEPServer.ServerName, KEPServer.ServerState);
                }
                KEPServer.ServerShutDown += ServerShutDown;//服务器断开事件
            }
            catch (Exception err)
            {
                Console.WriteLine("连接远程服务器出现错误：{0}", err.ToString());
                return false;
            }
            return true;
        }

        /// <summary>
        /// 断开与服务器的连接
        /// </summary>
        /// <param name="Reason"></param>
        public void ServerShutDown(string Reason)//服务器先行断开
        {
            if (KEPServer == null)
                return;
            if(KEPServer.OPCGroups.Count > 0)
                KEPServer.OPCGroups.RemoveAll();
            if(KEPServer.ServerState == 1)
            KEPServer.Disconnect();
            Trace.WriteLine("服务器已经先行断开！");
        }
        #endregion

    }
}
