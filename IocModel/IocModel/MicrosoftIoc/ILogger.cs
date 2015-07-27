using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IocModel.MicrosoftIoc
{
    public interface ILogger
    {
        string Writer(string message);
    }
}
