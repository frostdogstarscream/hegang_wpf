using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hegang.APP.ViewModels
{
    public class TestPoint:NotificationObject
    {
        private string id;
        private string name;
        private string address;
        private string dataType;
        private string clientAccess;
        private string scanRate;

        public string Id
        {
            get => id;
            set
            {
                id = value;
                this.OnPropertyChanged("Id");
            }
        }
        public string Name
        {
            get => name;
            set
            {
                name = value;
                this.OnPropertyChanged("Name");
            }
        }
        public string Address
        {
            get => address;
            set
            {
                address = value;
                this.OnPropertyChanged("Address");
            }
        }
        public string DataType
        {
            get => dataType;
            set
            {
                dataType = value;
                this.OnPropertyChanged("DataType");
            }
        }
        public string ClientAccess
        {
            get => clientAccess;
            set
            {
                clientAccess = value;
                this.OnPropertyChanged("ClientAccess");
            }
        }
        public string ScanRate
        {
            get => scanRate;
            set
            {
                scanRate = value;
                this.OnPropertyChanged("ScanRate");
            }
        }
    }
}
