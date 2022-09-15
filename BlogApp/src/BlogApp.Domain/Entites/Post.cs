namespace BlogApp.Domain.Entites;

public class Post
{
    public Guid Id { get; set; }
    public string Header { get; set; }
    public string Text { get; set; }
    public DateTime CreationTime { get; set; }
    public DateTime? LastEdited { get; set; }

    public Guid UserId { get; set; }
    public ICollection<Comment> Comments { get; set; }
    public ICollection<Tag> Tags { get; set; }
}
