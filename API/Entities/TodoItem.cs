using System;
namespace API.Entities
{
    public class TodoItem
    {
        public int Id { get; set; }
        public string Task { get; set; }
        public bool IsComplete { get; set; }
        public DateTime DueDate { get; set; }
        public string Username { get; set; }
    }
}