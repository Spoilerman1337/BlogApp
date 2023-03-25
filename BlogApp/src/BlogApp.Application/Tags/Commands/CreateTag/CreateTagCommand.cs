using MediatR;

namespace BlogApp.Application.Tags.Commands.CreateTag;

public class CreateTagCommand : IRequest<Guid>
{
    public string TagName { get; set; } = null!;
}
