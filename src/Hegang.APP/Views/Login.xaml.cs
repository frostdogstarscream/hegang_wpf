using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Reflection;
using Hegang.APP.Services.DbService;
using System.Configuration;
using Hegang.APP.Models.DataBase;
using System.Data;
using Hegang.APP.ViewModels;

namespace Hegang.APP.Views
{
    /// <summary>
    /// Login.xaml 的交互逻辑
    /// </summary>
    public partial class Login : Window
    {
        private DbObject o;
        public Login()
        {
            InitializeComponent();
            string db_str = ConfigurationManager.AppSettings["DB"];
            o = (DbObject)Assembly.Load("Hegang.APP").CreateInstance(db_str);
            this.DataContext = new LoginViewModel();

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
        private void min_btn_Click(object sender, RoutedEventArgs e)
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

        private void login_btn_Click(object sender, RoutedEventArgs e)
        {
            string str = string.Format("SELECT COUNT(id) FROM `user` WHERE userName = '{0}' AND pwd = '{1}'", this.userName.Text, this.pwd.Password);
            DataTable dt = o.GetDataTable(str);
            if (Convert.ToInt32(dt.Rows[0][0]) == 1)
            {
                MainWindow mainWindow = new MainWindow();
                Window window = Window.GetWindow(this);//关闭父窗体
                window.Close();
                mainWindow.ShowDialog();
            }
            else
                Console.WriteLine("登陆失败");
        }
    }
}
