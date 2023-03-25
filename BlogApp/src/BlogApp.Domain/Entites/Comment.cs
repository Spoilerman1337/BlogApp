namespace BlogApp.Domain.Entites;

public class Comment
{
    public Guid Id { get; set; }
    public string Text { get; set; } = null!; 
    public DateTime CreationTime { get; set; }
    public DateTime? LastEdited { get; set; }

    public Comment? ParentComment { get; set; }
    public Guid UserId { get; set; }
    public Post Post { get; set; } = null!;
    public ICollection<VoteComment> Votes { get; set; } = null!;
}
