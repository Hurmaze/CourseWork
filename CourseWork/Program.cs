using System;
using System.Collections.Generic;
using Projects;

namespace CourseWork
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.ForegroundColor = ConsoleColor.Black;
            Console.BackgroundColor = ConsoleColor.White;
            Console.Clear();
            var manager = CreateManager();
            ManagerManipulate(manager);
        }
        static void ManagerManipulate(ProjectManager manager)
        {
            bool alive = true;
            while (alive)
            {
                ConsoleColor color = Console.ForegroundColor;
                Console.ForegroundColor = ConsoleColor.DarkGreen;
                Console.WriteLine("What do you want to do? ");
                Console.WriteLine(" 1. Create a project.   2.Manipulate with a project.  ");
                Console.WriteLine(" 3. Show all projects.  4. Finish a project.       ");
                Console.WriteLine(" 5. Show all workers.   6. Simulate 8 hours. ");
                Console.WriteLine(" 7. Leave the program. ");
                Console.ForegroundColor = color;
                try
                {
                    int command = Int32.Parse(Console.ReadLine());
                    switch (command)
                    {
                        case 1:
                            CreateProject(manager);
                            break;
                        case 2:
                            ProjectManipulate(manager);
                            break;
                        case 3:
                            ShowAllProjects(manager);
                            break;
                        case 4:
                            FinishProject(manager);
                            break;
                        case 5:
                            ShowInfo(manager);
                            break;
                        case 6:
                            manager.Simulate8Hours();
                            break;
                        case 7:
                            alive = false;
                            break;
                    }
                }
                catch (Exception e)
                {
                    var clr = Console.ForegroundColor;
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.WriteLine(e.Message);
                    Console.ForegroundColor = clr;
                }
            }
        }
        static void ProjectManipulate(ProjectManager manager)
        {
            bool alive = true;
            uint id = 0;
            Console.WriteLine("Write an id of the project. ");
            while (alive)
            {
                try
                {
                    id = UInt32.Parse(Console.ReadLine());
                    alive = false;
                }
                catch { Console.WriteLine("You have entered a wrong value. Try again. "); };
            }
            alive = true;
            var project = manager.FindProject(id);
            while (alive)
            {
                ConsoleColor color = Console.ForegroundColor;
                Console.ForegroundColor = ConsoleColor.DarkGreen;
                Console.WriteLine("What do you want to do? ");
                Console.WriteLine(" 1. Create a new task.          2. Show all tasks.      3. Start task. ");
                Console.WriteLine(" 4. Finish task.                5. Delete task.         6. Change description.");
                Console.WriteLine(" 7. Get full info about task.   8. Show all statuses.   9. Info about employees. ");
                Console.WriteLine(" 10. Leave the project. ");
                Console.ForegroundColor = color;
                try
                {
                    int command = Int32.Parse(Console.ReadLine());
                    switch (command)
                    {
                        case 1:
                            {
                                CreateTask(project);
                                break;
                            }
                        case 2:
                            {
                                ShowInfo(project, "general");
                                break;
                            }
                        case 3:
                            {
                                TaskOperator(project, "start");
                                break;
                            }
                        case 4:
                            {
                                TaskOperator(project, "finish");
                                break;
                            }
                        case 5:
                            {
                                TaskOperator(project, "delete");
                                break;
                            }
                        case 6:
                            {
                                TaskOperator(project, "description");
                                break;
                            }
                        case 7:
                            {
                                ShowInfo(project, "detailed");
                                break;
                            }
                        case 8:
                            {
                                ShowStatutes(project);
                                break;
                            }
                        case 9:
                            {
                                ShowInfo(project, "workers");
                                break;
                            }
                        case 10:
                            {
                                alive = false;
                                break;
                            }
                    }

                }
                catch (Exception e)
                {
                    var clr = Console.ForegroundColor;
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    if (e is ArgumentOutOfRangeException)
                    {
                        var ex = e as ArgumentOutOfRangeException;
                        Console.WriteLine(ex.ParamName);
                    }
                    else
                        Console.WriteLine(e.Message);
                    Console.ForegroundColor = clr;
                }
            }
        }
        static ProjectManager CreateManager()
        {
            bool alive = true;
            uint emp = 0;
            while (alive)
            {
                try
                {
                    Console.WriteLine("How many employees your company has?");
                    emp = UInt32.Parse(Console.ReadLine());
                    alive = false;
                }
                catch { Console.WriteLine("You have entered a wrong value. The value should be positive integer number. "); };
            }
            ProjectManager manager = new ProjectManager(emp, ChangeNumOfTasksHandler, TimeHandler);
            return manager;
        }
        static void CreateProject(ProjectManager manager)
        {
            var clr = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.WriteLine("How many workers are required?");
            bool alive = true;
            uint emp = 0;
            while (alive)
            {
                try
                {
                    emp = UInt32.Parse(Console.ReadLine());
                    alive = false;
                }
                catch { Console.WriteLine("You have entered a wrong value. The value should be positive integer number. "); };
            }
            Console.WriteLine("Write a theme of project: ");
            string theme = Console.ReadLine();
            manager.AddProject(emp, theme, ChangeNumOfTasksHandler);
            Console.ForegroundColor = clr;
        }
        static void FinishProject(ProjectManager manager)
        {
            var clr = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.WriteLine("Write an id of the project. ");
            bool alive = true;
            uint id = 0;
            while (alive)
            {
                try
                {
                    id = UInt32.Parse(Console.ReadLine());
                    alive = false;
                }
                catch { Console.WriteLine("You have entered a wrong value. Try again. "); };
            }
            manager.FinishProject(id);
            Console.ForegroundColor = clr;
        }
        static void CreateTask(Project project)
        {
            var clr = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.WriteLine("Write description of the task (more than 3 characters): ");
            string descr = Console.ReadLine();
            Console.WriteLine("Write name of the task: ");
            string name = Console.ReadLine();
            Console.WriteLine("How much time you have to do it? ");
            Console.WriteLine("1. Write in hours. 2. Write in days/hours/minutes. ");
            bool option = true;
            bool time = true;
            int cmd = 1;
            while (option)
            {
                try
                {
                    cmd = Int32.Parse(Console.ReadLine());
                    if (cmd != 1 && cmd != 2)
                        throw new ArgumentException();
                    option = false;
                }
                catch
                {
                    Console.WriteLine("You have ntered wrong value. Try again. ");
                }
            }
            int hours = 0;
            (int days, int hours, int minutes) timeToDo = (0, 0, 0);
            switch (cmd)
            {
                case 1:
                    {
                        Console.WriteLine("Write a number of hours. ");
                        while (time)
                        {
                            try
                            {
                                hours = Int32.Parse(Console.ReadLine());
                                if (hours < 0)
                                    throw new Exception();
                                time = false;
                            }
                            catch { Console.WriteLine("You have entered wrong value. Try again. "); }
                        }
                        break;
                    }
                case 2:
                    {
                        Console.WriteLine("Write a number of days.");
                        while (time)
                        {
                            try
                            {
                                timeToDo.days = Int32.Parse(Console.ReadLine());
                                if (timeToDo.days < 0)
                                    throw new Exception();
                                time = false;
                            }
                            catch { Console.WriteLine("You have entered wrong value. Try again. "); }
                        }
                        Console.WriteLine("Write a number of hours. ");
                        time = true;
                        while (time)
                        {
                            try
                            {
                                timeToDo.hours = Int32.Parse(Console.ReadLine());
                                if (timeToDo.hours < 0)
                                    throw new Exception();
                                time = false;
                            }
                            catch { Console.WriteLine("You have entered wrong value. Try again. "); }
                        }
                        Console.WriteLine("Write a number of minutes. ");
                        time = true;
                        while (time)
                        {
                            try
                            {
                                timeToDo.minutes = Int32.Parse(Console.ReadLine());
                                if (timeToDo.minutes < 0)
                                    throw new Exception();
                                time = false;
                            }
                            catch { Console.WriteLine("You have entered wrong value. Try again. "); }
                        }
                        break;
                    }
            }
            Console.WriteLine("Choose priority: 1. High. 2. Medium. 3. Low. ");
            option = true;
            Priority prior = Priority.Low;
            while (option)
            {
                try
                {
                    cmd = Int32.Parse(Console.ReadLine());
                    if (cmd != 1 && cmd != 2 && cmd != 3)
                        throw new ArgumentException();
                    option = false;
                }
                catch { Console.WriteLine("You have entered wrong value. Try again. "); }
            }
            switch (cmd)
            {
                case 1: { prior = Priority.High; break; }
                case 2: { prior = Priority.Medium; break; }
                case 3: { prior = Priority.Low; break; }
            }
            if (hours == 0)
                project.AddTask(descr, name, timeToDo, prior, ChangeStatusHandler, ChangeDescriptionHandler);
            else
                project.AddTask(descr, name, hours, prior, ChangeStatusHandler, ChangeDescriptionHandler);
            Console.ForegroundColor = clr;
        }
        static void ShowInfo(Project project, string option)
        {
            var clr = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Black;
            switch (option)
            {
                case "general":
                    {
                        List<Task> tasks = project.GetTasksCopy();
                        if (tasks != null)
                        {
                            for (int i = 0; i < tasks.Count; i++)
                            {
                                if (tasks[i].Status == Status.InProgress)
                                {
                                    var time = tasks[i].GetRemainingTime();
                                    Console.WriteLine($"Id:{tasks[i].ID} Name: {tasks[i].Name}. Priority: {tasks[i].Priority}. Status: {tasks[i].Status}. Remaining time: {time.Item1} days, {time.Item2} hours, {time.Item3} minutes. ");
                                }
                                else if (tasks[i].Status == Status.Unstarted)
                                {
                                    var time = tasks[i].GetRemainingTime();
                                    Console.WriteLine($"Id:{tasks[i].ID} Name: {tasks[i].Name}. Priority: {tasks[i].Priority}. Status: {tasks[i].Status}. Time to do:     {time.Item1} days, {time.Item2} hours, {time.Item3} minutes. ");
                                }
                                else
                                    Console.WriteLine($"Id:{tasks[i].ID} Name: {tasks[i].Name}. Priority: {tasks[i].Priority}. Status: {tasks[i].Status}. ");
                            }
                            break;
                        }
                        else
                        {
                            Console.WriteLine("You have no tasks now");
                            break;
                        }
                    }
                case "detailed":
                    {
                        bool alive = true;
                        uint id = 0;
                        Console.WriteLine("Write an id of the task. ");
                        while (alive)
                        {
                            try
                            {
                                id = UInt32.Parse(Console.ReadLine());
                                alive = false;
                            }
                            catch { Console.WriteLine("You have entered a wrong value. Try again. "); };
                        }
                        Task task = project.GetSpecificTask(id);
                        if (task.Status == Status.InProgress)
                        {
                            var time = task.GetRemainingTime();
                            Console.WriteLine($"Id:{task.ID} Name: {task.Name}. Priority: {task.Priority}. Status: {task.Status}. Remaining time: {time.Item1} days, {time.Item2} hours, {time.Item3} minutes. ");
                        }
                        else if (task.Status == Status.Unstarted)
                        {
                            var time = task.GetRemainingTime();
                            Console.WriteLine($"Id:{task.ID} Name: {task.Name}. Priority: {task.Priority}. Status: {task.Status}. Time to do:     {time.Item1} days, {time.Item2} hours, {time.Item3} minutes. ");
                        }
                        else
                        {
                            Console.WriteLine($"Id:{task.ID} Name: {task.Name}. Priority: {task.Priority}. Status: {task.Status}. ");
                        }
                        Console.WriteLine($"Description: {task.Description}");
                        break;
                    }
                case "workers":
                    {
                        var temp = project.GetWorkersCopy();
                        Console.WriteLine($"Number of projects` workers: {temp.Count}");
                        foreach(Employee emp in temp)
                            Console.WriteLine($"Worker {emp.Name} with id {emp.EmployeeID} has {emp.OnTask} tasks, and is doing right now {emp.InWork} tasks.");
                        break;
                    }
            }
            Console.ForegroundColor = clr;
        }
        static void ShowInfo(ProjectManager manager)
        {
            var employees = manager.GetWorkersCopy();
            foreach(Employee emp in employees)
            {
                if (emp.Project!=null)
                    Console.WriteLine($"ID: {emp.EmployeeID}, Name: {emp.Name}, Project: {emp.Project}, Has {emp.OnTask} tasks. ");
                else
                    Console.WriteLine($"ID: {emp.EmployeeID}, Name: {emp.Name}, Project: Out of project, Has {emp.OnTask} tasks. ");
            }
        }
        static void ShowAllProjects(ProjectManager manager)
        {
            var clr = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Black;
            var projects = manager.GetProjects();
            foreach(Project prj in projects)
            {
                Console.WriteLine($"ID: {prj.ID}, Number of workers: {prj.GetWorkersCopy().Count}, Theme: {prj.Theme}");
            }
            Console.ForegroundColor = clr;
        }
        static void TaskOperator(Project project, string option)
        {
            var clr = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.WriteLine("Write an id of the task. ");
            bool alive = true;
            uint id = 0;
            while (alive)
            {
                try
                {
                    id = UInt32.Parse(Console.ReadLine());
                    alive = false;
                }
                catch { Console.WriteLine("You have entered a wrong value. Try again. "); };
            }
            switch (option)
            {
                case "start": { project.StartTask(id); break; }
                case "finish": { project.FinishTask(id); break; }
                case "delete": { project.DeleteTask(id); break; }
                case "description":
                    {
                        var task = project.GetSpecificTask(id);
                        Console.WriteLine("Current description: ");
                        Console.WriteLine(task.Description);
                        Console.WriteLine("Write a new description: ");
                        string desc = Console.ReadLine();
                        project.ChangeDescritpion(id, desc);
                        break;
                    }
            }
            Console.ForegroundColor = clr;
        }
        static void ShowStatutes(Project project)
        {
            var clr = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Black;
            StatusCounter stats = project.GetStatusCountInfo();
            Console.WriteLine($"Number of unstarted:  {stats.NumOfUnstarted}");
            Console.WriteLine($"Number of continuous: {stats.NumOfInProgress}");
            Console.WriteLine($"Number of done:       {stats.NumOfDone}");
            Console.WriteLine($"Number of overtermed: {stats.NumOfOvertermed}");
            Console.ForegroundColor = clr;
        }
        static void ChangeStatusHandler(object sender, TaskHandlerArgs args)
        {
            var clr = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.WriteLine(args.Message);
            Console.ForegroundColor = clr;
        }
        static void ChangeNumOfTasksHandler(object sender, TaskHandlerArgs args)
        {
            var clr = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.WriteLine(args.Message);
            Console.ForegroundColor = clr;
        }
        static void ChangeDescriptionHandler(object sender, TaskHandlerArgs args)
        {
            var clr = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.WriteLine(args.Message);
            Console.ForegroundColor = clr;
        }
        static void TimeHandler(object sender, TaskHandlerArgs args)
        {
            var clr = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.WriteLine(args.Message);
            Console.ForegroundColor = clr;
        }
    }
}
