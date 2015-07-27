using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FilesModel.EquipmentConfig
{
    /// <summary>
    /// 文件内容详细信息
    /// </summary>
    public class FileDataDetail
    {
        /// <summary>
        /// 节点名称
        /// </summary>
        public String Name { get; set; }

        /// <summary>
        /// 串口配置
        /// </summary>
        public List<SerialPortConfigInfo> SerialPortList { get; set; }

        /// <summary>
        /// 设备配置
        /// </summary>
        public List<EquipmentInfo> EquipmentList { get; set; }

        public FileDataDetail()
        {
            this.SerialPortList = new List<SerialPortConfigInfo>();
            this.EquipmentList = new List<EquipmentInfo>();
        }

    }
}
