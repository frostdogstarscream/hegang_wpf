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

            /*users = new ObservableCollection<User>();
            users.Add(new User("973480764", "张驰", "123456", "24", "汉族", "计算机应用技术"));
            users.Add(new User("973480765", "王赵文", "123456", "25", "汉族", "计算机应用技术"));*/
            
            string db_str = ConfigurationManager.AppSettings["DB"];
            o = (DbObject)Assembly.Load("Hegang.APP").CreateInstance(db_str);

            listViewInit();
            
        }

        private void listViewInit()
        {
            string str = "SELECT id,userName,pwd,age,nation,department FROM user;";
            DataTable dt = o.GetDataTable(str);

            users = new ObservableCollection<User>();
            for (int i = 0; i < dt.Rows.Count; i++)
                users.Add(new User(dt.Rows[i][0].ToString(), dt.Rows[i][1].ToString(), dt.Rows[i][2].ToString(), dt.Rows[i][3].ToString(), dt.Rows[i][4].ToString(), dt.Rows[i][5].ToString()));
            this.listView.ItemsSource = users;
        }

        private void modify_btn_Click(object sender, RoutedEventArgs e)
        {
            var btn = sender as Button;
            var user = btn.DataContext as User;
            ModifyUser modifyUser = new ModifyUser(user);
            modifyUser.Closed += new EventHandler(modifyUserClosed);
            modifyUser.ShowDialog();
        }

        private void modifyUserClosed(object sender, EventArgs e)
        {
            listViewInit();
        }
        

        private void delete_btn_Click(object sender, RoutedEventArgs e)
        {
            var btn = sender as Button;
            var user = btn.DataContext as User;

            string str = string.Format("DELETE FROM user WHERE id = '{0}';",user.Id);
            o.cmmdNoReturn(str);

            users.Remove(user);
            listViewInit();
        }

        private void add_btn_Click(object sender, RoutedEventArgs e)
        {
            ModifyUser modifyUser = new ModifyUser();
            modifyUser.Closed += new EventHandler(modifyUserClosed);
            modifyUser.ShowDialog();
        }
    }
}
