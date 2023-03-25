namespace BlogApp.Domain.Entites;

public class VoteComment
{
    public Guid UserId { get; set; }
    public bool IsUpvoted { get; set; }
    public Guid CommentId { get; set; }

    public Comment Comment { get; set; } = null!;
}
