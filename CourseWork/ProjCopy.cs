using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseWork
{
    //public class Propject : IManagly
    //{
    //    private static uint _projectID = 0;
    //    public uint ID { get; private set; }
    //    private event TaskHandlerDelegate _changeNumOfTasks;
    //    private Task[] _tasks;
    //    private List<Employee> _workers;
    //    public string Theme { get; private set; }
    //    private StatusCounter _statusCounter;
    //    public Project(List<Employee> emp, string theme, TaskHandlerDelegate ChangeNumOfTasks)
    //    {
    //        _workers = emp;
    //        _changeNumOfTasks += ChangeNumOfTasks;
    //        Theme = theme;
    //        _statusCounter = new StatusCounter();
    //        ID = ++_projectID;
    //        foreach (Employee worker in _workers)
    //            worker.OnProject = true;
    //    }
    //    public void AddTask(string description, string preview, double timeToDo, Priority priority, uint numOfWorkers,
    //        TaskHandlerDelegate ChangeStatus, TaskHandlerDelegate ChangeDescription)
    //    {
    //        var time = FormatTime(timeToDo);
    //        this.AddTask(description, preview, time, priority, numOfWorkers, ChangeStatus, ChangeDescription);
    //    }
    //    public void AddTask(string description, string preview, (int days, int hours, int minutes) timeToDo, Priority priority, uint numOfWorkers,
    //        TaskHandlerDelegate ChangeStatus, TaskHandlerDelegate ChangeDescription)
    //    {
    //        if (numOfWorkers <= _workers.Count)
    //        {
    //            List<Employee> toTask = new List<Employee>();
    //            for (int i = 0; i < _workers.Count; i++)
    //            {
    //                int countTask = 0;
    //                foreach (Employee worker in _workers)
    //                {
    //                    if (worker.OnTask <= countTask)
    //                    {
    //                        toTask.Add(worker);
    //                        if (toTask.Count == numOfWorkers)
    //                            break;
    //                    }
    //                }
    //                if (toTask.Count == numOfWorkers)
    //                    break;
    //                else
    //                    countTask++;
    //            }
    //            Task newTask = new Task(description, preview, timeToDo, priority, toTask, ChangeStatus, ChangeDescription);
    //            if (_tasks != null)
    //            {
    //                Task[] temp = new Task[_tasks.Length + 1];
    //                for (int i = 0; i < _tasks.Length; i++)
    //                    temp[i] = _tasks[i];
    //                temp[_tasks.Length] = newTask;
    //                _tasks = temp;
    //            }
    //            else
    //            {
    //                _tasks = new Task[1];
    //                _tasks[0] = newTask;
    //            }
    //            _statusCounter.NumOfUnstarted++;
    //            uint id = newTask.ID;
    //            _changeNumOfTasks?.Invoke(this, new TaskHandlerArgs($"Task with an id {id} has been successfully added. ", id));
    //        }
    //        else
    //            throw new ArgumentException("A required number of workers is more than the project has. ");
    //    }
    //    public void DeleteTask(uint id)
    //    {
    //        if (id == 0)
    //            throw new ArgumentOutOfRangeException("Task`s id starts from 1. You have entered 0.");
    //        else
    //        {
    //            Task taskToDel = FindTask(id);
    //            if (taskToDel != null)
    //            {
    //                Task[] temp = new Task[_tasks.Length - 1];
    //                int iterator = 0;
    //                for (int i = 0; i < _tasks.Length; i++)
    //                {
    //                    if (_tasks[i].ID == id)
    //                        continue;
    //                    temp[iterator] = _tasks[i];
    //                    iterator++;
    //                }
    //                _tasks = temp;
    //                foreach (Employee worker in taskToDel._workers)
    //                {
    //                    if (taskToDel.Status == Status.InProgress)
    //                    {
    //                        worker.InWork--;
    //                    }
    //                    worker.OnTask--;
    //                }
    //                switch (taskToDel.Status)
    //                {
    //                    case Status.Unstarted:
    //                        _statusCounter.NumOfUnstarted--;
    //                        break;
    //                    case Status.InProgress:
    //                        _statusCounter.NumOfInProgress--;
    //                        break;
    //                    case Status.Done:
    //                        _statusCounter.NumOfDone--;
    //                        break;
    //                    case Status.Overtermed:
    //                        _statusCounter.NumOfOvertermed--;
    //                        break;
    //                }
    //                _changeNumOfTasks?.Invoke(this, new TaskHandlerArgs($"Task with an id {id} has been successfully deleted. ", id));
    //            }
    //            else
    //                throw new MissingMemberException($"A task with id {id} is not exist. ");
    //        }
    //    }
    //    public Task[] GetAllTasks()
    //    {
    //        if (_tasks != null)
    //        {
    //            Task[] temp = new Task[_tasks.Length];
    //            for (int i = 0; i < _tasks.Length; i++)
    //                temp[i] = new Task(_tasks[i]);
    //            return temp;
    //        }
    //        else
    //            return null;
    //    }
    //    public List<Employee> GetEmployees()
    //    {
    //        List<Employee> temp = new List<Employee>();
    //        foreach (Employee worker in _workers)
    //            temp.Add(new Employee(worker));
    //        return temp;
    //    }
    //    public StatusCounter GetStatusCountInfo()
    //    {
    //        StatusCounter temp = new StatusCounter(_statusCounter);
    //        return temp;
    //    }

    //    public void StartTask(uint id)
    //    {
    //        if (id == 0)
    //            throw new ArgumentOutOfRangeException("Task`s id starts from 1. You have entered 0.");
    //        else
    //        {
    //            Task taskToStart = FindTask(id);
    //            if (taskToStart != null)
    //            {
    //                taskToStart.StartTask();
    //                _statusCounter.NumOfUnstarted--;
    //                _statusCounter.NumOfInProgress++;
    //            }
    //            else
    //                throw new MissingMemberException($"A task with id {id} is not exist. ");
    //        }
    //    }
    //    public void FinishTask(uint id)
    //    {
    //        if (id == 0)
    //            throw new ArgumentOutOfRangeException("Task`s id starts from 1. You have entered 0.");
    //        else
    //        {
    //            Task taskToFinish = FindTask(id);
    //            if (taskToFinish != null)
    //            {
    //                var curStatus = taskToFinish.Status;
    //                taskToFinish.FinishTask();
    //                switch (curStatus)
    //                {
    //                    case Status.InProgress: { _statusCounter.NumOfInProgress--; break; }
    //                    case Status.Unstarted: { _statusCounter.NumOfUnstarted--; break; }
    //                    case Status.Overtermed: { _statusCounter.NumOfOvertermed--; break; }
    //                }
    //                _statusCounter.NumOfDone++;
    //            }
    //            else
    //                throw new MissingMemberException($"A task with id {id} is not exist. ");
    //        }
    //    }
    //    public Task GetSpecificTask(uint id)
    //    {
    //        if (id == 0)
    //            throw new ArgumentOutOfRangeException("Task`s id starts from 1. You have entered 0.");
    //        else
    //        {
    //            Task taskToReturn = FindTask(id);
    //            if (taskToReturn != null)
    //            {
    //                Task temp = new Task(taskToReturn);
    //                return temp;
    //            }
    //            else
    //                throw new MissingMemberException($"A task with id {id} is not exist. ");
    //        }
    //    }
    //    public void ChangeDescritpion(uint id, string desc)
    //    {
    //        Task temp = FindTask(id);
    //        if (temp != null)
    //            temp.Description = desc;
    //        else
    //            throw new MissingMemberException($"A task with id {id} is not exist. ");
    //    }
    //    public void Simulate8Hours()
    //    {
    //        if (_tasks != null)
    //        {
    //            for (int i = 0; i < _tasks.Length; i++)
    //            {
    //                if (_tasks[i].Status == Status.InProgress)
    //                {
    //                    _tasks[i].SimulateHours(8);
    //                    var time = _tasks[i].GetRemainingTime();
    //                    if (time.Item1 <= 0 && time.Item2 <= 0 && time.Item3 <= 0)
    //                    {
    //                        _tasks[i].Overterm();
    //                        _statusCounter.NumOfInProgress--;
    //                        _statusCounter.NumOfOvertermed++;
    //                    }
    //                }
    //            }
    //        }
    //    }
    //    private Task FindTask(uint id)
    //    {
    //        if (_tasks != null)
    //        {
    //            for (int i = 0; i < _tasks.Length; i++)
    //            {
    //                if (_tasks[i].ID == id)
    //                    return _tasks[i];
    //            }
    //            return null;
    //        }
    //        return null;
    //    }
    //    private (int, int, int) FormatTime(double time)
    //    {
    //        (int days, int hours, int minutes) timeToDo;
    //        timeToDo.days = (int)time / 24;
    //        timeToDo.hours = (int)time % 24;
    //        double temp = Math.Round((double)time, 2);
    //        timeToDo.minutes = (int)((temp - Math.Truncate(temp)) * 60);
    //        return timeToDo;
    //    }
    //}
}
