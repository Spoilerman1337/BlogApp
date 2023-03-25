namespace BlogApp.Domain.Entites;

public class VotePost
{
    public Guid UserId { get; set; }
    public bool IsUpvoted { get; set; }
    public Guid PostId { get; set; }

    public Post Post { get; set; } = null!;
}
