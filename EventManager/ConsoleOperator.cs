using System;
namespace EventManager
{
    public delegate void KeyboardOperatorDelegate(ConsoleCommands command);
    public class ConsoleOperator
    {
        private bool _isStorageSelected = false;

        public ConsoleOperator() { }

        public void ListenForCommands(KeyboardOperatorDelegate listener)
        {
            do
            {
                while (!Console.KeyAvailable)
                {

                    if (_isStorageSelected)
                    {
                        ParseCommand(listener);
                    }
                    else
                    {
                        SelectStorage(listener);
                    }
                }
            } while (Console.ReadKey(true).Key != ConsoleKey.Escape);
        }

        private void ParseCommand(KeyboardOperatorDelegate listener)
        {

            Console.WriteLine("Enter Command:");

            string command = Console.ReadLine();

            switch (command)
            {
                case "-help":
                case "-h":
                    ShowAllCommands();
                    break;
                case "-add":
                case "-a":
                    listener(ConsoleCommands.AddTask);
                    break;
                case "-delete":
                case "-d":
                    listener(ConsoleCommands.DeleteTask);
                    break;
                case "-complete":
                case "-c":
                    listener(ConsoleCommands.CompleteTask);
                    break;
                case "-l":
                case "-list":
                    listener(ConsoleCommands.ShowList);
                    break;
                case "-e":
                case "-edit":
                    listener(ConsoleCommands.EditTask);
                    break;
            }
        }

        private void SelectStorage(KeyboardOperatorDelegate listener)
        {

            Console.WriteLine("Select Storage Type");
            Console.WriteLine("1 - Memory\n2 - Database");
            Console.Write(":");

            string command = Console.ReadLine();

            switch (command)
            {
                case "1":
                    listener(ConsoleCommands.SelectMemoryStorage);
                    _isStorageSelected = true;
                    break;
                case "2":
                    listener(ConsoleCommands.SelectDatabaseStorage);
                    _isStorageSelected = true;
                    break;
                default:
                    SelectStorage(listener);
                    break;
            }

            ShowAllCommands();
        }

        private void ShowAllCommands()
        {
            Console.WriteLine("-----------------------------------");
            Console.WriteLine("\t -h, -help     - Show all available commands");
            Console.WriteLine("\t -a, -add      - Add new task");
            Console.WriteLine("\t -d, -delete   - Delete task");
            Console.WriteLine("\t -c, -complete - Complete task");
            Console.WriteLine("\t -l, -list     - Show full list of tasks");
            Console.WriteLine("\t -e, -edit     - Edit task");
        }
    }
}
