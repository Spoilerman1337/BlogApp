using FluentValidation;

namespace BlogApp.Application.VotePosts.Queries.GetPostsVotes;

public class GetPostsVotesQueryValidator : AbstractValidator<GetPostsVotesQuery>
{
    public GetPostsVotesQueryValidator()
    {
        RuleFor(getPostsVotesQuery => getPostsVotesQuery.PostId).NotEqual(Guid.Empty);
    }
}
