using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CommonHelper.Results
{
    /// <summary>
    /// 操作消息工厂类
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public sealed class ResultInfoFactory<T> where T : ResultInfo, new()
    {
        public static T Create(bool isSuccessed, string resultCode, string returnMessage, params object[] parameters)
        {
            T instance = new T();
            instance.IsSuccessed = isSuccessed;
            instance.ReturnCode = resultCode;

            if (parameters != null && parameters.Length > 0)
            {
                instance.ReturnMessage = string.Format(returnMessage, parameters);
            }
            else
            {
                instance.ReturnMessage = returnMessage;
            }

            return instance;
        }
    }
}
