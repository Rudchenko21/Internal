using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoList.Enum;

namespace TODOList.Entities
{
    public class TaskTodo
    {
        public int Id { get; set; }

        public string Text { get; set; }

        public Status Status { get; set; }
    }
}
