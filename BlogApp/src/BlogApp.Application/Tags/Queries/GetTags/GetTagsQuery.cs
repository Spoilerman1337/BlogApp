using BlogApp.Application.Tags.Queries.GetTags.Models;
using MediatR;

namespace BlogApp.Application.Tags.Queries.GetTags;

//We don't need to pass here anything, only need this class for IRequest interface
public class GetTagsQuery : IRequest<List<GetTagsDto>> { }
