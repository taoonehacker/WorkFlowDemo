using Microsoft.VisualBasic.Activities;
using System;
using System.Activities;
using System.Activities.Statements;
using System.Collections.Generic;
using System.Text;

namespace WorkflowDemo.Commands.Debug
{
    public class WriteLineCommand:ICommandBase
    {
        public readonly string GroupName = "Debug";
        public Activity GetActivity(Dictionary<string, string> propertyDic)
        {
            var result = new WriteLine();
            var text = string.Empty;
            foreach (var key in propertyDic.Keys)
            {
                switch (key.ToLower())
                {
                    case "count":
                        text  += $"请输入{propertyDic[key]}";
                        break;
                    case "int":
                        text +=$"{propertyDic[key]}";
                        break;
                    case "string":
                        text += $"{propertyDic[key]}";
                        break;
                    default:
                        break;
                }
            }
            result.Text = new VisualBasicValue<string>(text);
            return result;
        }
    }
}
