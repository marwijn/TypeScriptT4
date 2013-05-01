using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using Noesis.Javascript;

namespace TypescriptT4
{
    public class ScriptInfo
    {
        Bridge _bridge;

        public void RunCompiler(string fileName)
        {

            string script;
            using (var stream = GetType().Assembly.GetManifestResourceStream("TypescriptT4.compilerjs.tsc.js"))
            {
                Debug.Assert(stream != null, "stream != null");
                var streamReader = new StreamReader(stream);
                script = streamReader.ReadToEnd();
            }

            _bridge = new Bridge {SourceFile = fileName};
            using (var context = new JavascriptContext())
            {
                context.SetParameter("bridge", _bridge);
                context.Run(script);
            }
            _bridge.CompileCompleted();
        }

        public IList<TsTypeInfo> GetClasses()
        {
            return _bridge.TsTypes.Where(t => !t.IsInterface).ToList();
        }

        public IList<TsTypeInfo> GetInterfaces()
        {
            return _bridge.TsTypes.Where(t => t.IsInterface).ToList();
        }
    }
}
