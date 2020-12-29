using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hegang.APP.ViewModels
{
    class User:NotificationObject
    {
        private string id;
        private string userName;
        private string pwd;
        private string age;
        private string nation;
        private string department;
        
        public User(string _id,string _userName,string _pwd,string _age,string _nation,string _department)
        {
            this.Id = _id;
            this.UserName = _userName;
            this.Pwd = _pwd;
            this.Age = _age;
            this.Nation = _nation;
            this.Department = _department;
        }

        public string Id 
        { 
            get => id;
            set 
            { 
                id = value;
                this.OnPropertyChanged("Id");
            }
        }
        public string UserName
        {
            get => userName;
            set
            {
                userName = value;
                this.OnPropertyChanged("UserName");
            }
        }

        public string Pwd
        {
            get => pwd;
            set
            {
                pwd = value;
                this.OnPropertyChanged("Pwd");
            }
        }

        public string Age
        {
            get => age;
            set
            {
                age = value;
                this.OnPropertyChanged("Pwd");
            }
        }
        public string Nation
        {
            get => nation;
            set
            {
                nation = value;
                this.OnPropertyChanged("Nation");
            }
        }
        public string Department
        {
            get => department;
            set
            {
                department = value;
                this.OnPropertyChanged("Department");
            }
        }
    }
}
