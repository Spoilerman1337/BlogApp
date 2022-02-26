namespace BlogApp.Application.Comments.Queries.GetComments.Models;

public class GetCommentsDto
{
    public Guid Id { get; set; }
    public string Text { get; set; }
    public DateTime CreationTime { get; set; }
    public DateTime? LastEdited { get; set; }
}
