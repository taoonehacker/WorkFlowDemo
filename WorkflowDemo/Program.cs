using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace WorkflowDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            var path = Path.GetFullPath("../../") + "\\Writeline.txt";

            Console.WriteLine("--- Guess Number Demo ---");

            var lines = new List<string>();

            using (var reader = new StreamReader(path))
            {
                string line = null;
                do
                {
                    line = reader.ReadLine();
                    if (line == null)
                        break;
                    lines.Add(line);
                }
                while (line != null);
            }

            Console.WriteLine("--- Parse document ---");

            var parser = new Parser();
            var parseTask = Task.Run(() => parser.Parse(lines.ToArray()));

            Console.WriteLine("--- Generic process xaml ---");

            GenericProcess.GenericXaml();

            Console.ReadLine();
        }
    }
}
