using Hegang.APP.Models.DataBase;
using Hegang.APP.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// UserManage.xaml 的交互逻辑
    /// </summary>
    public partial class UserManage : Window
    {
        private ObservableCollection<User> users;
        private DbObject o;
        public UserManage()
        {
            InitializeComponent();

            string db_str = ConfigurationManager.AppSettings["DB"];
            o = (DbObject)Assembly.Load("Hegang.APP").CreateInstance(db_str);

            usersInit();
            this.listView.ItemsSource = users;
        }

        private void usersInit()
        {
            string str = "SELECT id,userName,pwd,age,nation,department FROM users;";
            DataTable dt = o.GetDataTable(str);

            users = new ObservableCollection<User>();
            for (int i = 0; i < dt.Rows.Count; i++)
                users.Add(new User(dt.Rows[i][0].ToString(), dt.Rows[i][1].ToString(), dt.Rows[i][2].ToString(), dt.Rows[i][3].ToString(), dt.Rows[i][4].ToString(), dt.Rows[i][5].ToString()));
        }

        private void modify_btn_Click(object sender, RoutedEventArgs e)
        {
            var btn = sender as Button;
            var user = btn.DataContext as User;
            
        }

        private void delete_btn_Click(object sender, RoutedEventArgs e)
        {
            var btn = sender as Button;
            var user = btn.DataContext as User;

            string str = string.Format("DELETE FROM users WHERE id = '{0}';",user.Id);
            o.cmmdNoReturn(str);

            users.Remove(user);
        }
    }
}
