using MediatR;

namespace BlogApp.Application.Posts.Commands.DetachTags;

public class DetachTagsCommand : IRequest
{
    public List<Guid> TagId { get; set; }
    public Guid Id { get; set; }
}
