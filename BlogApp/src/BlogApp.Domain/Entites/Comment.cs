namespace BlogApp.Domain.Entites;

public class Comment
{
    public Guid Id { get; set; }
    public string Text { get; set; }
    public DateTime CreationTime { get; set; }
    public DateTime? LastEdited { get; set; }

    public virtual Guid UserId { get; set; }
    public Post Post { get; set; }
}
