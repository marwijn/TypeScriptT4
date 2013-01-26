using NUnit.Framework;
using TypescriptT4;
using System.Linq;

namespace TypeScriptT4Tests
{
    [TestFixture]
    public class ScriptInfoTests
    {
        [Test]
        public void GetClassesWillReturnTheClasses()
        {
            var scriptInfo = new ScriptInfo();
            scriptInfo.RunCompiler();
            CollectionAssert.AreEquivalent(new [] {"Point"}, scriptInfo.GetClasses().Select(c => c.Name));
        }
    }
}
