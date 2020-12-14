using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HFD.KEPWare
{
    public class PLCItem
    {
        /// <summary>
        /// PLCItem数据类型
        /// </summary>
        public Type ItemType
        {
            get { return _type; }
            set { _type = value; }
        }
        private Type _type;

        /// <summary>
        /// PLCItem的名称
        /// </summary>
        public string ItemName
        {
            get { return _name; }
            set { _name = value; }
        }
        private string _name;

        /// <summary>
        /// PLCItem的值,根据Item的类型来定
        /// </summary>
        public object ItemValue
        {
            get { return _value; }
            set { _value = value; }
        }
        private object _value;

        /// <summary>
        /// 采集时间
        /// </summary>
        public DateTime TimeStamp
        {
            get { return _time; }
            set { _time = value; }
        }
        private DateTime _time;
        public PLCItem()
        { }

        public PLCItem Clone()
        {
            PLCItem item = new PLCItem();
            item._name = this._name;
            item._type = this._type;
            item._value = this._value;
            item._time = this._time;
            return item;
        }

        public override string ToString()
        {
            return this._name + "\t" + this._type + "\t" + this._value + "\t" + this._time;
        }

    }
}
