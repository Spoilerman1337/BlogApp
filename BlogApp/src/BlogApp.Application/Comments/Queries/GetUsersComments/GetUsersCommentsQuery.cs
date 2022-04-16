using BlogApp.Application.Comments.Queries.GetUsersComments.Models;
using MediatR;

namespace BlogApp.Application.Comments.Queries.GetUsersComments;

public class GetUsersCommentsQuery : IRequest<List<GetUsersCommentsDto>>
{
    public Guid UserId { get; set; }
}
