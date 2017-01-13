using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TODOList.Entities;

namespace TodoList.Context
{
    public class TaskListContext : DbContext
    {
        public DbSet<TaskTodo> TaskTodos { get; set; }

        public TaskListContext(DbContextOptions<TaskListContext> options)
            : base(options)
        {
        }
    }
}
