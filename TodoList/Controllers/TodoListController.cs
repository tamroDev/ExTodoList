using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyWebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using TodoList.Data;

namespace TodoList.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoListController : ControllerBase
    {
        private readonly MyDBContext _context;

        public TodoListController(MyDBContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Get() 
        {
            var listTodo = _context.Jobs.ToList();
            return Ok(listTodo);
        }

        [HttpGet("{id}")]
        public IActionResult GetById (string id)
        {
            try
            {
                var toDo = _context.Jobs.SingleOrDefault(td => td.Id == Guid.Parse(id));
                if(toDo != null)
                {
                    return Ok(toDo);
                }

                return NotFound();
            } catch
            {
                return BadRequest();
            }
        }
        [HttpPost]
        public IActionResult CreateTodo(TodoVM model)
        {
            try
            {
                // Kiểm tra các thuộc tính có hợp lệ không
                if (model == null || string.IsNullOrWhiteSpace(model.nameTodo))
                {
                    return BadRequest(new { Error = "Invalid input data" });
                }

                var job = new Job
                {
                    Id = Guid.NewGuid(),  // Tạo Id mới cho Job
                    nameTodo = model.nameTodo,
                    isCompleted = model.isCompleted,
                };

                _context.Add(job);
                _context.SaveChanges();

                return Ok(new
                {
                    Success = true,
                    Data = job
                });
            }
            catch (Exception ex)
            {
                // Ghi log lỗi hoặc xử lý thông tin lỗi chi tiết
                return BadRequest(new { Error = ex.Message });
            }
        }

        [HttpPut("{id}")]
        public IActionResult UpdateTodo(string id, Todo todoEdit)
        {
            try
            {
                var todo = _context.Jobs.SingleOrDefault(td => td.Id == Guid.Parse(id));

                if (todo == null)
                {
                    return NotFound();
                }

                todo.nameTodo = todoEdit.nameTodo;
                todo.isCompleted = todoEdit.isCompleted;

                _context.Update(todo);
                _context.SaveChanges();

                return Ok();
            }
            catch
            {
                return BadRequest();
            }
        }


        [HttpDelete("{id}")]
        public IActionResult DeleteTodo(string id)
        {
            try
            {
                var todo = _context.Jobs.SingleOrDefault(td => td.Id == Guid.Parse(id));
                if (todo == null)
                {
                    return NotFound();
                }

                _context.Jobs.Remove(todo);
                _context.SaveChanges();

                return Ok();
            }
            catch
            {
                return BadRequest();
            }
        }

    }
}
