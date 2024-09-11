using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TodoList.Data
{
    [Table("Jobs")]
    public class Job
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        public string nameTodo { get; set; }
        public bool isCompleted { get; set; } = false;
    }
}
