using Hegang.APP.Models.DataBase;
using Hegang.APP.ViewModels;
using System;
using System.Collections.Generic;
using System.Configuration;
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
    /// ModifyUser.xaml 的交互逻辑
    /// </summary>
    public partial class ModifyUser : Window
    {
        private User user;
        private DbObject o;
        /**
         * flag为 true 代表修改用户信息
         * flag为 false 代表添加用户
         */
        private bool flag;

        public ModifyUser()
        {
            InitializeComponent();

            string db_str = ConfigurationManager.AppSettings["DB"];
            o = (DbObject)Assembly.Load("Hegang.APP").CreateInstance(db_str);

            this.flag = false;
        }

        public ModifyUser(User _user)
        {
            InitializeComponent();

            string db_str = ConfigurationManager.AppSettings["DB"];
            o = (DbObject)Assembly.Load("Hegang.APP").CreateInstance(db_str);

            this.User = _user;
            this.userName_tb.Text = user.UserName;
            this.nation_tb.Text = user.Nation;
            this.pwd_tb.Text = user.Pwd;
            this.department_tb.Text = user.Department;
            this.age_tb.Text = user.Age;

            this.flag = true;
        }

        internal User User { get => user; set => user = value; }

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
        /// 关闭窗体
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void close_btn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btn_modify_Click(object sender, RoutedEventArgs e)
        {
            string str;
            
            if (this.flag)
                str = string.Format("UPDATE user SET userName = '{0}', pwd = '{1}', age = '{2}', nation = '{3}', department = '{4}' WHERE id = '{5}';",
                    this.userName_tb.Text, this.pwd_tb.Text, this.age_tb.Text, this.nation_tb.Text, this.department_tb.Text, user.Id);
            else
                str = string.Format("INSERT INTO user(userName, pwd, age, nation, department) VALUES('{0}','{1}','{2}','{3}','{4}');",
                    this.userName_tb.Text, this.pwd_tb.Text, this.age_tb.Text, this.nation_tb.Text, this.department_tb.Text);
            
            o.cmmdNoReturn(str);
            this.Close();
        }
    }
}
