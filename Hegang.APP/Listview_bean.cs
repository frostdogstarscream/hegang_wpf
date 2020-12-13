using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hegang.APP
{
    class Listview_bean : INotifyPropertyChanged
    {
        private string item_name;
        private string item_value;
        private string item_handle;
        private string item_timestamp;

        public Listview_bean(string item_name,string item_value,string item_handle,string item_timestamp)
        {
            this.item_name = item_name;
            this.item_value = item_value;
            this.Item_handle = item_handle;
            this.item_timestamp = item_timestamp;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, e);
        }

        public string Item_name{
            get { return item_name; }
            set { 
                item_name = value;
                OnPropertyChanged(new PropertyChangedEventArgs("Item_name"));
            }
        }

        public string Item_value
        {
            get { return item_value; }
            set
            {
                item_value = value;
                OnPropertyChanged(new PropertyChangedEventArgs("Item_value"));
            }
        }

        public string Item_handle
        {
            get { return item_handle; }
            set
            {
                item_handle = value;
                OnPropertyChanged(new PropertyChangedEventArgs("Item_handle"));
            }
        }

        public string Item_timestamp
        {
            get { return item_timestamp; }
            set
            {
                item_timestamp = value;
                OnPropertyChanged(new PropertyChangedEventArgs("Item_timestamp"));
            }
        }

        public static List<Listview_bean> test()
        {
            List<Listview_bean> list = new List<Listview_bean>();
            list.Add(new Listview_bean("温度", "23", "2", "2020/12/13 13:22"));
            list.Add(new Listview_bean("高度", "850", "4", "2020/12/13 13:23"));
            return list;
        }

    }
}
