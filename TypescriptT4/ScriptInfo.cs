using System.Collections;
using System.Collections.Generic;
using System.IO;
using Noesis.Javascript;

namespace TypescriptT4
{
    public class ScriptInfo
    {
        Bridge bridge;

        public void RunCompiler(string fileName)
        {

            string script;
            using (var stream = GetType().Assembly.GetManifestResourceStream("TypescriptT4.compilerjs.tsc.js"))
            {
                var streamReader = new StreamReader(stream);
                script = streamReader.ReadToEnd();
            }

            bridge = new Bridge();
            bridge.SourceFile = fileName;
            using (var context = new JavascriptContext())
            {
                context.SetParameter("bridge", bridge);
                context.Run(script);
            }
        }

        public IList<TsClassInfo> GetClasses()
        {
            return bridge.TsClasses;
        }
    }
}
