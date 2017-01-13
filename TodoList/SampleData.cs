using System;
using System.Linq;
using TodoList.Context;
using Microsoft.Extensions.DependencyInjection;
using TodoList.Enum;
using TODOList.Entities;

namespace TodoList
{
    public static class SampleData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            var context = serviceProvider.GetService<TaskListContext>();

            if (!context.TaskTodos.Any())
            {
                context.TaskTodos.AddRange(
                    new TaskTodo
                    {
                        Text = "First to do task",
                        Status = Status.Done
                    },
                    new TaskTodo
                    {
                        Text = "Second to do task",
                        Status = Status.Planned
                    },
                    new TaskTodo
                    {
                        Text = "Third to do task",
                        Status = Status.InProgress
                    }
                );
                context.SaveChanges();
            }
        }
    }
}
