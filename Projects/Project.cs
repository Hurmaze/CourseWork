using System;
using System.Collections.Generic;

namespace Projects
{
    public class Project
    {
        private static uint _counterID = 0;
        public readonly uint ID;
        private event TaskHandlerDelegate ChangeNumOfTasks;
        private List<Task> _tasks;
        private List<Employee> _workers;
        public string Theme { get; private set; }
        private StatusCounter _statusCounter;
        public Project(List<Employee> emp, string theme, TaskHandlerDelegate ChangeNumOfTasks)
        {
            if (emp != null)
            {
                _workers = emp;
                _tasks = new List<Task>();
                this.ChangeNumOfTasks += ChangeNumOfTasks;
                Theme = theme;
                _statusCounter = new StatusCounter();
                ID = ++_counterID;
                foreach (Employee worker in _workers)
                    worker.AddOnProject(this);
            }
            else
                throw new NullReferenceException();
        }
        public Project(Project toCopy)
        {
            if (toCopy != null)
            {
                ID = toCopy.ID;
                ChangeNumOfTasks = toCopy.ChangeNumOfTasks;
                _tasks = toCopy.GetTasksCopy();
                _workers = toCopy.GetWorkersCopy();
                Theme = toCopy.Theme;
                _statusCounter = new StatusCounter(toCopy._statusCounter);
            }
            else
                throw new NullReferenceException();
        }
        public void AddTask(string description, string preview, double timeToDo, Priority priority, 
            TaskHandlerDelegate ChangeStatus, TaskHandlerDelegate ChangeDescription)
        {
            var time = FormatTime(timeToDo);
            this.AddTask(description, preview, time, priority, ChangeStatus, ChangeDescription);
        }
        public void AddTask(string description, string preview, (int days, int hours, int minutes) timeToDo, Priority priority, 
            TaskHandlerDelegate ChangeStatus, TaskHandlerDelegate ChangeDescription)
        {
            Employee toTask = null;
            int countTask = 0;
            while(true)
            {
                foreach (Employee worker in _workers)
                {
                    if (worker.OnTask <= countTask)
                    {
                        toTask = worker;
                        break;
                    }
                }
                if (toTask != null)
                    break;
                else
                    countTask++;
            }
            Task newTask = new Task(description, preview, timeToDo, priority, ChangeStatus, ChangeDescription);
            _tasks.Add(newTask);
            toTask.AddOnTask(newTask);
            _statusCounter.NumOfUnstarted++;
            uint id = newTask.ID;
            ChangeNumOfTasks?.Invoke(this, new TaskHandlerArgs($"Task with an id {id} has been successfully added. ", id)); 
        }
        public void DeleteTask(uint id)
        {
            if (id == 0)
                throw new ArgumentOutOfRangeException("Task`s id starts from 1. You have entered 0.");
            else
            {
                Task taskToDel = FindTask(id);
                if (taskToDel != null)
                {
                    foreach(Employee worker in _workers)
                        if (worker.Has(taskToDel))
                        {
                            worker.DoneTask(taskToDel);
                            break;
                        }
                    _tasks.Remove(taskToDel);
                    switch (taskToDel.Status)
                    {
                        case Status.Unstarted:
                            _statusCounter.NumOfUnstarted--;
                            break;
                        case Status.InProgress:
                            _statusCounter.NumOfInProgress--;
                            break;
                        case Status.Done:
                            _statusCounter.NumOfDone--;
                            break;
                        case Status.Overtermed:
                            _statusCounter.NumOfOvertermed--;
                            break;
                    }
                    ChangeNumOfTasks?.Invoke(this, new TaskHandlerArgs($"Task with an id {id} has been successfully deleted. ", id));
                }
                else
                    throw new MissingMemberException($"A task with id {id} is not exist. ");
            }
        }
        public List<Task> GetTasksCopy()
        {
            List<Task> temp = new List<Task>();
            foreach (Task task in _tasks)
                temp.Add(new Task(task));
            return temp;
        }
        public List<Employee> GetWorkersCopy()
        {
            List<Employee> temp = new List<Employee>();
            foreach (Employee worker in _workers)
                temp.Add(new Employee(worker));
            return temp;
        }
        public StatusCounter GetStatusCountInfo()
        {
            StatusCounter temp = new StatusCounter(_statusCounter);
            return temp;
        }

