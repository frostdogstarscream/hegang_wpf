﻿using Hegang.APP.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hegang.APP.ViewModels
{
    class LoginViewModel:NotificationObject
    {
        #region 数据属性
        private string userName;
        private string pwd;
        #endregion

        #region 命令属性
        public DelegateCommand LoginCommand { get; set; }
        public DelegateCommand ForgetPwdCommand { get; set; }
        public DelegateCommand RegisterCommand { get; set; }
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
        #endregion

        public LoginViewModel()
        {
            #region 命令属性初始化
            this.LoginCommand = new DelegateCommand();
            this.LoginCommand.ExcuteAction = new Action<object>(this.LoginCommandExecute);
            #endregion
        }

        private void LoginCommandExecute(object parameter)
        {
            Console.WriteLine("登录按钮已点击");
            Console.WriteLine(this.userName);
            Console.WriteLine(this.pwd);
        }
    }
}
