using System;
using System.Collections.Generic;
using System.Text;

namespace WorkflowDemo
{
    public class DocumentContext
    {
        private static DocumentContext _instance;
        private static readonly object locker = new object();
        private readonly Queue<ProcessData> _processQueue;
        private DocumentContext()
        {
            _processQueue = new Queue<ProcessData>();
        }

        public static DocumentContext GetInstance()
        {
            if (_instance != null) return _instance;
            lock (locker)
            {
                if(_instance==null)
                {
                    _instance = new DocumentContext();
                }
            }
            return _instance;
        }

        public void EnQueue(ProcessData entity)
        {
            _processQueue.Enqueue(entity);
        }

        public ProcessData Dequeue()
        {
            return _processQueue.Dequeue();
        }

        public Queue<ProcessData> GetProcesses()
        {
            return _processQueue;
        }

    }

    public class ProcessData
    {
        public ProcessData()
        {
            ParameterDic = new Dictionary<string, string>();
        }

        public string GroupName { get; set; }
        public string ActivityName { get; set; }

        public Dictionary<string, string> ParameterDic { get; set; }

    }
}
