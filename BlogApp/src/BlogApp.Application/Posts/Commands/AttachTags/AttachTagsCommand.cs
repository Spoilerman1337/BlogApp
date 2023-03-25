using MediatR;

namespace BlogApp.Application.Posts.Commands.AttachTags;

public class AttachTagsCommand : IRequest
{
    public List<Guid> TagId { get; set; } = null!;
    public Guid Id { get; set; }
}
