﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

namespace TypescriptT4
{
    public class TsTypeInfo
    {
        public string Name { get; private set; }
        public string ModuleName { get; private set; }
        public bool IsInterface { get; private set; }

        public IList<TsMethodInfo> Methods = new List<TsMethodInfo>(); 

        public TsTypeInfo(string name, string moduleName, bool isInterface)
        {
            Name = name;
            ModuleName = moduleName;
            IsInterface = isInterface;
        }
    }

    public class TsArgument
    {
        public string Name { get; set; }
        public string Type { get; set; } 
    }

    public class TsMethodInfo
    {
        public string Name { get; private set; }
        public IList<TsArgument> Arguments { get; private set; } 


        public TsMethodInfo(string name)
        {
            Name = name;
            Arguments = new List<TsArgument>();
        }

    }

    public class Bridge
    {
        public IList<TsTypeInfo> TsTypes = new List<TsTypeInfo>();
        public IList<TsMethodInfo> TsMethods = new List<TsMethodInfo>();



        private string _currentModule;
        private TsTypeInfo _currentType;
        private TsMethodInfo _currentMethod;

        public string SourceFile { get; set; }

        public void Print(object iString)
        {
            Console.WriteLine(iString);
        }

        public void Print(string iString, object parameter1)
        {
            Console.WriteLine(iString, parameter1);
        }

        public void Print(string iString, object parameter1, object parameter2)
        {
            Console.WriteLine(iString, parameter1, parameter2);
        }

        public string ReadFile(string fileName)
        {
            if (fileName.StartsWith("<ExecutingPath>"))
            {
                var resourcePath = fileName.Substring("<ExecutingPath>".Length+1);
                using (var stream = GetType().Assembly.GetManifestResourceStream("TypescriptT4.compilerjs."+resourcePath ))
                {
                    Debug.Assert(stream != null, "stream != null");
                    var streamReader = new StreamReader(stream);
                    var result = streamReader.ReadToEnd();
                    return result;
                }
            }
            using (var streamReader = new StreamReader(fileName))
            {
                return streamReader.ReadToEnd();
            }
        }

        public StreamWriter CreateWriter(string fileName)
        {
            var writer = new StreamWriter(fileName);
            return writer;
        }

        public bool FileExists(string path)
        {
            return File.Exists(path);
        }

        public bool DirectoryExists(string path)
        {
            return Directory.Exists(path);
        }

        public string DirName (string path)
        {
            if (path.StartsWith("<ExecutingPath>")) return "<ExecutingPath>";
            return Path.GetDirectoryName(path);
        }

        public void CreateDirectory(string path)
        {
            Directory.CreateDirectory(path);
        }

        public void StartType(string typeName, bool isInterface)
        {
            if (_currentType != null) throw new Exception("Start class inside a class");
            _currentType = new TsTypeInfo(typeName, _currentModule, isInterface);
            TsTypes.Add(_currentType);
        }

        public void EndType()
        {
            if (_currentType == null) throw new Exception("End class while not in a class");
            _currentType = null;
        }

        public void StartFunction (string functionName)
        {
            if (_currentMethod != null) throw new Exception("Start function inside a function");
            var method = new TsMethodInfo(functionName);
            _currentMethod = method;
            if (_currentType != null)
            {
                _currentType.Methods.Add(method);
            }
            TsMethods.Add(method);
        }

        public void EndFunction()
        {
            if (_currentMethod == null) throw new Exception("End function without a start");
            _currentMethod = null;
        }

        public void StartModule(string moduleName)
        {
            if (_currentModule != null) throw new Exception("Start module inside a module");
            _currentModule = moduleName;
        }

        public void EndModule()
        {
            if (_currentModule == null) throw new Exception("End module without a start");
            _currentModule = null;
        }

        public void StartConstructor()
        {
            StartFunction("constructor");
        }

        public void EndConstructor()
        {
            EndFunction();
        }


        public void AddArgument(string name, object type)
        {
            if (_currentMethod == null) throw new Exception("Add argument while not in a method");
            _currentMethod.Arguments.Add(new TsArgument{Name = name,Type = type.ToString()});
        }

        public void CompileCompleted()
        {
            if (_currentType != null) throw new Exception("Class not closed");
            if (_currentMethod != null) throw new Exception("Method not closed");
            if (_currentModule != null) throw new Exception("Module not closed");
        }
    }
}
