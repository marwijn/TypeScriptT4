﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace TypescriptT4
{
    public class TsClassInfo
    {
        public string Name { get; private set; }

        public string ModuleName { get; private set; }

        public IList<TsMethodInfo> Methods = new List<TsMethodInfo>(); 

        public TsClassInfo(string name, string moduleName)
        {
            Name = name;
            ModuleName = moduleName;
        }
    }

    public class TsMethodInfo
    {
        public string Name { get; private set; }

        public TsMethodInfo(string name)
        {
            Name = name;
        }
    }

    public class Bridge
    {
        public IList<TsClassInfo> TsClasses = new List<TsClassInfo>();
        public IList<TsMethodInfo> TsMethods = new List<TsMethodInfo>();



        private string _currentModule;
        private TsClassInfo _currentClass;

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
                    var streamReader = new StreamReader(stream);
                    var result = streamReader.ReadToEnd();
                    return result;
                }
            }
            else
            {
                using (var streamReader = new StreamReader(fileName))
                {
                    return streamReader.ReadToEnd();
                }                
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

        public void StartClass(string className)
        {
            _currentClass = new TsClassInfo(className, _currentModule);
            TsClasses.Add(_currentClass);
        }

        public void EndClass()
        {
            _currentClass = null;
        }

        public void StartFunction (string functionName)
        {
            var method = new TsMethodInfo(functionName);
            if (_currentClass != null)
            {
                _currentClass.Methods.Add(method);
            }
            TsMethods.Add(method);
        }

        public void StartModule(string moduleName)
        {
            _currentModule = moduleName;
        }

        public void EndModule()
        {
            _currentModule = null;
        }
    }
}
