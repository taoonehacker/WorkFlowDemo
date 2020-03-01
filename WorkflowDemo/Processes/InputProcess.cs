using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace WorkflowDemo.Processes
{
    public class InputProcess : IProcess
    {
        private readonly string ActivityName = "WriteLineCommand";
        private readonly string GroupName = "Debug";
        private readonly DocumentContext _documentContext;
        private readonly Dictionary<string, string> _ruleDic;
        private readonly Dictionary<string, string> _propertyDic;
        public InputProcess()
        {
            _documentContext = DocumentContext.GetInstance();
            _ruleDic = new Dictionary<string, string>
            {
                { "输入", "^输入\\S*" },
                { "input", "^input\\S*" }
            };

            _propertyDic = new Dictionary<string, string>
            {
                { "Count", "(?<=输入)\\d(?=\\S*数字)" },
                { "Int", "数字$" },
                { "String", "字符串$" }
            };

        }

        public bool IsMatch(string symbols)
        {
            var result = false;
            foreach(var rule in _ruleDic.Values)
            {
                if (Regex.IsMatch(symbols, rule))
                {
                    result = true;
                    break;
                }
            }
            return result;
        }

        public void Process(string symbols)
        {
            var entity = new ProcessData
            {
                GroupName = GroupName,
                ActivityName= ActivityName
            };

            foreach (var key in _propertyDic.Keys)
            {
                var result = Regex.Match(symbols, _propertyDic[key]);
                if(result.Success)
                {
                    entity.ParameterDic.Add(key, result.Value);
                }
            }
            _documentContext.EnQueue(entity);
        }
    }
}
