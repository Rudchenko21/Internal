
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TodoList.Context;
using TodoList.Enum;
using TodoList.ViewModel;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace TodoList.WebApi
{
    [Route("api/[controller]")]
    public class HomeController : Controller
    {
        private readonly TaskListContext _context;

        public HomeController(TaskListContext context)
        {
            _context = context;
        }

        // GET: api/values
        [HttpGet]
        public JsonResult Get()
        {
            TaskTodoListViewModel viewModel = new TaskTodoListViewModel
            {
                LisTaskTodosStatusDone = _context.TaskTodos.Where(m => m.Status == Status.Done).ToList(),
                LisTaskTodosStatusInprogress = _context.TaskTodos.Where(m => m.Status == Status.InProgress).ToList(),
                LisTaskTodosStatusPlanned = _context.TaskTodos.Where(m => m.Status == Status.Planned).ToList()
            };

            return Json(viewModel);
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
