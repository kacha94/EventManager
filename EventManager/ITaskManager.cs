using System;
using System.Collections.Generic;
using System.Linq;

namespace EventManager
{
    public interface ITaskManager
    {
        void Add(TaskModel task);
        void Edit(TaskModel task);
        void Delete(TaskModel task);
        IEnumerable<TaskModel> Tasks();
    }
}
