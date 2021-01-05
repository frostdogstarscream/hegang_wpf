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

        #region 窗体成员
        private Register regWindow;
        private Tip tipWindow;
        private MainWindow mainWindow;
        #endregion

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
            //Environment.Exit(0);
            this.Close();
        }

        private void login_btn_Click(object sender, RoutedEventArgs e)
        {
            if (this.id.Text == "" || this.pwd.Password == "")
            {
                this.tipWindow = new Tip("信息输入不完整！");
                this.tipWindow.ShowDialog();
                return;
            }
            
            string str;
            bool flag = true;

            if (this.user_rb.IsChecked==true)
                str = string.Format("SELECT userName,age,nation,department FROM `user` WHERE id = '{0}' AND pwd = '{1}'", this.id.Text, this.pwd.Password);
            else
            {
                str = string.Format("SELECT userName FROM `admin` WHERE id = '{0}' AND pwd = '{1}'", this.id.Text, this.pwd.Password);
                flag = false;
            }
                
            
            DataTable dt = o.GetDataTable(str);
            if (dt.Rows.Count == 1)
            {
                User user = new User();
                user.Id = this.id.Text;
                user.UserName = dt.Rows[0][0].ToString();
                user.Pwd = this.pwd.Password;
                if (flag)
                {
                    user.Age = dt.Rows[0][1].ToString();
                    user.Nation = dt.Rows[0][2].ToString();
                    user.Department = dt.Rows[0][3].ToString();
                }
                
                this.mainWindow = new MainWindow(user,flag);
                /*Window window = Window.GetWindow(this);//关闭父窗体
                window.Close();*/
                this.Close();
                this.mainWindow.Show();
            }
            else
            {
                this.tipWindow = new Tip("用户名或密码不正确！");
                this.tipWindow.ShowDialog();
            }
                
        }

        private void reg_btn_Click(object sender, RoutedEventArgs e)
        {
            this.regWindow = new Register();
            this.Close();
            this.regWindow.ShowDialog();
        }
    }
}
