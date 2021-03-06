namespace BlogApp.Domain.Entites;

public class Comment
{
    public Guid Id { get; set; }
    public string Text { get; set; }
    public DateTime CreationTime { get; set; }
    public DateTime? LastEdited { get; set; }
    public Guid? ParentComment { get; set; }

    public Guid UserId { get; set; }
    public Post Post { get; set; }
}
