using System;
using System.Collections.Generic;

namespace Projects
{
    public class Task
    {
        private event TaskHandlerDelegate _descriptionEvent;
        private event TaskHandlerDelegate _changeStatusEvent;
        private static uint _counterId = 0;
        public uint ID { get; private set; }
        private List<Employee> _workers;
        private (int days, int hours, int minutes) _timeToDo;
        private string _description;
        private string _name;
        public Status Status { get; private set; }
        public Priority Priority { get; set; }
        public string Description
        {
            set
            {
                if (value.Length >= 3)
                {
                    _description = value;
                    _descriptionEvent?.Invoke(this, new TaskHandlerArgs("The description has been successfully changed."));
                }
                else
                    throw new ArgumentException("The description is too short. The task hasn`t been created. ");
            }
            get
            {
                return _description;
            }
        }
        public string Name
        {
            get { return _name; }
            set
            {
                if (value.Length >= 3 && value.Length <= 25)
                    _name = value;
                else if (value.Length <= 3)
                    throw new ArgumentException("The preview is too short. The task hasn`t been created. ");
                else
                    throw new ArgumentException("The preview is too long. The task hasn`t been created. ");
            }
        }
        public Task(string description, string name, (int days, int hours, int minutes) time, Priority priority, List<Employee> workers,
            TaskHandlerDelegate ChangeStatus, TaskHandlerDelegate ChangeDescription)
        {
            if (workers!=null)
            {
                _changeStatusEvent += ChangeStatus;
                Description = description;
                _descriptionEvent += ChangeDescription;
                Name = name;
                SetTimeToDo(time.days, time.hours, time.minutes);
                Priority = priority;
                Status = Status.Unstarted;
                _workers = workers;
                ID = ++_counterId;
                foreach (Employee worker in _workers)
                    worker.AddOnTask(this);
            }
        }
        public Task(Task toCopy)
        {
            if (toCopy != null)
            {
                _workers = new List<Employee>();
                _description = toCopy._description;
                Name = toCopy.Name;
                ID = toCopy.ID;
                foreach (Employee worker in toCopy._workers)
                    _workers.Add(new Employee(worker));
                _timeToDo = toCopy._timeToDo;
                Status = toCopy.Status;
                Priority = toCopy.Priority;
            }
        }
        public (int, int, int) GetRemainingTime()
        {
            return _timeToDo;
        }
        public void StartTask()
        {
            if (Status == Status.Unstarted)
            {
                Status = Status.InProgress;
                foreach (Employee worker in _workers)
                    worker.StartedTask();
                _changeStatusEvent?.Invoke(this, new TaskHandlerArgs($"Task with an id {ID} has been successfully started. ", ID));
            }
            else if (Status == Status.InProgress)
                throw new InvalidOperationException("The task has been already started. Current status is InProgress");
            else
                throw new InvalidOperationException("The task has been already finished.");
        }
        public void FinishTask()
        {
            if (Status == Status.Done)
                throw new InvalidOperationException("The task has been already finished.");
            else
            {
                Status = Status.Done;
                foreach(Employee worker in _workers)
                {
                    worker.DoneTask(this);
                }
                _changeStatusEvent?.Invoke(this, new TaskHandlerArgs($"Task with an id {ID} has been successfully finished. ", ID));
            }
        }
        public void DeleteTask()
        {
            foreach (Employee emp in _workers)
                emp.DoneTask(this);
        }
        public void Overterm()
        {
            Status = Status.Overtermed;
            _changeStatusEvent?.Invoke(this, new TaskHandlerArgs($"Task with an id {ID} has overtermed. ", ID));
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
        public List<Employee> GetWorkers()
        {
            var temp = new List<Employee>();
            foreach (Employee emp in _workers)
                temp.Add(emp);
            return temp;
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
