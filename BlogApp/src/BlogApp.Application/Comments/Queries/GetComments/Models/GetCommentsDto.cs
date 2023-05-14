using BlogApp.Domain.Entites;
using Mapster;

namespace BlogApp.Application.Comments.Queries.GetComments.Models;

public class GetCommentsDto : IMapFrom<Comment>
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public Guid PostId { get; set; }
    public string Text { get; set; } = null!;
    public DateTime CreationTime { get; set; }
    public DateTime? LastEdited { get; set; }
    public Guid? ParentCommentId { get; set; }

    public void ConfigureMapping(TypeAdapterConfig config)
    {
        config.NewConfig<Comment, GetCommentsDto>();
    }
}
