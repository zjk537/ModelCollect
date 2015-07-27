using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FilesModel.EquipmentConfig
{
    /// <summary>
    /// 设备配置
    /// </summary>
    public class EquipmentConfigInfo
    {
        /// <summary>
        /// 序号
        /// </summary>
        public Int32 DDC_ID { get; set; }

        /// <summary>
        /// 变量类型
        /// </summary>
        public String VarType { get; set; }

        /// <summary>
        /// 功能码(Hex)
        /// </summary>
        public String Func { get; set; }

        /// <summary>
        /// 偏移量(Hex)
        /// </summary>
        public String Offset { get; set; }

        /// <summary>
        /// 点位描述
        /// </summary>
        public String Desc { get; set; }

        /// <summary>
        /// 单位
        /// </summary>
        public String Unit { get; set; }

        /// <summary>
        /// 点位
        /// </summary>
        public String Point { get; set; }

        /// <summary>
        /// 参数1
        /// </summary>
        public String Param1 { get; set; }

        /// <summary>
        /// 参数2
        /// </summary>
        public String Param2 { get; set; }
    }
}
