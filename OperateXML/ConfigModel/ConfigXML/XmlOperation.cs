using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConfigModel.ConfigXML
{
    public enum XmlOperation
    {
        /// <summary>
        /// 空
        /// </summary>
        None,

        /// <summary>
        /// 业务名称已存在
        /// </summary>
        BusinessTypeExist,

        /// <summary>
        /// 可以新增业务
        /// </summary>
        BusinessType,

        /// <summary>
        /// 服务已存在
        /// </summary>
        CommendServiceExist,

        /// <summary>
        /// 可以新增服务
        /// </summary>
        CommendService
    }
}
