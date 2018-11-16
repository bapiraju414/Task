using Moq;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Linq;
using System.Text;
using TaskManager.BusinessLayer;
using TaskManager.DataLayer;
using TaskManager.Entities;

namespace TaskManagerTest
{
    public class MockTaskManagerBL
    {
        public Collection<Task> GetTask()
        {
            var data = new Collection<Task>()
            {
                new Task()
                {
                    Task_ID = 1,
                    TaskName = "Task 1",
                       Priority =10,
                       Start_Date = DateTime.Now.Date,
                       End_Date = DateTime.Now.AddDays(10)
                       
                },
                new Task()
                {
                    Task_ID = 2,
                    TaskName = "Task 2",
                       Priority =10,
                       Start_Date = DateTime.Now.Date,
                       End_Date = DateTime.Now.AddDays(10)

                },
                 new Task()
                {
                    Task_ID = 3,
                    TaskName = "Task 3",
                       Priority =10,
                       Start_Date = DateTime.Now.Date,
                       End_Date = DateTime.Now.AddDays(10)

                }
            };

            return data;
        }

        public Collection<string> GetParentTasks(int? taskId = null)
        {
            Collection<Task> tasks = GetTask();
            var data = new Collection<string>();
            tasks.Where(x => (taskId == null) || (x.Task_ID != taskId))
                  .Select(t => t.TaskName).ToList()
                  .ForEach(y => data.Add(y));
            return data;
        }

        public Task GetTaskById(int taskId)
        {
            Collection<Task> tasks = GetTask();
            var data = tasks.Where(x => x.Task_ID == taskId)
                .Select(t => new Task()
                {
                    Task_ID = t.Task_ID,
                    TaskName = t.TaskName,                   
                    Priority = t.Priority,
                    Start_Date = t.Start_Date,
                    End_Date = t.End_Date                    
                }).FirstOrDefault();

            return data;
        }

        public int AddTask(Task task)
        {
            return 0;
        }

        public int UpdateTask(Task task)
        {
            return 0;
        }

        public int EndTask(int taskId)
        {
            return 0;
        }
    }
}
