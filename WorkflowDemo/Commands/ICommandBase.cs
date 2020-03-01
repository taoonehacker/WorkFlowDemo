using System;
using System.Activities;
using System.Collections.Generic;
using System.Text;

namespace WorkflowDemo.Commands
{
    public interface ICommandBase
    {
        Activity GetActivity(Dictionary<string,string> propertyDic);
    }
}
