using MediatR;

namespace BlogApp.Application.Posts.Commands.DetachTags;

public class DetachTagsCommand : IRequest
{
    public List<Guid> TagId { get; set; } = null!;
    public Guid Id { get; set; }
}
