using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TODOList.Entities;

namespace TodoList.ViewModel
{
    public class TaskTodoListViewModel
    {
        public IList<TaskTodo> LisTaskTodosStatusPlanned { get; set; }

        public IList<TaskTodo> LisTaskTodosStatusInprogress { get; set; }

        public IList<TaskTodo> LisTaskTodosStatusDone { get; set; }
    }
}
