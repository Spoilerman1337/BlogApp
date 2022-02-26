using BlogApp.Application.Common.Interfaces;
using BlogApp.Domain.Entites;

namespace BlogApp.Application.Comments.Queries.GetComment.Models;

public class GetCommentDto : IMapFrom<Comment>
{
    public Guid Id { get; set; }
    public string Text { get; set; }
    public DateTime CreationTime { get; set; }
    public DateTime? LastEdited { get; set; }
}
