using System;

namespace Projects
{
    public class Task
    {
        public event TaskHandlerDelegate DescriptionEvent;
        public event TaskHandlerDelegate ChangeStatusEvent;
        private static uint _counterID = 0;
        public readonly uint ID;
        private (int days, int hours, int minutes) _timeToDo;
        private string _description;
        private string _name;
        public Status Status { get; private set; }
        public Priority Priority { get; private set; }
        /// <exception cref="ArgumentException"></exception>
        public string Description
        {
            set
            {
                if (value.Length >= 3)
                {
                    _description = value;
                    DescriptionEvent?.Invoke(this, new TaskHandlerArgs("The description has been successfully changed."));
                }
                else
                    throw new ArgumentException("The description is too short. The task hasn`t been created. ");
            }
            get
            {
                return _description;
            }
        }
        /// <exception cref="ArgumentException"></exception>
        public string Name
        {
            get { return _name; }
            set
            {
                if (value.Length >= 3 && value.Length <= 25)
                    _name = value;
                else if (value.Length <= 3)
                    throw new ArgumentException("The name is too short. The task hasn`t been created. ");
                else
                    throw new ArgumentException("The name is too long. The task hasn`t been created. ");
            }
        }
        public Task(string description, string name, (int days, int hours, int minutes) time, Priority priority,
            TaskHandlerDelegate ChangeStatus, TaskHandlerDelegate ChangeDescription, TaskHandlerDelegate StatusCounter)
        {
            ChangeStatusEvent += ChangeStatus;
            ChangeStatusEvent += StatusCounter;
            Description = description;
            DescriptionEvent += ChangeDescription;
            Name = name;
            SetTimeToDo(time.days, time.hours, time.minutes);
            Priority = priority;
            Status = Status.Unstarted;
            ID = ++_counterID;
        }
        public Task(Task toCopy)
        {
            if (toCopy != null)
            {
                _description = toCopy._description;
                Name = toCopy.Name;
                ID = toCopy.ID;
                _timeToDo = toCopy._timeToDo;
                Status = toCopy.Status;
                Priority = toCopy.Priority;
            }
        }
        public (int, int, int) GetRemainingTime()
        {
            return _timeToDo;
        }
        /// <exception cref="InvalidOperationException"></exception>
        public void StartTask()
        {
            if (Status == Status.Unstarted)
            {
                var prev = Status;
                Status = Status.InProgress;
                ChangeStatusEvent?.Invoke(this, new TaskHandlerArgs($"Task with an id {ID} has been successfully started. ", ID, prev, Status));
            }
            else if (Status == Status.InProgress)
                throw new InvalidOperationException("The task has been already started. Current status is InProgress");
            else
                throw new InvalidOperationException("The task has been already finished.");
        }
        /// <exception cref="InvalidOperationException"></exception>
        public void FinishTask()
        {
            if (Status == Status.Done)
                throw new InvalidOperationException("The task has been already finished.");
            else
            {
                var prev = Status;
                Status = Status.Done;
                ChangeStatusEvent?.Invoke(this, new TaskHandlerArgs($"Task with an id {ID} has been successfully finished. ", ID, prev, Status));
            }
        }
        public void Overterm()
        {
            var prev = Status;
            Status = Status.Overtermed;
            ChangeStatusEvent?.Invoke(this, new TaskHandlerArgs($"Task with an id {ID} has been overtermed. ", ID, prev, Status));
        }
        public void SimulateHours(int hours)
        {
            if (hours <= _timeToDo.hours)
                _timeToDo.hours -= hours;
            else if (hours >= _timeToDo.hours && hours < 24)
            {
                if (_timeToDo.days - 1 < 0)
                {
                    _timeToDo.days = 0;
                    _timeToDo.hours = 0;
                    _timeToDo.minutes = 0;
                }
                else
                {
                    _timeToDo.days--;
                    _timeToDo.hours += 24 - hours;
                }
            }
            else
            {
                _timeToDo.days -= hours / 24;
                _timeToDo.hours -= hours % 24;
            }
        }
        private void SetTimeToDo(int days, int hours, int minutes)
        {
            _timeToDo.days = days;
            if (hours >= 24)
            {
                _timeToDo.days += hours / 24;
                _timeToDo.hours = hours % 24;
            }
            else
                _timeToDo.hours = hours;
            if (minutes >= 60)
            {
                _timeToDo.days += minutes / 3600;
                int tempHours = minutes - minutes / 3600;
                _timeToDo.hours += tempHours / 60;
                _timeToDo.minutes = tempHours % 60;
            }
            else
                _timeToDo.minutes = minutes;
        }
    }
}
