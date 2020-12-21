using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Hegang.APP.Commands
{
    class DelegateCommand : ICommand
    {
        //当命令是否可以执行的状态发生改变时通知命令调用者
        public event EventHandler CanExecuteChanged;

        /// <summary>
        /// 判断命令能否执行
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        public bool CanExecute(object parameter)
        {
            if (this.CanExecuteFunc == null)
                return true;
            return this.CanExecuteFunc(parameter);
        }

        /// <summary>
        /// 执行的命令的具体操作
        /// </summary>
        /// <param name="parameter"></param>
        public void Execute(object parameter)
        {
            if (this.ExcuteAction == null)
                return;
            this.ExcuteAction(parameter);
        }
        
        //委托是将不会变化的部分封装，传递会修改的部分
        //无返回值的泛型委托
        public Action<object> ExcuteAction{ get; set; }

        //有返回值的泛型委托
        public Func<object,bool> CanExecuteFunc{ get; set; }
    }
}
