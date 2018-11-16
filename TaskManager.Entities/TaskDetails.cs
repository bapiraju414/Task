using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManager.Entities
{
    public class TaskDetails
    {
        public int TaskId { get; set;}
        public string Task_Name { get; set;}

        public string Parent_Task { get; set;}

        public int priority { get; set;}

        public DateTime StartDate { get; set;}

        public DateTime EndDate { get; set;}
    }
}
