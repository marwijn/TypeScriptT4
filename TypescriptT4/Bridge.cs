using System;
using System.IO;

namespace TypescriptT4
{
    public class Bridge
    {
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
            var streamReader = new StreamReader(fileName);

            string script = streamReader.ReadToEnd();
            streamReader.Close();
            return script;
        }

        public StreamWriter CreateWriter(string fileName)
        {
            var writer = new StreamWriter(fileName);
            return writer;
        }
    }
}
