using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IocModel.CastleIoc
{
    public interface ILogFormatter
    {
        string Format(string message);
    }
}
