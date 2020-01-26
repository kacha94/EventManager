using System;
using System.Collections.Generic;
using System.Linq;

namespace EventManager
{
    public class TaskManagerLocal: ITaskManager
    {
        private List<TaskModel> _taskList;

        public TaskManagerLocal()
        {
          _taskList= new List<TaskModel>();
        }

        public void Add(TaskModel task)
        {
            _taskList.Add(task);
        }
        
        public void Edit(TaskModel task)
        {
            var existTask = _taskList.FirstOrDefault(x => x.Id == task.Id);
            existTask = task;
        }

        public void Delete(TaskModel task)
        {
            _taskList.Remove(task);
        }
        
        public IEnumerable<TaskModel> Tasks()
        {
            return _taskList;
        }
    }
}
