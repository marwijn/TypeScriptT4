using NUnit.Framework;
using TypescriptT4;
using System.Linq;

namespace TypeScriptT4Tests
{
    [TestFixture]
    public class SingleClassTests
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
            Assert.AreEqual("function1", scriptInfo.GetClasses().Single().Methods.Single(m => m.Name == "function1").Name);
        }

        [Test]
        public void ModuleNameWillContainTheModuleName()
        {
            Assert.AreEqual("Module2.Module1", scriptInfo.GetClasses().Single().ModuleName);
        }

        [Test]
        public void ArgumentsForFunction()
        {
            Assert.AreEqual("parameter1", scriptInfo.GetClasses().Single().Methods.Single(m=>m.Name=="function1").Arguments.Single().Name);
        }

        
    }
}
