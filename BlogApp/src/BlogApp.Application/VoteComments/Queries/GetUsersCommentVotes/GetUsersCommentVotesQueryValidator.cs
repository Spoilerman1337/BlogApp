using FluentValidation;

namespace BlogApp.Application.VoteComments.Queries.GetUsersCommentVotes;

public class GetUsersCommentVotesQueryValidator : AbstractValidator<GetUsersCommentVotesQuery>
{
    public GetUsersCommentVotesQueryValidator()
    {
        RuleFor(getUsersCommentVotesQuery => getUsersCommentVotesQuery.UserId).NotEqual(Guid.Empty);
    }
}
