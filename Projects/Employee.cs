using System.Collections.Generic;
using System;
namespace Projects
{
    public class Employee
    {
        private static uint IDcounter = 0;
        private List<Task> _tasks;
        public Project Project { get; private set; }
        public uint EmployeeID { get; private set; }
        public string Name { get; set; }
        public uint InWork { get; private set; }
        public uint OnTask { get; private set; }
        public bool OnProject { get; private set; }
        public Employee() 
        { 
            EmployeeID = ++IDcounter; 
            OnProject = false; 
            Name = "Misha" + IDcounter; 
            InWork = 0; 
            OnTask = 0;
            _tasks = new List<Task>();
        }
        public Employee(Employee toCopy)
        {
            if (toCopy != null)
            {
                InWork = toCopy.InWork;
                OnTask = toCopy.OnTask;
                Name = toCopy.Name;
                EmployeeID = toCopy.EmployeeID;
                OnProject = toCopy.OnProject;
                _tasks = toCopy.GetAllTasks();
                Project = new Project(toCopy.Project);
            }
            else
                throw new NullReferenceException();
        }
        public void AddOnProject(Project prj)
        {
            if (prj != null)
            {
                OnProject = true;
                Project = prj;
            }
            else
                throw new NullReferenceException();
        }
        public void OutOfProject()
        {
            OnProject = false;
            Project = null;
            OnTask = 0;
            InWork = 0;
            _tasks = null;
        }
        public void AddOnTask(Task task)
        {
            if (task != null)
            {
                _tasks.Add(task);
                OnTask++;
            }
            else
                throw new NullReferenceException();
        }
        public void StartedTask()
        {
            InWork++;
        }
        public void DoneTask(Task task)
        {
            if (task!=null)
            {
                bool del = _tasks.Contains(task);
                if (del)
                {
                    _tasks.Remove(task);
                    OnTask--;
                    if (task.Status == Status.InProgress)
                        InWork--;
                }
                else
                    throw new MissingMemberException();
            }
            else
                throw new NullReferenceException();
        }
        public List<Task> GetAllTasks()
        {
            List<Task> temp = new List<Task>();
            foreach (Task task in _tasks)
                temp.Add(new Task(task));
            return temp;
        }
    }
}
