using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IocModel.CastleIoc
{
    public class TextFormat : ILogFormatter
    {
        public TextFormat()
        {

        }

        #region ILogFormatter 成员

        public string Format(string message)
        {
            return "【" + message + "】";
        }

        #endregion
    }
}
