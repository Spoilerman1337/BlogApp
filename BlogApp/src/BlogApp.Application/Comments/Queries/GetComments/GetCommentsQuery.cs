using BlogApp.Application.Comments.Queries.GetComments.Models;
using MediatR;

namespace BlogApp.Application.Comments.Queries.GetComments;

//We don't need to pass here anything, only need this class for IRequest interface
public class GetCommentsQuery : IRequest<List<GetCommentsDto>> { }
