using System;
using System.Collections.Generic;
using System.IO;

namespace TypescriptT4
{
    public class TsClassInfo
    {
        public string Name { get; private set; }

        public TsClassInfo(string name)
        {
            Name = name;
        }
    }

    public class Bridge
    {
        public IList<TsClassInfo> TsClasses = new List<TsClassInfo>();

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

        public void AddClass(string className)
        {
            TsClasses.Add(new TsClassInfo(className));
        }
    }
}
