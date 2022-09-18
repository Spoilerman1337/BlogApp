using FluentValidation;

namespace BlogApp.Application.VotePosts.Queries.GetUsersPostVotes;

public class GetUsersPostVotesQueryValidator : AbstractValidator<GetUsersPostVotesQuery>
{
    public GetUsersPostVotesQueryValidator()
    {
        RuleFor(getUsersPostVotesQuery => getUsersPostVotesQuery.UserId).NotEqual(Guid.Empty);
    }
}
