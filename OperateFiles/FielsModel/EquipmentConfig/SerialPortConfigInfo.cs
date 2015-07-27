using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FilesModel.EquipmentConfig
{
    /// <summary>
    /// 串口配置
    /// </summary>
    public class SerialPortConfigInfo
    {
        /// <summary>
        /// 序号
        /// </summary>
        public Int32 DDC_ID { get; set; }

        /// <summary>
        /// 端口号
        /// </summary>
        public Int32 Comm { get; set; }

        /// <summary>
        /// 波特率
        /// </summary>
        public Int32 Baud { get; set; }

        /// <summary>
        /// 数据位
        /// </summary>
        public Int32 DataBit { get; set; }

        /// <summary>
        /// 停止位
        /// </summary>
        public Int32 StopBit { get; set; }

        /// <summary>
        /// 同位
        /// </summary>
        public String Parity { get; set; }

        /// <summary>
        /// 重送位
        /// </summary>
        public Int32 CNT_Resend { get; set; }
    }
}
