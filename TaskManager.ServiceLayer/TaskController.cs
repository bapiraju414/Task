using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TaskManager.BusinessLayer;
using TaskManager.DataLayer;
using TaskManager.Entities;
using static TaskManager.BusinessLayer.TaskBL;

namespace TaskManager.ServiceLayer
{
    [System.Web.Http.Cors.EnableCors(origins: "*", headers: "*", methods: "*")]
    public class TaskController : ApiController
    {
        // GET api/<controller>
        TaskBL bl = new TaskBL();
        [Route("api/GetTaskColection")]
        public IEnumerable<TaskDetails> Get()
        {
            return bl.GetAll();
        }

        [Route("api/GetParentTask")]
        public IEnumerable<ParentTask> GetParentTask()
        {
            return bl.GetParentTask();
        }

        [Route("api/GetTaskyByID/{id:int}")]
        public TaskSet Get(int id)
        {
            return bl.Get(id);
        }

        // POST api/<controller>
        [Route("api/AddTask")]
        public IHttpActionResult Post([FromBody]Task item)
        {
            if (!ModelState.IsValid)
                return BadRequest("Invalid data.");
            bl.AddTask(item);
            return Ok();
        }

        [Route("api/UpdateTask")]
        public void Put([FromBody]Task item)
        {
            bl.Update(item);
        }


        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }
    }
}