using System;
using System.Linq;
using System.Reflection;

namespace ET
{
    public class StaticMethod: IStaticMethod
    {
        private readonly MethodInfo methodInfo;

        private readonly object[] param;

        public StaticMethod(Assembly assembly, string typeName, string methodName)
        {
            if (assembly is null)
            {
                throw new Exception("Assembly is NULL");
            }

            Log.Info($"Type is Ready {assembly.GetName().Name}, TypeCount:{assembly.ExportedTypes.Count()}, Read TypeName:{typeName},Method:{methodName}");
            Type type = assembly.GetType(typeName);
            Log.Info($"Type is Ready {type.Namespace},{type.Name}");
            MethodInfo method = type.GetMethod(methodName);
            Log.Info($"MethodInfo is Ready {method.Name}");
            this.methodInfo = method;
            this.param = new object[this.methodInfo.GetParameters().Length];
        }

        public override void Run()
        {
            this.methodInfo.Invoke(null, param);
        }

        public override void Run(object a)
        {
            this.param[0] = a;
            this.methodInfo.Invoke(null, param);
        }

        public override void Run(object a, object b)
        {
            this.param[0] = a;
            this.param[1] = b;
            this.methodInfo.Invoke(null, param);
        }

        public override void Run(object a, object b, object c)
        {
            this.param[0] = a;
            this.param[1] = b;
            this.param[2] = c;
            this.methodInfo.Invoke(null, param);
        }
    }
}