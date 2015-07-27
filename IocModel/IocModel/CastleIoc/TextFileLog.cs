using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IocModel.CastleIoc
{
    public class TextFileLog : ILog
    {
        private string target;
        private ILogFormatter format;

        public TextFileLog(string target, ILogFormatter format)
        {
            this.target = target;
            this.format = format;
        }

        #region ILog 成员

        public string Writer(string message)
        {
            string Msg = this.format.Format(message);
            Msg += target;

            return Msg;
        }

        #endregion
    }
}
