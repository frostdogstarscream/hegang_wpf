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

namespace Hegang.APP.Views
{
    /// <summary>
    /// Login.xaml 的交互逻辑
    /// </summary>
    public partial class Login : Window
    {
        public Login()
        {
            InitializeComponent();

            string str = ConfigurationManager.AppSettings["DB"];
            DbObject o = (DbObject)Assembly.Load("Hegang.APP").CreateInstance(str);
            DataTable dt = o.GetDataTable("SELECT * FROM fzd WHERE zd1 > 1000;");
            Console.WriteLine(dt.Rows.Count);

            //Console.WriteLine(ConfigurationManager.AppSettings["DB"]);

            /*Dictionary<string, string> dic = new Dictionary<string, string>();
            DbServiceObject dbServiceObject = (DbServiceObject)Assembly.Load("Hegang.APP").CreateInstance("Hegang.APP.Services.DbService.Mtsjzt_live");
            DbServiceContext context = new DbServiceContext(dbServiceObject);
            
            context.ContextInterface(ref dic);*/

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
    }
}
