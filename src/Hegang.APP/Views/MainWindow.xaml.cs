using Hegang.APP.Models;
using Hegang.APP.Services.DbService;
using Hegang.APP.ViewModels;
using Hegang.APP.Views;
using HFD.KEPWare;
using OPCAutomation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Threading;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;

namespace Hegang.APP
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        #region 窗体成员
        private DataBaseConfig dbcWindow;
        private Login loginWindow;
        private PLCItemConfig picWindow;
        private UserManage umWindow;
        private PerInfoManage piWindow;
        #endregion

        #region 普通私有成员变量
        private User user;
        private KEPWareDataAdapter da;
        private DbServiceInput input;
        private DataSaveService dataSaveService;
        private List<string> channel_device_list;
        private TimeJudgeItemList timeJudgeItemList;
        private FixedTimeTaskService fixedTimeTaskService;
        private DateTime nowTime;
        #endregion

        #region 构造函数
        public MainWindow()
        {
            InitializeComponent();
            this.initMembers();
        }

        public MainWindow(User _user,bool flag)
        {
            InitializeComponent();
            this.initMembers();

            if (flag)
                this.u_man.Visibility =Visibility.Collapsed;

            this.user = _user;
            this.Title = string.Format("{0}您好！欢迎使用鸟山矿PLC监测服务程序", user.UserName);
        }
        #endregion

        public void initMembers()
        {
            this.color_bar.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#065279"));
            this.color_bar_text.Text = "就绪";
            nowTime = DateTime.Now;
            fixedTimeTaskService = new FixedTimeTaskService();
        }

        public void initDbMembers()
        {
            if(dataSaveService==null)
                dataSaveService = new DataSaveService(); 
            if(timeJudgeItemList==null)
                timeJudgeItemList = new TimeJudgeItemList();
        }

        #region 窗体控制按钮
        /// <summary>
        /// 鼠标左键按住可以移动窗体
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        /// <summary>
        /// 窗体最小化
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void minimize_btn_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        /// <summary>
        /// 关闭整个程序
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void close_btn_Click(object sender, RoutedEventArgs e)
        {
            Environment.Exit(0);
        }
        #endregion

        #region 其他按钮
        /// <summary>
        /// 显示数据库配置窗体
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_DBConfig_Click(object sender, RoutedEventArgs e)
        {
            DataBaseConfig dataBaseConfig = new DataBaseConfig();
            dataBaseConfig.Owner = this;
            dataBaseConfig.ShowDialog();
        }

        private void db_conf_Click(object sender, RoutedEventArgs e)
        {
            this.dbcWindow = new DataBaseConfig();
            this.dbcWindow.ShowDialog();
            if (!string.IsNullOrEmpty(dbcWindow.Rtn_msg))
                this.color_bar_text.Text = dbcWindow.Rtn_msg;
        }

        private void tp_conf_Click(object sender, RoutedEventArgs e)
        {
            this.picWindow = new PLCItemConfig();
            this.picWindow.ShowDialog();
        }

        private void u_man_Click(object sender, RoutedEventArgs e)
        {
            this.umWindow = new UserManage();
            this.umWindow.ShowDialog();
        }

        private void pi_man_Click(object sender, RoutedEventArgs e)
        {
            this.piWindow = new PerInfoManage(user);
            this.piWindow.ShowDialog();
            if (!string.IsNullOrEmpty(piWindow.Rtn_msg))
                this.color_bar_text.Text = piWindow.Rtn_msg;
        }

        private void help_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void exit_Click(object sender, RoutedEventArgs e)
        {
            this.loginWindow = new Login();
            this.Close();
            this.loginWindow.ShowDialog();
        }

        private void btn_connect_Click(object sender, RoutedEventArgs e)
        {
            da = new KEPWareDataAdapter();
            this.btn_connect.IsEnabled = false;
            this.console_tb.Text += "KEPWareDataAdapter对象已完成初始化。\n";
            
            object serverList = da.GetLocalServer();
            List<String> tmpList = new List<string>();
            foreach (string turn in (Array)serverList)
            {
                tmpList.Add(turn);
            }
            this.cmb_server_list.ItemsSource = tmpList;
            this.cmb_server_list.SelectedIndex = 0;
            this.console_tb.Text += "本地OPC服务器获取完成。\n";

            if (!string.IsNullOrEmpty(this.cmb_server_list.SelectedItem.ToString()))
            {
                da.InitialConnection(this.cmb_server_list.SelectedItem.ToString());
                this.console_tb.Text += "已连接本地OPC服务器。\n";

                this.chk_treeview.ItemsSource = get_tree(da.GetPLCDevices());
                this.btn_read.IsEnabled = true;
                this.color_bar_text.Text = "OPC已连接";
            }
            else
            {
                this.console_tb.Text += "没有选择任何本地OPC服务器。\n";
            }
        }

        private void btn_read_Click(object sender, RoutedEventArgs e)
        {
            channel_device_list = new List<string>();
            foreach (Node parent_node in this.chk_treeview.Items)
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
            if (channel_device_list.Count == 0)
            {
                Tip tip = new Tip("请选择需要监测的PLC设备！");
                tip.ShowDialog();
                return;
            }

            if (input == null)
                initialDic();

            da.CreateGroup(channel_device_list);

            for (int i = 0; i < channel_device_list.Count; i++)
                da.MyGroups[i].DataChange += new DIOPCGroupEvent_DataChangeEventHandler(GroupDataChange);

            #region 结果更新
            this.color_bar.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#9C5333"));
            this.color_bar_text.Text = "监测服务已启动";
            this.console_tb.Text += "开始读取数据。\n";
            this.btn_stop.IsEnabled = true;
            this.btn_read.IsEnabled = false;
            this.menu.IsEnabled = false;
            #endregion

            #region 记录：创建线程执行任务
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
            #endregion
        }

        private void btn_stop_Click(object sender, RoutedEventArgs e)
        {
            this.btn_stop.IsEnabled = false;

            //不同控件的清空方式有所差异
            this.listView.Items.Clear();
            this.cmb_server_list.ItemsSource = null;
            this.chk_treeview.ItemsSource = null;

            for (int i = 0; i < channel_device_list.Count; i++)
                da.MyGroups[i].DataChange -= new DIOPCGroupEvent_DataChangeEventHandler(GroupDataChange);

            da.Close();
            this.console_tb.Text += "KEPWareAdapter资源已释放。\n";
            this.console_tb.Text += "数据数据读取已停止。\n";
            this.btn_connect.IsEnabled = true;
            this.menu.IsEnabled = true;
            this.color_bar.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#065279"));
            this.color_bar_text.Text = "就绪";
        }

        private void d_save_Click(object sender, RoutedEventArgs e)
        {
            initDbMembers();
        }

        private void d_stat_Click(object sender, RoutedEventArgs e)
        {
            initDbMembers();
            if (this.d_stat.IsChecked)
            {
                fixedTimeTaskService.Stat_isEnabled = true;
                fixedTimeTaskService.setTaskAtZero();
                fixedTimeTaskService.setTaskPerHour();
            }
            else
            {
                fixedTimeTaskService.Stat_isEnabled = false;
            }
        }

        private void d_fore_Click(object sender, RoutedEventArgs e)
        {
            initDbMembers();
            if (this.d_fore.IsChecked)
            {
                fixedTimeTaskService.Fore_isEnabled = true;
                fixedTimeTaskService.setTaskPerMinute();
            }
            else
            {
                fixedTimeTaskService.Stat_isEnabled = false;
            }
        }
        #endregion

        private void listView_ScrollChanged(object sender, System.Windows.Controls.ScrollChangedEventArgs e)
        {
            /*if (this.listView.Items.Count != 0)
                this.listView.ScrollIntoView(this.listView.Items[this.listView.Items.Count - 1]);*/
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
            input = new DbServiceInput();
            //初始化dic
            foreach (Node channel in this.chk_treeview.Items)
            {
                foreach (Node device in channel.ChildList)
                {
                    List<string> itemList = CSVUtils.getTpNames(device.NodeName);
                    if (itemList != null)
                    {
                        foreach (string itemName in itemList)
                            input.Dic.Add(channel.NodeName + "." + device.NodeName + "." + itemName, "0");
                    }
                    else
                    {
                        this.console_tb.Text += "对应点表不存在！\n";
                    }
                }
            }
            this.console_tb.Text += "PLC点表初始化完成。\n";
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
            //ListView设置1s更新一次，如果不设置更新间隔，会出现"高度必须为非负值"错误
            if ((DateTime.Now - nowTime).TotalMilliseconds > 200)
            {
                for (int i = 1; i <= NumItems; i++)
                {
                    if (this.listView.Items.Count >= 100)
                        this.listView.Items.RemoveAt(0);
                    Console.WriteLine(channel[i - 1] + "." + device[i - 1] + "." + itemName[i - 1] + " " + itemValues.GetValue(i).ToString() + " " + ((int)clientHandles.GetValue(i) - 1).ToString() + " " + time);
                    this.listView.Items.Add(new ListViewItem(channel[i - 1] + "." + device[i - 1] + "." + itemName[i - 1], itemValues.GetValue(i).ToString(), ((int)clientHandles.GetValue(i) - 1).ToString(), time));

                }
                //滚动条自动移动到最下方
                //this.listView.ScrollIntoView(this.listView.Items[this.listView.Items.Count - 1]);
                nowTime = DateTime.Now;
            }
            #endregion

            #region 数据存储
            if (this.d_save.IsChecked||this.d_stat.IsChecked||this.d_fore.IsChecked)
            {
                //重置时间判断列表每一项的标志位
                timeJudgeItemList.resetFlags();

                for (int i = 0; i < NumItems; i++)
                {
                    set_data_to_dic(channel[i], device[i], itemName[i], itemValues.GetValue(i + 1).ToString());
                    // 将故障信息存入数据库
                    if (channel[i] == "故障测试" && itemValues.GetValue(i + 1).ToString() == "True")
                        Gz.save(dataSaveService.O, device[i], itemName[i]);
                }

                //重置时间判断列表每一项的时间信息
                timeJudgeItemList.resetTimeList();

                //将出故障外的PLC数据存储到数据库
                dataSaveService.savePLCData(ref input, timeJudgeItemList);
            }
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
