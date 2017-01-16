
using System;
using System.Linq;
using System.Net;
using Microsoft.AspNetCore.Mvc;
using TodoList.Context;

namespace TodoList.WebApi
{
    [Route("api/[controller]")]
    public class HomeController : Controller
    {
        private static int BADREQUEST = 1;
        private readonly TaskListContext _context;

        public HomeController(TaskListContext context)
        {
            _context = context;
        }

        // GET: api/values
        [HttpGet]
        public JsonResult Get()
        {
            var rand = new Random();
            var randNumber = rand.Next(1, 10);

            if (BADREQUEST == randNumber)
            {
                return Json(HttpStatusCode.BadRequest);
            }

            var list = _context.TaskTodos.ToList();

            return Json(list);
        }

        
    }
}
