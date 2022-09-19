using FluentValidation;

namespace BlogApp.Application.VoteComments.Queries.GetCommentsVotes;

public class GetCommentsVotesQueryValidator : AbstractValidator<GetCommentsVotesQuery>
{
    public GetCommentsVotesQueryValidator()
    {
        RuleFor(getCommentsVotesQuery => getCommentsVotesQuery.CommentId).NotEqual(Guid.Empty);
    }
}
