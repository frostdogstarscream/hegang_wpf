using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace HFD.KEPWare
{
    /// <summary>
    /// 定义PLC数据块,Key为设备名称
    /// </summary>
    public class PLCDataPack: Dictionary<string, PLCItem>
    {
        /// <summary>
        /// PLC设备名称
        /// </summary>
        public string DeviceName
        {
            get { return _device; }
            set { _device = value; }
        }
        private string _device;

        /// <summary>
        /// PLC数据包事务ID
        /// </summary>
        public int TransactonId
        {
            get { return _transid; }
            set { _transid = value; }
        }
        private int _transid;

        /// <summary>
        /// 构造函数（无参数）
        /// </summary>
        public PLCDataPack()
        {
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="_device"></param>
        /// <param name="_transid"></param>
        public PLCDataPack(string _device, int _transid )
        {
            this._device = _device;
            this._transid = _transid;
        }

        /// <summary>
        /// 复制当前数据包
        /// </summary>
        /// <returns></returns>
        public PLCDataPack Clone()
        {
            PLCDataPack pack = new PLCDataPack();
            pack._transid = this._transid;
            pack._device = this._device;
            foreach (KeyValuePair<string, PLCItem> kvp in this)
                pack.Add(kvp.Key, kvp.Value.Clone());
            return pack;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            foreach (KeyValuePair<string, PLCItem> kvp in this)
                sb.AppendLine(kvp.Key + "\t" + kvp.Value);
            return sb.ToString();
        }
    }
}
