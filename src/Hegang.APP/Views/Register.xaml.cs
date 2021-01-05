using Hegang.APP.Models.DataBase;
using Hegang.APP.ViewModels;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Reflection;
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

namespace Hegang.APP.Views
{
    /// <summary>
    /// Register.xaml 的交互逻辑
    /// </summary>
    public partial class Register : Window
    {
        private DbObject o;

        #region 窗体成员
        private MainWindow mainWindow;
        private Tip tipWindow;
        #endregion

        public Register()
        {
            InitializeComponent();

            string db_str = ConfigurationManager.AppSettings["DB"];
            o = (DbObject)Assembly.Load("Hegang.APP").CreateInstance(db_str);
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
        /// 关闭整个程序
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void close_btn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btn_reg_Click(object sender, RoutedEventArgs e)
        {
            bool flag = this.id_tb.Text == "" || this.userName_tb.Text=="" || this.pwd_tb.Text == "" || this.age_tb.Text == "" || this.nation_tb.Text == "" || this.department_tb.Text == "";
            if (flag)
            {
                this.tipWindow = new Tip("信息输入不完整！");
                this.tipWindow.ShowDialog();
                return;
            }

            string str = string.Format("SELECT COUNT(id) FROM user WHERE id = '{0}'",this.id_tb.Text);
            DataTable dt = o.GetDataTable(str);
            if(Convert.ToInt32(dt.Rows[0][0])!= 0)
            {
                this.tipWindow = new Tip("该工号已注册！");
                this.tipWindow.ShowDialog();
                return;
            }
            else
            {
                str = string.Format("INSERT INTO user(id, userName, pwd, age, nation, department) VALUES('{0}','{1}','{2}','{3}','{4}','{5}');",
                this.id_tb.Text, this.userName_tb.Text, this.pwd_tb.Text, this.age_tb.Text, this.nation_tb.Text, this.department_tb.Text);
                o.cmmdNoReturn(str);
                this.tipWindow = new Tip("注册成功！");
                this.tipWindow.ShowDialog();

                User user = new User();
                user.Id = this.id_tb.Text;
                user.UserName = this.userName_tb.Text;
                user.Pwd = this.pwd_tb.Text;
                user.Age = this.age_tb.Text;
                user.Nation = this.nation_tb.Text;
                user.Department = this.department_tb.Text;

                this.mainWindow = new MainWindow(user,true);
                this.Close();
                this.mainWindow.Show();
            }
        }
    }
}
