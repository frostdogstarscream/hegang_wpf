using Hegang.APP.Commands;
using Hegang.APP.Models;
using Hegang.APP.Services.DbService;
using HFD.KEPWare;
using OPCAutomation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Hegang.APP.ViewModels
{
    class MainWindowViewModel : NotificationObject
    {
        #region 普通私有成员变量
        private KEPWareDataAdapter da;
        private DbServiceInput input;
        private DataSaveService dataSaveService;
        private List<string> channel_device_list;
        private TimeJudgeItemList timeJudgeItemList;
        private FixedTimeTaskService fixedTimeTaskService;
        #endregion

        #region 数据属性
        private List<string> serverListToString;
        private ObservableCollection<Node> tree;
        private ObservableCollection<ListViewItem> listViewItemList;
        private string selectedServer;
        private bool btn_connect_isEnabled;
        private bool btn_read_isEnabled;
        private bool btn_stop_isEnabled;
        private string colorBar_text;
        private string colorBar_color;
        private string consoleText;
        #endregion

        #region 命令属性
        public DelegateCommand ConnectCommand { get; set; }
        public DelegateCommand ReadCommand { get; set; }
        public DelegateCommand StopCommand { get; set; }
        #endregion

        public MainWindowViewModel()
        {
            #region 数据属性初始化
            this.ConsoleText = "";
            this.ListViewItemList = new ObservableCollection<ListViewItem>();
            this.ColorBar_color = "#065279";
            this.ColorBar_text = "就绪";
            #endregion

            #region 命令属性初始化
            this.ConnectCommand = new DelegateCommand();
            this.ConnectCommand.ExcuteAction = new Action<object>(this.ConnectCommandExecute);

            this.ReadCommand = new DelegateCommand();
            this.ReadCommand.ExcuteAction = new Action<object>(this.ReadCommandExecute);

            this.StopCommand = new DelegateCommand();
            this.StopCommand.ExcuteAction = new Action<object>(this.StopCommandExecute);
            #endregion

            #region 普通私用成员变量初始化
            da = new KEPWareDataAdapter();
            this.ConsoleText += "KEPWareDataAdapter对象已完成初始化。\n";
            timeJudgeItemList = new TimeJudgeItemList();
            input = new DbServiceInput();
            dataSaveService = new DataSaveService();
            fixedTimeTaskService = new FixedTimeTaskService();
            #endregion

            #region 按钮初始化
            this.Btn_connect_isEnabled = true;
            this.Btn_read_isEnabled = false;
            this.Btn_stop_isEnabled = false;
            #endregion

            #region 设置定时任务
            fixedTimeTaskService.setTaskAtZero();
            fixedTimeTaskService.setTaskPerHour();
            fixedTimeTaskService.setTaskPerMinute();
            #endregion
        }

        public List<string> ServerListToString
        {
            get { return serverListToString; }
            set
            {
                serverListToString = value;
                this.OnPropertyChanged("ServerListToString");
            }
        }

        public ObservableCollection<Node> Tree
        {
            get { return tree; }
            set
            {
                tree = value;
                this.OnPropertyChanged("Tree");
            }
        }

        public ObservableCollection<ListViewItem> ListViewItemList
        {
            get { return listViewItemList; }
            set
            {
                listViewItemList = value;
                this.OnPropertyChanged("ListViewItemList");
            }
        }

        public string SelectedServer
        {
            get { return selectedServer; }
            set
            {
                selectedServer = value;
                this.OnPropertyChanged("SelectedServer");
            }
        }

        public bool Btn_connect_isEnabled
        {
            get { return btn_connect_isEnabled; }
            set
            {
                btn_connect_isEnabled = value;
                this.OnPropertyChanged("Btn_connect_isEnabled");
            }
        }

        public bool Btn_read_isEnabled
        {
            get { return btn_read_isEnabled; }
            set
            {
                btn_read_isEnabled = value;
                this.OnPropertyChanged("Btn_read_isEnabled");
            }
        }

        public bool Btn_stop_isEnabled
        {
            get { return btn_stop_isEnabled; }
            set
            {
                btn_stop_isEnabled = value;
                this.OnPropertyChanged("Btn_stop_isEnabled");
            }
        }

        public string ColorBar_text
        {
            get { return colorBar_text; }
            set
            {
                colorBar_text = value;
                this.OnPropertyChanged("ColorBar_text");
            }
        }

        public string ColorBar_color
        {
            get { return colorBar_color; }
            set
            {
                colorBar_color = value;
                this.OnPropertyChanged("ColorBar_color");
            }
        }

        public string ConsoleText
        {
            get { return consoleText; }
            set
            {
                consoleText = value;
                this.OnPropertyChanged("ConsoleText");
            }
        }

        private void ConnectCommandExecute(object parameter)
        {
            this.Btn_connect_isEnabled = false;
            object serverList = da.GetLocalServer();
            this.ConsoleText += "本地OPC服务器获取完成。\n";
            List<String> tmpList = new List<string>();
            foreach (string turn in (Array)serverList)
            {
                tmpList.Add(turn);
            }
            this.ServerListToString = tmpList;

            da.InitialConnection(this.selectedServer);
            this.ConsoleText += "已连接本地OPC服务器。\n";

            this.Tree = get_tree(da.GetPLCDevices());
            this.Btn_read_isEnabled = true;
            this.ColorBar_text = "OPC已连接";
        }

        private void ReadCommandExecute(object parameter)
        {
            this.ColorBar_color = "#9C5333";
            this.ColorBar_text = "监测服务已启动";
            
            this.Btn_read_isEnabled = false;

            if (null == da.MyGroups)
            {
                this.initialDic();
                this.ConsoleText += "PLC点表初始化完成。\n";

                channel_device_list = new List<string>();

                foreach (Node parent_node in this.tree)
                {
                    if (parent_node.IsChecked == true)
                    {
                        foreach (Node child_node in parent_node.ChildList)
                        {
                            if (child_node.IsChecked == true)
                                channel_device_list.Add(parent_node.NodeName + "." + child_node.NodeName);
                        }
                    }
                }
                da.CreateGroup(channel_device_list);
            }

            for (int i = 0; i < channel_device_list.Count; i++)
                da.MyGroups[i].DataChange += new DIOPCGroupEvent_DataChangeEventHandler(GroupDataChange);

            this.ConsoleText += "开始读取数据。\n";
            this.Btn_stop_isEnabled = true;

            // 新建一个线程，保证加载PLC过程中窗体不会假死。
            /*new Thread(o =>
            {
                if (null == da.MyGroups)
                    da.CreateGroup(channel_device_list);

                for (int i = 0; i < channel_device_list.Count; i++)
                    da.MyGroups[i].DataChange += new DIOPCGroupEvent_DataChangeEventHandler(GroupDataChange);
            })
            {
                IsBackground = true
            }.Start();*/
        }

        private void StopCommandExecute(object parameter)
        {
            this.Btn_stop_isEnabled = true;

            for (int i = 0; i < channel_device_list.Count; i++)
                da.MyGroups[i].DataChange -= new DIOPCGroupEvent_DataChangeEventHandler(GroupDataChange);
            
            this.ConsoleText += "数据数据读取已停止。\n";
            this.Btn_read_isEnabled = true;
        }

        /// <summary>
        /// 将 da.GetPLCDevices() 的返回值转换为TreeView的ItemsSource
        /// </summary>
        /// <param name="channel_device_list"></param>
        /// <returns></returns>
        private ObservableCollection<Node> get_tree(List<string> channel_device_list)
        {
            //将 通道.设备List 转为 通道.设备Dictionary
            Dictionary<string, List<string>> channel_device_dic = new Dictionary<string, List<string>>();
            foreach (string s in channel_device_list)
            {
                int pos = s.IndexOf(".");
                string channel = s.Substring(0, pos);
                string device = s.Substring(pos + 1);
                if (!channel_device_dic.ContainsKey(channel))
                {
                    channel_device_dic.Add(channel, new List<string>());
                    channel_device_dic[channel].Add(device);
                }
                else
                {
                    channel_device_dic[channel].Add(device);
                }
            }

            //将 通道.设备Dictionary 转为 tree
            ObservableCollection<Node> tree = new ObservableCollection<Node>();
            foreach (string key in channel_device_dic.Keys)
            {
                Node channel_node = new Node(key);
                List<string> deviceList = channel_device_dic[key];

                ObservableCollection<Node> device_node_col = new ObservableCollection<Node>();
                
                foreach (string device_name in deviceList)
                    device_node_col.Add(new Node(device_name, channel_node));
                
                channel_node.ChildList = device_node_col;
                tree.Add(channel_node);
            }
            return tree;
        }

        /// <summary>
        /// 初始化字典
        /// </summary>
        private void initialDic()
        {
            //初始化dic
            foreach(Node channel in tree)
            {
                foreach(Node device in channel.ChildList)
                {
                    List<string> itemList = CSVUtils.getTpNames(device.NodeName);
                    foreach (string itemName in itemList)
                        input.Dic.Add(channel.NodeName+"."+ device.NodeName+"."+itemName, "0");
                }
            }
        }

        /// <summary>
        /// 每当项数据有变化时执行的事件,从1开始
        /// </summary>
        /// <param name="TransactionID">处理ID</param>
        /// <param name="NumItems">项个数</param>
        /// <param name="ClientHandles">项客户端句柄</param>
        /// <param name="ItemValues">TAG值</param>
        /// <param name="Qualities">品质</param>
        /// <param name="TimeStamps">时间戳</param>
        void GroupDataChange(int TransactionID, int NumItems, ref Array ClientHandles, ref Array ItemValues, ref Array Qualities, ref Array TimeStamps)
        {
            #region 初始化参数
            ///////////////////////////////////////////////////////
            Array clientHandles = ClientHandles;
            Array itemValues = ItemValues;
            ///////////////////////////////////////////////////////

            /*  存储相应信息
            * time         时间戳
            * channel      通道名称
            * devicename   设备名称
            * ItemName     标记名称
            */
            string time = System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            string[] channel = new string[NumItems];
            string[] device = new string[NumItems];
            string[] itemName = new string[NumItems];
            for (int i = 1; i <= NumItems; i++)
            {
                channel[i - 1] = (da.htOpcItems[(int)clientHandles.GetValue(i) - 1] + "").Split(new char[] { '.' })[0];
                device[i - 1] = (da.htOpcItems[(int)clientHandles.GetValue(i) - 1] + "").Split(new char[] { '.' })[1];
                itemName[i - 1] = (da.htOpcItems[(int)clientHandles.GetValue(i) - 1] + "").Split(new char[] { '.' })[2];
            }
            #endregion

            #region 数据展示
            for (int i = 1; i <= NumItems; i++)
            {
                if (this.ListViewItemList.Count >= 100)
                    this.ListViewItemList.RemoveAt(0);

                this.ListViewItemList.Add(new ListViewItem(channel[i - 1] + "." + device[i - 1] + "." + itemName[i - 1], itemValues.GetValue(i).ToString(), ((int)clientHandles.GetValue(i) - 1).ToString(), time));
            }
            //this.listView.ScrollIntoView(this.listView.Items[this.listView.Items.Count - 1]);
            #endregion

            #region 数据存储
            //重置时间判断列表每一项的标志位
            timeJudgeItemList.resetFlags();

            for (int i = 0; i < NumItems; i++)
            {
                set_data_to_dic(channel[i], device[i], itemName[i], itemValues.GetValue(i + 1).ToString());
                // 将故障信息存入数据库
                if (channel[i] == "故障测试" && itemValues.GetValue(i + 1).ToString() == "True")
                    Gz.save(dataSaveService.O,device[i], itemName[i]);
            }

            //重置时间判断列表每一项的时间信息
            timeJudgeItemList.resetTimeList();

            //将出故障外的PLC数据存储到数据库
            dataSaveService.savePLCData(ref input, timeJudgeItemList);
            #endregion
        }

        /// <summary>
        /// 将PLC数据设置到字典中
        /// </summary>
        /// <param name="channel"></param>
        /// <param name="device"></param>
        /// <param name="itemName"></param>
        /// <param name="itemValue"></param>
        public void set_data_to_dic(string channel, string device, string itemName, string itemValue)
        {
            string key = channel + "." + device + "." + itemName;
            if (input.Dic.ContainsKey(key))
                input.Dic[key] = itemValue;
        }
    }
}
