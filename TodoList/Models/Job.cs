// File: Models/HangHoa.cs
using System;

namespace MyWebApi.Models
{
    public class TodoVM
    {
        public string nameTodo { get; set; }

        public bool isCompleted { get; set; }
    }

    public class Todo : TodoVM
    {
        public Guid Id { get; set; }
    }
}
