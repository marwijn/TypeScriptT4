using System.IO;
using Noesis.Javascript;

namespace TypescriptT4
{
    public class ScriptInfo
    {
        public void RunCompiler()
        {

            string script;
            using (var stream = GetType().Assembly.GetManifestResourceStream("TypescriptT4.compilerjs.tsc.js"))
            {
                var streamReader = new StreamReader(stream);
                script = streamReader.ReadToEnd();
            }

            using (var context = new JavascriptContext())
            {
                var bridge = new Bridge();

                context.SetParameter("bridge", bridge);
                context.Run(script);
            }
        }
    }
}
