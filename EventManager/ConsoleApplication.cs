using System;
using System.Collections.Generic;
using System.Linq;

namespace EventManager
{
    public class ConsoleApplication
    {
        private ITaskManager _taskManager;
        private ConsoleOperator _keyboardListener;

        public ConsoleApplication()
        {
            _taskManager = new TaskManagerDB();
            _keyboardListener = new ConsoleOperator();
        }

        public void Start()
        {
            _keyboardListener.ListenForCommands(ListenForKeyboardCommands);
        }

        void ListenForKeyboardCommands(ConsoleCommands command)
        {
            switch (command)
            {
                case ConsoleCommands.AddTask:
                    AddTask();
                    break;
                case ConsoleCommands.CompleteTask:
                    CompleteTask();
                    break;
                case ConsoleCommands.DeleteTask:
                    DeleteTask();
                    break;
                case ConsoleCommands.EditTask:
                    EditeTask();
                    break;
                case ConsoleCommands.ShowList:
                    DisplayList();
                    break;
                case ConsoleCommands.SelectDatabaseStorage:
                    _taskManager = new TaskManagerDB();
                    break;
                case ConsoleCommands.SelectMemoryStorage:
                    _taskManager = new TaskManagerLocal();
                    break;
                default:
                    break;
            }
        }

        void DisplayList()
        {
            var t = new TablePrinter("ID", "Title", "Description", "IsCompleted");
            foreach (var task in _taskManager.Tasks())
            {
                t.AddRow(task.Id, task.Title, task.Description, task.IsCompleted);   
            }
            t.Print();
        }

        void AddTask()
        {
            Console.Write("Enter Title:");
            string name = Console.ReadLine();
            Console.Write("Enter Description:");
            string descr = Console.ReadLine();

            var  taskModel = new TaskModel{Title = name, Description =descr };
            _taskManager.Add(taskModel);
            Console.WriteLine("New task was successfuly added");
        }

        void CompleteTask()
        {
            Console.Write("Enter ID:");
            string id = Console.ReadLine();

            var existTask = _taskManager.Tasks().FirstOrDefault(x => x.Id == id);
            if (existTask != null)
            {
                existTask.IsCompleted = true;
                _taskManager.Edit(existTask);
                Console.WriteLine("Task with given ID {0} was successfuly completed", id);
            }
            else
            {
                Console.WriteLine("Task with given ID {0} wasn't found", id);
            }
        }

        void DeleteTask()
        {
            Console.Write("Enter ID:");
            string id = Console.ReadLine();

            var existTask = _taskManager.Tasks().FirstOrDefault(x => x.Id == id);
            if (existTask != null)
            {
                existTask.IsCompleted = true;
                _taskManager.Delete(existTask);
                Console.WriteLine("Task was successfuly deleted");
            }
            else
            {
                Console.WriteLine("Task with given ID {0} wasn't found", id);
            }
        }

        void EditeTask()
        {
            Console.Write("Enter ID:");
            string id = Console.ReadLine();
            Console.Write("Enter Title:");
            string title = Console.ReadLine();
            Console.Write("Enter Description:");
            string description = Console.ReadLine();
            
            var existTask = _taskManager.Tasks().FirstOrDefault(x => x.Id == id);
            if (existTask != null)
            {
                existTask.Title = title;
                existTask.Description = description;
                _taskManager.Edit(existTask);
                Console.WriteLine("Task was successfuly edited");
            }
            else
            {
                Console.WriteLine("Task with given ID {0} wasn't found", id);
            }
        }
    }
}

