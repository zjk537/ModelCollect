using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Configuration;
using ConfigModel.GetConfigCollection;
using ConfigModel.GetFromResources;

namespace ConfigModel
{
    public class ConfigManager
    {
        /// <summary>
        /// 获取配置文件中 PathServiceConfig.config
        /// </summary>
        /// <returns></returns>
        public static PathServiceConfig GetPathServiceConfig()
        {
          PathServiceConfig  pathServiceConfig = ConfigurationManager.GetSection("PathServiceConfig") as PathServiceConfig;
            if (pathServiceConfig == null || pathServiceConfig.Equals(new PathServiceConfig()))
                throw new Exception("没有配置节点：PathServiceConfig");

            return pathServiceConfig;
        }

        /// <summary>
        /// 获取配置文件中的信息 直接在app.config配置
        /// </summary>
        /// <returns></returns>
        public static UserConfig GetUserInfo()
        {
            UserConfig userConfig = ConfigurationManager.GetSection("UserConfig") as UserConfig;

            if (userConfig == null || userConfig.Equals(new UserConfig()))
                throw new Exception("没有配置节点：UserConfig");

            return userConfig;
        }
    }
}
