namespace BlogApp.Domain.Entites;

public class Tag
{
    public Guid Id { get; set; }
    public string TagName { get; set; } = null!;

    public ICollection<Post> Posts { get; set; } = null!;
}
