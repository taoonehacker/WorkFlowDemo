using System;
using System.Collections.Generic;
using System.Text;

namespace WorkflowDemo.Processes
{
    public interface IProcess
    {
        bool IsMatch(string symbols);
        void Process(string symbols);
    }
}
