namespace API.DTO
{
    public class TodoDto
    {
        public int Id { get; set; }
        public string Task { get; set; }
        public bool IsComplete { get; set; }
        public DateTime DueDate { get; set; }
    }
}