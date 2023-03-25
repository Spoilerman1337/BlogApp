namespace BlogApp.Domain.Entites;

public class Post
{
    public Guid Id { get; set; }
    public string Header { get; set; } = null!;
    public string Text { get; set; } = null!;
    public DateTime CreationTime { get; set; }
    public DateTime? LastEdited { get; set; }

    public Guid UserId { get; set; }
    public ICollection<Comment> Comments { get; set; } = null!;
    public ICollection<Tag> Tags { get; set; } = null!; 
    public ICollection<VotePost> Votes { get; set; } = null!; 
}
