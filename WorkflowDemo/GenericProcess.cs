using System;
using System.Activities;
using System.Activities.Statements;
using System.Activities.XamlIntegration;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xaml;
using WorkflowDemo.Commands;

namespace WorkflowDemo
{
    public class GenericProcess
    {
        public static void GenericXaml()
        {
            var queue = DocumentContext.GetInstance().GetProcesses();

            var activityBuilder = GetActivityBuilder(queue);

            SaveToXaml(activityBuilder);
        }
        
        private static ActivityBuilder<int> GetActivityBuilder(Queue<ProcessData> queue)
        {
            ActivityBuilder<int> result = new ActivityBuilder<int>
            {
                Name = "Test"
            };

            while (queue.Count != 0)
            {
                var processData = queue.Dequeue();
                var type = typeof(ICommandBase);

                var types = AppDomain.CurrentDomain.GetAssemblies()
                  .SelectMany(a => a.GetTypes().Where(_ => _.GetInterfaces().Contains(type)));

                var t = types.FirstOrDefault(a => a.Name == $"{processData.ActivityName}");
                if (t == null) return result;
                var method = t.GetMethod("GetActivity");
                var parameters = new object[] { processData.ParameterDic };
                if (method != null)
                {
                    var obj = Activator.CreateInstance(t);
                    var activity = (Activity)method.Invoke(obj, parameters);
                    var sequence = new Sequence();
                    sequence.Activities.Add(activity);
                    result.Implementation = sequence;
                }
            }
            return result;
        }

        private static void SaveToXaml(ActivityBuilder<int> ab)
        {
            var path = Path.GetFullPath("../../") + "Workflows\\test.xaml";
            StreamWriter sw = File.CreateText(path);
            XamlWriter xw = ActivityXamlServices.CreateBuilderWriter(new XamlXmlWriter(sw, new XamlSchemaContext()));
            XamlServices.Save(xw, ab);
            sw.Close();
        }
    }
}
