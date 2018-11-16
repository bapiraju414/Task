using System;
using System.Collections.Generic;
using System.Linq;
using TaskManager.DataLayer;
using TaskManager.Entities;


namespace TaskManager.BusinessLayer
{
    public class TaskBL
    {
      //  TaskDBEntities tb = new TaskDBEntities();
        private readonly TaskDBEntities tb;
        public TaskBL()
        {
            tb = new TaskDBEntities();
        }

        public TaskBL(TaskDBEntities taskManager)
        {
            tb = taskManager;
        }

        public partial class TaskSet
        {
            public int Task_ID { get; set; }
            public Nullable<int> Parent_ID { get; set; }
            public string TaskName { get; set; }
            public Nullable<System.DateTime> Start_Date { get; set; }
            public Nullable<System.DateTime> End_Date { get; set; }
            public Nullable<int> Priority { get; set; }
        }

        public int AddTask(Task item)
        {
            int result = -1;
            tb.Tasks.Add(item);
            result= tb.SaveChanges();
            return result;
        }

        public List<TaskDetails> GetAll()
        {
            List<TaskDetails> tdlist = new List<TaskDetails>();
            var taskDetails= tb.GetTaskDetails().ToList();
            foreach (var item in taskDetails)
            {
                tdlist.Add( new TaskDetails
                {
                    TaskId = item.Task_ID,
                    Task_Name = item.TaskName,
                    Parent_Task = item.Parent_Task,
                    StartDate = (DateTime) item.Start_Date,
                    EndDate = (DateTime) item.End_Date,
                    priority = (int)item.Priority
                });

                
            }
            return tdlist;
           
        }

        public List<ParentTask> GetParentTask()
        {
            return tb.ParentTasks.ToList();
        }


        public TaskSet Get(int Tid)
        {

            //DataLayer.Task task = tb.Tasks.Find(Tid);
            //return task;

            TaskSet task = new TaskSet();
            task = tb.Tasks
                .Where(x => x.Task_ID == Tid)
                .Select(t => new TaskSet()
                {
                    Task_ID = t.Task_ID,
                    TaskName = t.TaskName,                   
                    Priority = t.Priority,
                    Parent_ID =t.Parent_ID,
                    Start_Date = t.Start_Date,
                    End_Date = t.End_Date                 
                }).FirstOrDefault();

            return task;
        }

        public int Update(Task task)
        {         

            int result = -1;
            var tTask = tb.Tasks.Where(t => t.Task_ID == task.Task_ID).FirstOrDefault();
            if (tTask != null)            {
                tTask.TaskName = task.TaskName;
                task.Parent_ID = task.Parent_ID;
                tTask.Priority = task.Priority;
                tTask.Start_Date = task.Start_Date;
                tTask.End_Date = task.End_Date;
                result = tb.SaveChanges();
            }
            return result;
        }

    }
}
