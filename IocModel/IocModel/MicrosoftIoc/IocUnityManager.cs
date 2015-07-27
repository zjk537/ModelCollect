using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Practices.Unity;


namespace IocModel.MicrosoftIoc
{
    public class IocUnityManager
    {
        static IUnityContainer container;
        public static IUnityContainer GetInstance()
        {
            if (container == null)
            {
                container = new UnityContainer();

                /// RegisterType 创建的实例默认不是单例模式的实例 LifetimeManager 这个可以用来把它设置成单例模式
                //// 泛型参数方式  
                //// 注册接口映射  满足条件：IUnityContainer RegisterType<TFrom, TTo>() where TTo : TFrom
                //container.RegisterType<ILogger, DatabaseLogger>();

                //// 注册接口映射 用下面这种方式，带参数的，就要Microsoft.Practices.ObjectBuilder2.dll 
                //// 因为 方法：IUnityContainer RegisterType(Type t, params InjectionMember[] injectionMembers)中的 参数 InjectionMember
                //// 有用到 Microsoft.Practices.ObjectBuilder2.dll 中的接口，所以添加对Microsoft.Practices.ObjectBuilder2.dll的引用
                //container.RegisterType<ILogger, FlatFileLogger>("FlatFileLogger");

                // // 参数方式
                //container.RegisterType(typeof(ILogger), typeof(DatabaseLogger));
                //container.RegisterType(typeof(ILogger), typeof(FlatFileLogger), "FlatFileLogger");

                /// RegisterInstance 创建的实例默认是单例模式的，但也要防止这个创建实例的单例创建
                container.RegisterInstance<ILogger>(new DatabaseLogger());
                container.RegisterInstance<ILogger>("FlatFileLogger",new FlatFileLogger());
                
            }

            return container;
        }
    }
}
