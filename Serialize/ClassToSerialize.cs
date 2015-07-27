using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Serialize
{
    [Serializable]
    public class ClassToSerialize
    {
        /// <summary>
        /// ID编号
        /// </summary>
        public int id = 100;

        /// <summary>
        /// 姓名
        /// </summary>
        public string name = "zjk";

        /// <summary>
        /// 性别
        /// </summary>
        public string sex = "man";

        /// <summary>
        /// 手机号码
        /// </summary>
        [NonSerialized]
        public string MoblieNumber = "13825200137";
    }
}
