using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace CommonHelper.Results
{
    /// <summary>
    /// 操作结果信息类
    /// </summary>
    [JsonObject]
    public class ResultInfo
    {
        /// <summary>
        /// 获取或设置操作是否成功
        /// </summary>
        [JsonProperty("sucess")]
        public bool IsSuccessed { get; set; }

        /// <summary>
        /// 获取或设置操作码
        /// </summary>
        [JsonProperty("code")]
        public string ReturnCode { get; set; }

        /// <summary>
        /// 获取或设置操作提示信息
        /// </summary>
        [JsonProperty("msg")]
        public string ReturnMessage { get; set; }


        public ResultInfo()
        { }

        public ResultInfo(bool isSuccessed, string returnCode, string returnMessage)
        {
            this.IsSuccessed = isSuccessed;
            this.ReturnCode = returnCode;
            this.ReturnMessage = returnMessage;
        }

        /// <summary>
        /// 根据返回的类型创建返回对象实例
        /// </summary>
        /// <param name="type">要创建的类型</param>
        /// <param name="isSuccessed">是否成功</param>
        /// <param name="returnMessage">返回描述信息</param>
        /// <returns></returns>
        public static object CreateResultInfo(Type type, bool isSuccessed, string returnCode, string returnMessage)
        {
            object objResultInfo = Activator.CreateInstance(type);
            ResultInfo resultInfo = objResultInfo as ResultInfo;

            if (resultInfo != null)
            {
                resultInfo.IsSuccessed = isSuccessed;
                resultInfo.ReturnCode = returnCode;
                resultInfo.ReturnMessage = returnMessage;
            }

            return objResultInfo;
        }
    }

    /// <summary>
    /// 操作信息类
    /// </summary>
    /// <typeparam name="T">ResultInfo</typeparam>
    [JsonObject]
    public class ResultInfo<T> : ResultInfo
    {
        public ResultInfo()
        { }

        public ResultInfo(bool isSuccessed, string returnCode, string returnMessage)
            : base(isSuccessed, returnCode, returnMessage)
        { }

        [JsonProperty("data")]
        public T ReturnEntity { get; set; }
    }
}
