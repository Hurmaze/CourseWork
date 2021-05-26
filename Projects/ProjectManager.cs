
using System.Collections.Generic;
using System;

namespace Projects
{
    public class ProjectManager
    {
        private List<Employee> _employees;
        private List<Project> _projects;
        private event TaskHandlerDelegate ChangeNumOfProjects;
        private event TaskHandlerDelegate TimeChanging;
        public ProjectManager(uint numOfEmployees, TaskHandlerDelegate ChangeNumOfProjects, TaskHandlerDelegate TimeIsRunning)
        {
            _projects = new List<Project>();
            _employees = new List<Employee>();
            for (int i = 0; i < numOfEmployees; i++)
                _employees.Add(new Employee());
            ChangeNumOfProjects += ChangeNumOfProjects;
            TimeChanging += TimeIsRunning;
        }
        public void AddProject(uint numOfWorkers, string description, TaskHandlerDelegate ChangeNumOfTasks)
        {
            if(_employees.Count<numOfWorkers)
                throw new ArgumentException("A required number of workers is more than the company has. ");
            List<Employee> toProject = new List<Employee>();
            foreach (Employee emp in _employees)
            {
                if (emp.OnProject == false)
                    toProject.Add(emp);
                if (toProject.Count == numOfWorkers)
                    break;
            }
            if (toProject.Count<numOfWorkers)
                throw new ArgumentException("A required number of workers is more than free workers. ");
            var temp = new Project(toProject, description, ChangeNumOfTasks);
            _projects.Add(temp);
            ChangeNumOfProjects?.Invoke(this, new TaskHandlerArgs($"Task with an id {temp.ID} has been successfully added. ", temp.ID));
        }
        public void FinishProject(uint ID)
        {
            bool del = false;
            foreach(Project prj in _projects)
            {
                if (prj.ID == ID)
                {
                    prj.FinishProject();
                    _projects.Remove(prj);
                    del = true;
                }    
            }
            if (del == false)
                throw new MissingMemberException($"A project with an id {ID} is not exist. ");
            else
                ChangeNumOfProjects?.Invoke(this, new TaskHandlerArgs($"Task with an id {ID} has been successfully finished. ", ID));
        }
        public Project FindProject(uint id)
        {
            foreach(Project prj in _projects)
            {
                if (prj.ID == id)
                    return prj;
            }
            throw new MissingMemberException($"A project with an id {id} is not exist. ");
        }
        public List<Project> GetProjects()
        {
            var temp = new List<Project>(_projects);
            return temp;
        }
        public List<Employee> GetWorkersCopy()
        {
            var temp = new List<Employee>();
            foreach (Employee worker in _employees)
                temp.Add(new Employee(worker));
            return temp;
        }
        public void Simulate8Hours()
        {
            foreach (Project prj in _projects)
                prj.Simulate8Hours();
            TimeChanging?.Invoke(this, new TaskHandlerArgs("8 hours later..."));
        }
    }
}