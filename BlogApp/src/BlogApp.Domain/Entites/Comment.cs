namespace BlogApp.Domain.Entites
{
    public class Comment
    {
        public Guid Id { get; set; }
        public string Text { get; set; }
        public DateTime CreationTime { get; set; }
        public DateTime LastEdited { get; set; }
    }
}
