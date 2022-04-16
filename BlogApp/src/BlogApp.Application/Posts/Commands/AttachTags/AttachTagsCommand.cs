using MediatR;

namespace BlogApp.Application.Posts.Commands.AttachTags;

public class AttachTagsCommand : IRequest
{
    public List<Guid> TagId { get; set; }
    public Guid Id { get; set; }
}
