using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WorkflowDemo.Processes;

namespace WorkflowDemo
{
    public class Parser
    {
        private List<IProcess> _processes;
        public Parser()
        {
            _processes = GetProcesses();
        }

        private List<IProcess> GetProcesses()
        {
            var result = new List<IProcess>();
            var type = typeof(IProcess);
            var types = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(a => a.GetTypes().Where(t => t.GetInterfaces().Contains(type)));
            foreach (var v in types)
            {
                if (v.IsClass)
                {
                    result.Add(Activator.CreateInstance(v) as IProcess);
                }
            }
            return result;
        }

        public void Parse(string[] symbolsArray)
        {
            foreach (var symbol in symbolsArray)
            {
                var matchProcesses = _processes.FirstOrDefault(e => e.IsMatch(symbol));
                if (matchProcesses == null) Console.WriteLine("No Match Cmd");
                else matchProcesses.Process(symbol);
            }
        }       
    }

}
