using System.Linq;
using Microsoft.AspNetCore.Mvc;
using TodoList.Context;
using TodoList.Enum;
using TodoList.ViewModel;
using TODOList.Entities;

namespace TodoList.Controllers
{
    public class HomeController : Controller
    {
        private TaskListContext _context;

        public HomeController(TaskListContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            TaskTodoListViewModel viewModel = new TaskTodoListViewModel
            {
                LisTaskTodosStatusDone = _context.TaskTodos.Where(m => m.Status==Status.Done).ToList(),
                LisTaskTodosStatusInprogress = _context.TaskTodos.Where(m => m.Status== Status.InProgress).ToList(),
                LisTaskTodosStatusPlanned = _context.TaskTodos.Where(m => m.Status== Status.Planned).ToList()
            };

            return View(viewModel);
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
