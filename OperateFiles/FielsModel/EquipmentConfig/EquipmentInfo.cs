using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FilesModel.EquipmentConfig
{
    /// <summary>
    /// 设备信息
    /// </summary>
    public class EquipmentInfo
    {
        /// <summary>
        /// 设备名称
        /// </summary>
        public String Name { get; set; }

        /// <summary>
        /// 设备配置
        /// </summary>
        public List<EquipmentConfigInfo> EquipmentConfigList { get; set; }

        public EquipmentInfo()
        {
            EquipmentConfigList = new List<EquipmentConfigInfo>();
        }
    }
}
