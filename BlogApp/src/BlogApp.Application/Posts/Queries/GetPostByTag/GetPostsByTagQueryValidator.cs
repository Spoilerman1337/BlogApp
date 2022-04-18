﻿using FluentValidation;

namespace BlogApp.Application.Posts.Queries.GetPostByTag;

public class GetPostsByTagQueryValidator : AbstractValidator<GetPostsByTagQuery>
{
    public GetPostsByTagQueryValidator()
    {
        RuleFor(getPostQuery => getPostQuery.TagId).NotEqual(Guid.Empty);
    }
}
