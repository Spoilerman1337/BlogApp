using MediatR;

namespace BlogApp.Application.Tags.Commands.UpdateTag;

public class UpdateTagCommand : IRequest
{
    public Guid Id { get; set; }
    public string TagName { get; set; }
}


