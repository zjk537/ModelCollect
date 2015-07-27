using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using Castle.Windsor;
using Castle.Windsor.Configuration.Interpreters;

namespace IocModel.CastleIoc
{
    public class CastleIocManager
    {
        static IWindsorContainer container = null;

        public static IWindsorContainer GetInstance()
        {
            if (container == null)
            {
                container = new WindsorContainer(new XmlInterpreter("CastleIoc/BasicUsage.xml"));

                container.AddComponent("txtLog", typeof(ILog), typeof(TextFileLog));
                container.AddComponent("txtFormat", typeof(ILogFormatter), typeof(TextFormat));
            }

            return container;
        }
    }
}
