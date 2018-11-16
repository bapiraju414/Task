using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Text;
using TaskManager.BusinessLayer;
using TaskManager.DataLayer;
using TaskManager.Entities;

namespace TaskManagerTest
{
    [TestFixture]
    public class TaskManagerBLTest
    {
        [Test]
        public void GetTaskTestBL()
        {
            Mock<TaskDBEntities> mockContext = MockSP();
            var taskManagerBL = new TaskBL(mockContext.Object);            
            List<TaskDetails> tasks = taskManagerBL.GetAll();
            Assert.IsNotNull(tasks);
            foreach (var task in tasks)
            {
                Assert.IsNotNull(task.TaskId);
                Assert.IsNotNull(task.Task_Name);
                Assert.IsNotNull(task.priority);
                Assert.IsNotNull(task.StartDate);
                Assert.IsNotNull(task.EndDate);
            }
        }

       
        [Test]
        public void GetParentTasksTest_WithoutID()
        {
            Mock<TaskDBEntities> mockContext = MockparentTask();
            var taskManagerBL = new TaskBL(mockContext.Object);
            List<ParentTask> tasks = taskManagerBL.GetParentTask();
            Assert.IsNotNull(tasks);
            Assert.IsTrue(tasks.Count > 0);
        }

        [Test]
        public void GetTaskByIdTest()
        {
            Mock<TaskDBEntities> mockContext = MockDataSetList();
            var taskManagerBL = new TaskBL(mockContext.Object);
            var task = taskManagerBL.Get(1);
            Assert.IsNotNull(task);
            Assert.AreEqual(1, task.Task_ID);
        }

        [Test]
        public void AddTaskTest()
        {
            Mock<TaskDBEntities> mockContext = MockDataSetList();
            var taskManagerBL = new TaskBL(mockContext.Object);
            Task model = new Task()
            {
                Task_ID = 1,
                TaskName = "Task 1",
                Parent_ID = 1,
                Priority = 10,
                Start_Date = DateTime.Now.Date,
                End_Date = DateTime.Now.AddDays(10)
            };
            int result = taskManagerBL.AddTask(model);

            Assert.IsTrue(result == 0);
        }

        [Test]
        public void UpdateTaskTest()
        {
            Mock<TaskDBEntities> mockContext = MockDataSetList();
            var taskManagerBL = new TaskBL(mockContext.Object);
            Task model = new Task()
            {
                Task_ID = 1,
                TaskName = "Task 1",
                Priority = 10,
                Parent_ID=1,
                Start_Date = DateTime.Now.Date,
                End_Date = DateTime.Now.AddDays(10)
            };
            int result = taskManagerBL.Update(model);

            Assert.IsTrue(result == 0);
        }

       

        private static Mock<TaskDBEntities> MockDataSetList()
        {
            var data = new List<Task>()
            {
               new Task()
                {
                    Task_ID = 1,
                    TaskName = "Task 1",                      
                       Priority =10,
                        Parent_ID=1,
                       Start_Date = DateTime.Now.Date,
                       End_Date = DateTime.Now.AddDays(10)                      
                },
               new Task()
                {
                    Task_ID = 2,
                    TaskName = "Task 2",
                       Priority =20,
                        Parent_ID=2,
                       Start_Date = DateTime.Now.Date,
                       End_Date = DateTime.Now.AddDays(10)
                },
                 new Task()
                {
                    Task_ID = 3,
                    TaskName = "Task 3",
                       Priority =10,
                        Parent_ID=3,
                       Start_Date = DateTime.Now.Date,
                       End_Date = DateTime.Now.AddDays(10)
                }
            }.AsQueryable();

            var mockset = new Mock<DbSet<Task>>();
            var mocksp = new Mock<ObjectResult<GetTaskDetails_Result>>();
            mockset.As<IQueryable<Task>>().Setup(m => m.Provider).Returns(data.Provider);
            mockset.As<IQueryable<Task>>().Setup(m => m.Expression).Returns(data.Expression);
            mockset.As<IQueryable<Task>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockset.As<IQueryable<Task>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());
            var mockContext = new Mock<TaskDBEntities>();
            mockContext.Setup(m => m.Tasks).Returns(mockset.Object);

            return mockContext;
        }

        private static Mock<TaskDBEntities> MockparentTask()
        {
            var data = new List<ParentTask>()
            {
               new ParentTask()
                {
                   Parent_ID =1,
                   Parent_Task="ygfugu"

                },
               new ParentTask()
                {
                    Parent_ID =1,
                   Parent_Task="ygfugu"
                },
                 new ParentTask()
                {
                    Parent_ID =1,
                   Parent_Task="ygfugu"
                }
            }.AsQueryable();

            var mockset = new Mock<DbSet<ParentTask>>();            
            mockset.As<IQueryable<ParentTask>>().Setup(m => m.Provider).Returns(data.Provider);
            mockset.As<IQueryable<ParentTask>>().Setup(m => m.Expression).Returns(data.Expression);
            mockset.As<IQueryable<ParentTask>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockset.As<IQueryable<ParentTask>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());
            var mockContext = new Mock<TaskDBEntities>();
            mockContext.Setup(m => m.ParentTasks).Returns(mockset.Object);

            return mockContext;
        }

        private static Mock<TaskDBEntities> MockSP()
        {
            var data = new List<GetTaskDetails_Result>()
            {
               new GetTaskDetails_Result()
                {
                    Task_ID = 1,
                    TaskName = "Task 1",
                       Priority =10,                       
                        Parent_Task="JJ",
                       Start_Date = DateTime.Now.Date,
                       End_Date = DateTime.Now.AddDays(10)
                       
                },
               new GetTaskDetails_Result()
                {
                    Task_ID = 1,
                    TaskName = "Task 1",
                       Priority =10,
                        Parent_Task="JJ",
                       Start_Date = DateTime.Now.Date,
                       End_Date = DateTime.Now.AddDays(10)
                       
                },
                 new GetTaskDetails_Result()
                {
                      Task_ID = 1,
                    TaskName = "Task 1",
                       Priority =10,
                        Parent_Task="JJ",
                       Start_Date = DateTime.Now.Date,
                       End_Date = DateTime.Now.AddDays(10)

                }
            }.AsQueryable();

            var mockset = new Mock<DbSet<TaskDetails>>();
            var mocksp = new Mock<ObjectResult<GetTaskDetails_Result>>();
            mocksp.As<IQueryable<GetTaskDetails_Result>>().Setup(m => m.Provider).Returns(data.Provider);
            mocksp.As<IQueryable<GetTaskDetails_Result>>().Setup(m => m.Expression).Returns(data.Expression);
            mocksp.As<IQueryable<GetTaskDetails_Result>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mocksp.As<IQueryable<GetTaskDetails_Result>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());
            var mockContext = new Mock<TaskDBEntities>();
            mockContext.Setup(m => m.GetTaskDetails()).Returns(mocksp.Object);

            return mockContext;
        }
    }
}
