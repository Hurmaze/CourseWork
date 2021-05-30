
namespace Projects
{
    public delegate void TaskHandlerDelegate(object sender, TaskHandlerArgs e);
    public class TaskHandlerArgs
    {
        public string Message { get; private set; }
        public uint ID { get; private set; }
        public Status PrevStatus { get; private set; }
        public Status NewStatus { get; private set; }
        public TaskHandlerArgs(string message) { Message = message; }
        public TaskHandlerArgs(string message, uint id)
        {
            Message = message;
            ID = id;
        }
        public TaskHandlerArgs(string message, uint id, Status prevStatus, Status newStatus)
        {
            Message = message;
            ID = id;
            PrevStatus = prevStatus;
            NewStatus = newStatus;
        }
    }
}
