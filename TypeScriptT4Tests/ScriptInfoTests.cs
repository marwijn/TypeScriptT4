using NUnit.Framework;
using TypescriptT4;

namespace TypeScriptT4Tests
{
    [TestFixture]
    public class ScriptInfoTests
    {
        [Test]
        public void CompileEmptyFileWillNotThrow()
        {
            var scriptInfo = new ScriptInfo();
            scriptInfo.RunCompiler();
        }
    }
}
