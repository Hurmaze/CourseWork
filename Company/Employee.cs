using System.Collections.Generic;

namespace Company
{
    public class Employee
    {
        private static uint IDcounter = 0;
        public uint EmployeeID { get; private set; }
        public string Name { get; set; }
        public uint InWork;
        public uint OnTask;
        public bool OnProject { get; set; }
        public Employee() { EmployeeID = ++IDcounter; OnProject = false; Name = "Misha" + EmployeeID; InWork = 0; OnTask = 0;  }
        public Employee(Employee toCopy)
        {
            InWork = toCopy.InWork;
            OnTask = toCopy.OnTask;
            Name = toCopy.Name;
            EmployeeID = toCopy.EmployeeID;
            OnProject = toCopy.OnProject;
        }
        public void AddTask(Task task,)
        {

        }
    }
}
