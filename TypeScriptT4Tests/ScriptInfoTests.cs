using NUnit.Framework;
using TypescriptT4;
using System.Linq;

namespace TypeScriptT4Tests
{
    [TestFixture]
    public class ScriptInfoTests
    {
        private ScriptInfo scriptInfo;

        [TestFixtureSetUp]
        public void Setup()
        {
            scriptInfo = new ScriptInfo();
            scriptInfo.RunCompiler("SingleClass.ts");
        }

        [Test]
        public void GetClassesWillReturnTheClass()
        {
            Assert.AreEqual("Class1", scriptInfo.GetClasses().Single().Name);
        }

        [Test]
        public void GetMethodsWillReturnTheMethod()
        {
            Assert.AreEqual("function1", scriptInfo.GetClasses().Single().Methods.First().Name);
        }

        [Test]
        public void ModuleNameWillContainTheModuleName()
        {
            Assert.AreEqual("Module1", scriptInfo.GetClasses().Single().ModuleName);
        }

    }
}
