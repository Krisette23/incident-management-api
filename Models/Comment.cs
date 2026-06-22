namespace incidentmanagement.Models
{
    public class Comment
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public DateTime CreatedAt { get; set; }
        public int IncidentId { get; set; }
        public int UserId { get; set; }
    }
}
