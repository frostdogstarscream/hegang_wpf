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
        private User user;

        #region 窗体成员
        private DataBaseConfig dbcWindow;
        private Login loginWindow;
        private PLCItemConfig picWindow;
        private UserManage umWindow;
        private PerInfoManage piWindow;
        #endregion


        public MainWindow(User _user,bool flag)
        {
            InitializeComponent();
            this.DataContext = new MainWindowViewModel();
            
            if (flag)
                this.u_man.Visibility =Visibility.Collapsed;

            //this.listViewScroll();

            this.user = _user;

            this.Title = string.Format("{0}您好！欢迎使用鸟山矿PLC监测服务程序", user.UserName);
            
            
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

        public void listViewScroll(){
            DateTime now = DateTime.Now;
            //设置任务启动时间  
            double hour = Convert.ToDouble(DateTime.Now.Hour);
            double minute = Convert.ToDouble(DateTime.Now.Minute);
            double second = Convert.ToDouble(DateTime.Now.Second);
            double millSecond= Convert.ToDouble(DateTime.Now.Millisecond+250);



            DateTime startTime = DateTime.Today.AddHours(hour).AddMinutes(minute).AddSeconds(second).AddMilliseconds(millSecond);

            int delay = (int)((startTime - now).TotalMilliseconds);
            var t = new System.Threading.Timer(handle_event);
            //设置线程参数 任务delay毫秒后启动
            t.Change(delay, Timeout.Infinite);
        }

        private void handle_event(object state)
        {
            // 执行任务
            if(this.listView.Items.Count!=0)
                this.listView.ScrollIntoView(this.listView.Items[this.listView.Items.Count - 1]);
            // 再次设定任务执行时间
            listViewScroll();
        }

        private void listView_ScrollChanged(object sender, System.Windows.Controls.ScrollChangedEventArgs e)
        {
            /*if (this.listView.Items.Count != 0)
                this.listView.ScrollIntoView(this.listView.Items[this.listView.Items.Count - 1]);*/
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
    }
}
