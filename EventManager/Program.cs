using System;
using System.Collections.Generic;
using Npgsql;

namespace EventManager
{
    class Program
    {
        static void Main(string[] args)
        {
            var app = new ConsoleApplication();
            app.Start();
        }
    }
}
