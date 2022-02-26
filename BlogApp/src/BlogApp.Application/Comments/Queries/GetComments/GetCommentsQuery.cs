using BlogApp.Application.Comments.Queries.GetComments.Models;
using MediatR;

namespace BlogApp.Application.Comments.Queries.GetComments;

public class GetCommentsQuery : IRequest<List<GetCommentsDto>>
{ 
    public Guid UserId { get; set; }
}
