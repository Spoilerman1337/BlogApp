namespace BlogApp.Domain.Entites;

public class Tag
{
    public Guid Id { get; set; }
    public string TagName { get; set; }

    public ICollection<Post> Posts { get; set; }
}
