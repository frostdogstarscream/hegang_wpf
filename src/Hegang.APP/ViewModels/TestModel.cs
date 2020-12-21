using Hegang.APP.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hegang.APP.ViewModels
{
    class TestModel:NotificationObject
    {
        private ObservableCollection<ListViewItem> listViewItems;
        

        public DelegateCommand ModifyCommand { get; set; }

        public ObservableCollection<ListViewItem> ListViewItems 
        {
            get 
            { 
                return listViewItems; 
            }
            set 
            { 
                listViewItems = value;
                this.OnPropertyChanged("ListViewItems");
            }
        }

        public TestModel()
        {
            listViewItems = new ObservableCollection<ListViewItem>();
            listViewItems.Add(new ListViewItem("温度", "23", "12", "2020-12-20 22:14"));
            listViewItems.Add(new ListViewItem("振动", "1", "10", "2020-12-20 22:15"));

            this.ModifyCommand = new DelegateCommand();
            this.ModifyCommand.ExcuteAction = new Action<object>(this.ModifyCommandExecute);
        }

        private void ModifyCommandExecute(object parameter)
        {
            Console.WriteLine("Hello world!");
            listViewItems.RemoveAt(0);
            listViewItems.Add(new ListViewItem("温度", "30", "12", "2020-12-20 22:14"));
        }
    }
}
