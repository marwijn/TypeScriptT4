using NUnit.Framework;
using TypescriptT4;
using System.Linq;

namespace TypeScriptT4Tests
{
    [TestFixture]
    public class SingleClassTests
    {
        private ScriptInfo _scriptInfo;

        [TestFixtureSetUp]
        public void Setup()
        {
            _scriptInfo = new ScriptInfo();
            _scriptInfo.RunCompiler("SingleClass.ts");
        }

        [Test]
        public void GetClassesWillReturnTheClass()
        {
            Assert.AreEqual("Class1", _scriptInfo.GetClasses().Single().Name);
        }

        [Test]
        public void GetMethodsWillReturnTheMethod()
        {
            Assert.AreEqual("function1", _scriptInfo.GetClasses().Single().Methods.Single(m => m.Name == "function1").Name);
        }

        [Test]
        public void ModuleNameWillContainTheModuleName()
        {
            Assert.AreEqual("Module2.Module1", _scriptInfo.GetClasses().Single().ModuleName);
        }

        [Test]
        public void ArgumentsForFunction()
        {
            Assert.AreEqual("parameter1", _scriptInfo.GetClasses().Single().Methods.Single(m=>m.Name=="function1").Arguments.Single().Name);
        }
        
        [Test]
        public void ConstructorArugments()
        {
            Assert.AreEqual("parameter2", _scriptInfo.GetClasses().Single().Methods.Single(m=>m.Name=="constructor").Arguments.Single().Name);
            Assert.AreEqual("Module3.Interface1", _scriptInfo.GetClasses().Single().Methods.Single(m => m.Name == "constructor").Arguments.Single().Type);
        }


    }
}
