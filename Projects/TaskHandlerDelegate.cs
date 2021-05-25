using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projects
{
    public delegate void TaskHandlerDelegate(object sender, TaskHandlerArgs e);
    public class TaskHandlerArgs
    {
        public string Message { get; private set; }
        public uint Id { get; private set; }
        public TaskHandlerArgs(string message) => Message = message;
        public TaskHandlerArgs(string message, uint id)
        {
            Message = message;
            Id = id;
        }
    }
}
