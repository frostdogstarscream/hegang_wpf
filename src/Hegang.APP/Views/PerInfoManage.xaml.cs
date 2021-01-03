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
    /// PerInfoManage.xaml 的交互逻辑
    /// </summary>
    public partial class PerInfoManage : Window
    {
        private DbObject o;
        private User user;
        public PerInfoManage(User _user)
        {
            InitializeComponent();

            string db_str = ConfigurationManager.AppSettings["DB"];
            o = (DbObject)Assembly.Load("Hegang.APP").CreateInstance(db_str);

            this.user = _user;
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
            string str = string.Format("UPDATE user SET userName = '{0}', pwd = '{1}', age='{2}', nation='{3}',department='{4}' WHERE id = '{5}'",
                this.userName_tb.Text, this.pwd_tb.Text, this.age_tb.Text, this.nation_tb.Text, this.department_tb.Text, user.Id);
            o.cmmdNoReturn(str);
        }
    }
}
