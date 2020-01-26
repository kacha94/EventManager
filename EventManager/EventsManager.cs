using System;
namespace EventManager
{
    public class EventsManager
    {
        public EventsManager()
        {
        }

        public void CreateTask()
        {
            Console.Write("Enter Title:");
            string name = Console.ReadLine();
            Console.Write("Enter Description:");
            string descr = Console.ReadLine();

            Console.WriteLine("-----------------------------------");
            Console.WriteLine(name);
            Console.WriteLine(descr);
        }

        private void AddTask()
        {

        }
    }
}
