using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Configuration;

namespace ConfigModel.GetFromResources
{
    public class UserConfig : ConfigurationSection
    {
        [ConfigurationProperty("firstName", IsRequired = true)]
        public String FirstName
        {
            get
            {
                return this["firstName"] as String;
            }
        }

        [ConfigurationProperty("lastName", IsRequired = true)]
        public String LastName
        {
            get
            {
                return this["lastName"] as String;
            }
        }

        /// <summary>
        /// 读取资源文件里的配置
        /// </summary>
        public String ResourceMessage
        {
            get
            {
                return UserResource.NoticeMessage;
            }
        }
    }
}
