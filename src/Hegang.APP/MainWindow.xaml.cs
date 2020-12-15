using HFD.KEPWare;
using OPCAutomation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace Hegang.APP
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        private KEPWareDataAdapter da;
        private ObservableCollection<Node> tree;

        public MainWindow()
        {
            InitializeComponent();
            da = new KEPWareDataAdapter();
            this.btn_stop.IsEnabled = false;
            this.btn_read.IsEnabled = false;
        }

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
        /// 加载并连接OPC
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_connect_Click(object sender, RoutedEventArgs e)
        {
            this.btn_connect.IsEnabled = false;

            object serverList = da.GetLocalServer();
            List<string> serverListToString = new List<string>();
            foreach (string turn in (Array)serverList)
            {
                serverListToString.Add(turn);
            }
            this.cmb_server_list.ItemsSource = serverListToString;
            da.InitialConnection(cmb_server_list.Text);

            this.tree = get_tree(da.GetPLCDevices());
            chk_treeview.ItemsSource = this.tree;

            this.color_bar_text.Text = "OPC已连接";
            this.btn_read.IsEnabled = true;
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
                {
                    device_node_col.Add(new Node(device_name, channel_node));
                }
                channel_node.ChildList = device_node_col;
                tree.Add(channel_node);
            }
            return tree;
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
            Array clientHandles = ClientHandles;
            Array itemValues = ItemValues;
            /*  获取相应信息
            * time         时间戳
            * channel      通道名称
            * devicename   设备名称
            * ItemName     标记名称
            */
            string time = System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            string[] channel = new string[NumItems];
            string[] deviceName = new string[NumItems];
            string[] itemName = new string[NumItems];
            for (int i = 1; i <= NumItems; i++)
            {
                channel[i - 1] = (da.htOpcItems[(int)clientHandles.GetValue(i) - 1] + "").Split(new char[] { '.' })[0];
                deviceName[i - 1] = (da.htOpcItems[(int)clientHandles.GetValue(i) - 1] + "").Split(new char[] { '.' })[1];
                itemName[i - 1] = (da.htOpcItems[(int)clientHandles.GetValue(i) - 1] + "").Split(new char[] { '.' })[2];
            }
            #endregion

            #region 数据展示
            for (int i = 1; i <= NumItems; i++)
            {
                if (this.listView.Items.Count >= 200)
                {
                    this.listView.Items.RemoveAt(0);
                }
                this.listView.Items.Add(new ListViewItem(itemName[i - 1], itemValues.GetValue(i).ToString(), ((int)clientHandles.GetValue(i) - 1).ToString(), time));
            }
            this.listView.ScrollIntoView(this.listView.Items[this.listView.Items.Count-1]);
            #endregion
        }

        /// <summary>
        /// 设置窗体底端颜色条
        /// </summary>
        /// <param name="color_str"></param>
        /// <param name="text"></param>
        private void set_color_bar(string color_str,string text)
        {
            Color color = (Color)ColorConverter.ConvertFromString(color_str);
            this.color_bar.Background = new SolidColorBrush(color);
            this.color_bar_text.Text = text;
        }

        /// <summary>
        /// 读取PLC数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_read_Click(object sender, RoutedEventArgs e)
        {
            set_color_bar("#F36838", "监测服务已启动");

            List<string> channel_device_list = new List<string>();
            foreach(Node parent_node in this.tree)
            {
                if (parent_node.IsChecked == true)
                {
                    foreach(Node child_node in parent_node.ChildList)
                    {
                        if (child_node.IsChecked == true)
                            channel_device_list.Add(parent_node.NodeName + "." + child_node.NodeName);
                    }
                }
            }

            if (channel_device_list.Count == 0)
            {
                Tip tip = new Tip("请选择至少一个PLC设备！");
                tip.Owner = this;
                tip.ShowDialog();
                return;
            }

            this.btn_read.IsEnabled = false;
            this.btn_stop.IsEnabled = true;
            
            // 新建一个线程，保证加载PLC过程中窗体不会假死。
            new Thread(o => {
                da.CreateGroup(channel_device_list);

                for (int i = 0; i < channel_device_list.Count; i++)
                {
                    da.MyGroups[i].DataChange += new DIOPCGroupEvent_DataChangeEventHandler(GroupDataChange);
                }
            })
            {
                IsBackground = true
            }.Start();
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

        /// <summary>
        /// 停止读取PLC数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_stop_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
