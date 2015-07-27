﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IocModel.MicrosoftIoc
{
    public class FlatFileLogger : ILogger
    {
        #region ILogger 成员

        public string Writer(string message)
        {
            return string.Format("Message：{0} \r\n Target:FlatFile", message);
        }

        #endregion
    }
}