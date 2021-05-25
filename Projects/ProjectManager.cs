
using System.Collections.Generic;
using System;

namespace Projects
{
    public class ProjectManager
    {
        private List<Employee> _employees;
        private List<Project> _projects;
        private event TaskHandlerDelegate _changeNumOfProjects;
        public ProjectManager(uint numOfEmployees, TaskHandlerDelegate ChangeNumOfProjects)
        {
            _projects = new List<Project>();
            _employees = new List<Employee>();
            for (int i = 0; i < numOfEmployees; i++)
                _employees.Add(new Employee());
            _changeNumOfProjects += ChangeNumOfProjects;
        }
        public void AddProject(uint numOfWorkers, string description, TaskHandlerDelegate ChangeNumOfTasks, TaskHandlerDelegate TimeIsRunning)
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
            var temp = new Project(toProject, description, ChangeNumOfTasks, TimeIsRunning);
            _projects.Add(temp);
            _changeNumOfProjects?.Invoke(this, new TaskHandlerArgs($"Task with an id {temp.ID} has been successfully added. ", temp.ID));
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
                _changeNumOfProjects?.Invoke(this, new TaskHandlerArgs($"Task with an id {ID} has been successfully finished. ", ID));
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
    }
}