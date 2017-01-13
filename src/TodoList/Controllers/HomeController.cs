using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Polly;
using TodoList.Context;
using TodoList.Enum;
using TodoList.ViewModel;
using TODOList.Entities;

namespace TodoList.Controllers
{
    public class HomeController : Controller
    {
        private TaskListContext _context;
        private readonly List<string> _apiList;
        public HomeController(TaskListContext context)
        {
            _context = context;
            _apiList = new List<string>
            {
                "http://10.23.22.111/cardsboard/api/cards" ,
                "http://10.23.22.119/core/api/CardApi",
                "http://10.23.22.118/oleksii_budianskyi/todomanager/api/tasks"
            };
        }

        public async Task<IActionResult> Index()
        {
            TaskTodoListViewModel viewModel = new TaskTodoListViewModel
            {
                LisTaskTodosStatusDone = _context.TaskTodos.Where(m => m.Status==Status.Done).ToList(),
                LisTaskTodosStatusInprogress = _context.TaskTodos.Where(m => m.Status== Status.InProgress).ToList(),
                LisTaskTodosStatusPlanned = _context.TaskTodos.Where(m => m.Status== Status.Planned).ToList()
            };

            return View(viewModel);
        }

        private async Task Method(TaskTodoListViewModel viewModel)
        {
            foreach (var link in _apiList)
            {
                using (var httpclient = new HttpClient())
                {
                    var respond = await httpclient.GetStringAsync(link);
                    var todoList = JsonConvert.DeserializeObject<TaskTodo[]>(respond);

                    viewModel.LisTaskTodosStatusDone =
                        viewModel.LisTaskTodosStatusDone.Concat(todoList.Where(m => m.Status == Status.Done))
                            .ToList();

                    viewModel.LisTaskTodosStatusInprogress =
                        viewModel.LisTaskTodosStatusInprogress.Concat(
                                todoList.Where(m => m.Status == Status.InProgress))
                            .ToList();

                    viewModel.LisTaskTodosStatusPlanned =
                        viewModel.LisTaskTodosStatusPlanned.Concat(
                                todoList.Where(m => m.Status == Status.Planned))
                            .ToList();
                }
            }
        }

        [HttpPost]
        public IActionResult Update(int id)
        {
            var task = _context.TaskTodos.Where(m => m.Id.Equals(id)).First();
            switch (task.Status)
            {
                case Status.Planned:
                    task.Status= Status.InProgress;
                    break;

                case Status.InProgress:
                    task.Status= Status.Done;
                    break;

                default:
                    break;
            }

            _context.TaskTodos.Update(task);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Add(TaskTodo entity)
        {
            if (entity != null)
            {
                entity.Status = Status.Planned;
                _context.Add(entity);
                _context.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            var task = _context.TaskTodos.Where(m => m.Id.Equals(id)).First();
            _context.TaskTodos.Remove(task);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}