        public void StartTask(uint id)
        {
            if (id == 0)
                throw new ArgumentOutOfRangeException("Task`s id starts from 1. You have entered 0.");
            else
            {
                Task taskToStart = FindTask(id);
                if (taskToStart != null)
                {
                    foreach (Employee worker in _workers)
                        if (worker.Has(taskToStart))
                        {
                            worker.StartTask();
                            break;
                        }
                    taskToStart.StartTask();
                    _statusCounter.NumOfUnstarted--;
                    _statusCounter.NumOfInProgress++;
                }
                else
                    throw new MissingMemberException($"A task with id {id} is not exist. ");
            }
        }
        public void FinishTask(uint id)
        {
            if (id == 0)
                throw new ArgumentOutOfRangeException("Task`s id starts from 1. You have entered 0.");
            else
            {
                Task taskToFinish = FindTask(id);
                if (taskToFinish != null)
                {
                    var curStatus = taskToFinish.Status;
                    foreach (Employee worker in _workers)
                        if (worker.Has(taskToFinish))
                        {
                            worker.DoneTask(taskToFinish);
                            break;
                        }
                    taskToFinish.FinishTask();
                    switch (curStatus)
                    {
                        case Status.InProgress: { _statusCounter.NumOfInProgress--; break; }
                        case Status.Unstarted: { _statusCounter.NumOfUnstarted--; break; }
                        case Status.Overtermed: { _statusCounter.NumOfOvertermed--; break; }
                    }
                    _statusCounter.NumOfDone++;
                }
                else
                    throw new MissingMemberException($"A task with id {id} is not exist. ");
            }
        }
        public Task GetSpecificTask(uint id)
        {
            if (id == 0)
                throw new ArgumentOutOfRangeException("Task`s id starts from 1. You have entered 0.");
            else
            {
                Task taskToReturn = FindTask(id);
                if (taskToReturn != null)
                {
                    Task temp = new Task(taskToReturn);
                    return temp;
                }
                else
                    throw new MissingMemberException($"A task with id {id} is not exist. ");
            }
        }
        public void ChangeDescritpion(uint id, string desc)
        {
            Task temp = FindTask(id);
            if (temp != null)
                temp.Description = desc;
            else
                throw new MissingMemberException($"A task with id {id} is not exist. ");
        }
        public void Simulate8Hours()
        {
            for (int i = 0; i < _tasks.Count; i++)
            {
                if (_tasks[i].Status == Status.InProgress)
                {
                    _tasks[i].SimulateHours(8);
                    var time = _tasks[i].GetRemainingTime();
                    if (time.Item1 <= 0 && time.Item2 <= 0 && time.Item3 <= 0)
                    {
                        _tasks[i].Overterm();
                        _statusCounter.NumOfInProgress--;
                        _statusCounter.NumOfOvertermed++;
                    }
                }
            }
        }
        public void FinishProject()
        {
            foreach(Employee emp in _workers)
                emp.OutOfProject();
        }
        private Task FindTask(uint id)
        {
            if (_tasks != null)
            {
                for (int i = 0; i < _tasks.Count; i++)
                {
                    if (_tasks[i].ID == id)
                        return _tasks[i];
                }
                return null;
            }
            return null;
        }
        private (int, int, int) FormatTime(double time)
        {
            (int days, int hours, int minutes) timeToDo;
            timeToDo.days = (int)time / 24;
            timeToDo.hours = (int)time % 24;
            double temp = Math.Round((double)time, 2);
            timeToDo.minutes = (int)((temp - Math.Truncate(temp)) * 60);
            return timeToDo;
        }
    }
}
