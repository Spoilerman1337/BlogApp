using BlogApp.Domain.Entites;
using BlogApp.Infrastructure.Persistance;
using Microsoft.EntityFrameworkCore;

namespace BlogApp.Application.UnitTests.Common;

public class BlogAppContextFactory
{
    protected internal static Guid UserAId = Guid.NewGuid();
    protected internal static Guid UserBId = Guid.NewGuid();
    protected internal static Guid NoPostUser = Guid.NewGuid();

    protected internal static Guid ToBeUpdatedCommentId = Guid.NewGuid();
    protected internal static Guid ToBeDeletedCommentId = Guid.NewGuid();

    protected internal static Guid ToBeUpdatedPostId = Guid.NewGuid();
    protected internal static Guid ToBeDeletedPostId = Guid.NewGuid();

    protected internal static Guid ToBeUpdatedTagId = Guid.NewGuid();
    protected internal static Guid ToBeDeletedTagId = Guid.NewGuid();

    public static BlogDbContext Create()
    {
        var options = new DbContextOptionsBuilder<BlogDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

        var context = new BlogDbContext(options);

        var tags = new List<Tag>
        {
            new Tag
            {
                Id = Guid.Parse("3E07C1D3-01B8-4CC1-B32D-0E5813A0D2FF"),
                TagName = "Nullam",
                Posts = new List<Post>()
            },
            new Tag
            {
                Id = Guid.Parse("79121046-24BD-4518-813F-79878A48AC73"),
                TagName = "Mauris",
                Posts = new List<Post>()
            },
            new Tag
            {
                Id = ToBeUpdatedTagId,
                TagName = "Aliquam",
                Posts = new List<Post>()
            },
            new Tag
            {
                Id = ToBeDeletedTagId,
                TagName = "Nam",
                Posts = new List<Post>()
            }
        };

        var posts = new List<Post>
        {
            new Post
            {
                Id = Guid.Parse("2A9C5C84-032D-49D6-B43B-D4028679B8D9"),
                UserId = UserAId,
                Header = "Nulla a nisi.",
                Text = "Ut dui dui, malesuada at placerat ut, cursus vel enim. Lorem ipsum dolor.",
                CreationTime = DateTime.Now.AddYears(-2),
                Comments = new List<Comment>(),
                Tags = new List<Tag>()
            },
            new Post
            {
                Id = Guid.Parse("D30526A7-E44C-4163-B8A7-E0452C7E12FA"),
                UserId = UserBId,
                Header = "Ut in sapien.",
                Text = "Nunc quis nulla condimentum massa pharetra condimentum. Fusce vestibulum mi eros, ut euismod.",
                CreationTime = DateTime.Now.AddDays(1),
                Comments = new List<Comment>(),
                Tags = new List<Tag>()
            },
            new Post
            {
                Id = ToBeUpdatedPostId,
                UserId = UserAId,
                Header = "Vivamus lobortis augue.",
                Text = "Vestibulum ut efficitur nibh. Cras massa odio, sodales at ullamcorper sit amet, sagittis.",
                CreationTime = DateTime.Now.AddMonths(-1),
                Comments = new List<Comment>(),
                Tags = new List<Tag>()
            },
            new Post
            {
                Id = ToBeDeletedPostId,
                UserId = UserBId,
                Header = "Cras dignissim euismod.",
                Text = "Curabitur nibh nulla, pharetra sit amet finibus eget, egestas ac ex. Vivamus quis.",
                CreationTime = DateTime.Now,
                Comments = new List<Comment>(),
                Tags = new List<Tag>()
            }
        };

        var comments = new List<Comment>
        {
            new Comment
            {
                Id = Guid.Parse("8BAB8E58-5FB2-4E92-AE7F-643DF6D3D2A6"),
                UserId = UserAId,
                CreationTime = DateTime.Now.AddDays(-1),
                Text = "Lorem ipsum dolor sit amet",
            },
            new Comment
            {
                Id = Guid.Parse("0401EAD8-CB5C-4BCF-8ABF-7F63FCCF3155"),
                UserId = UserBId,
                CreationTime = DateTime.Now.AddYears(1),
                Text = "Pellentesque consectetur libero in diam convallis, ut blandit est placerat. Donec fringilla turpis.",
            },
            new Comment
            {
                Id = ToBeUpdatedCommentId,
                UserId = UserBId,
                CreationTime = DateTime.Now,
                Text = "Suspendisse bibendum dolor sapien.",
            },
            new Comment
            {
                Id = ToBeDeletedCommentId,
                UserId = UserBId,
                CreationTime = DateTime.Now,
                Text = "Proin nec erat.",
            }
        };

        var votePosts = new List<VotePost>
        {
            new VotePost
            {
                PostId = Guid.Parse("2A9C5C84-032D-49D6-B43B-D4028679B8D9"),
                UserId = UserBId,
                IsUpvoted = true
            },
            new VotePost
            {
                PostId = Guid.Parse("2A9C5C84-032D-49D6-B43B-D4028679B8D9"),
                UserId = NoPostUser,
                IsUpvoted = true
            },
            new VotePost
            {
                PostId = ToBeUpdatedPostId,
                UserId = NoPostUser,
                IsUpvoted = false
            },
            new VotePost
            {
                PostId = ToBeUpdatedPostId,
                UserId = UserBId,
                IsUpvoted = false
            }
        };

        var voteComments = new List<VoteComment>
        {
            new VoteComment
            {
                CommentId = Guid.Parse("8BAB8E58-5FB2-4E92-AE7F-643DF6D3D2A6"),
                UserId = UserBId,
                IsUpvoted = true
            },
            new VoteComment
            {
                CommentId = Guid.Parse("8BAB8E58-5FB2-4E92-AE7F-643DF6D3D2A6"),
                UserId = NoPostUser,
                IsUpvoted = true
            },
            new VoteComment
            {
                CommentId = ToBeUpdatedCommentId,
                UserId = NoPostUser,
                IsUpvoted = false
            },
            new VoteComment
            {
                CommentId = ToBeUpdatedCommentId,
                UserId = UserBId,
                IsUpvoted = false
            }
        };

        (tags.Where(t => t.Id == Guid.Parse("3E07C1D3-01B8-4CC1-B32D-0E5813A0D2FF"))
            .First().Posts
            as List<Post>)!
            .AddRange(posts
                .Where(p => p.Id == ToBeUpdatedPostId || 
                            p.Id == ToBeDeletedPostId));

        tags.Where(t => t.Id == Guid.Parse("79121046-24BD-4518-813F-79878A48AC73"))
            .First().Posts
            .Add(posts
                .Where(p => p.Id == ToBeDeletedPostId)
                .First());

        tags.Where(t => t.Id == Guid.Parse("79121046-24BD-4518-813F-79878A48AC73"))
            .First().Posts
            .Add(posts
                .Where(p => p.Id == Guid.Parse("2A9C5C84-032D-49D6-B43B-D4028679B8D9"))
                .First());

        posts.Where(p => p.Id == Guid.Parse("2A9C5C84-032D-49D6-B43B-D4028679B8D9"))
            .First().Tags
            .Add(tags
                .Where(t => t.Id == Guid.Parse("79121046-24BD-4518-813F-79878A48AC73"))
                .First());

        (posts.Where(p => p.Id == Guid.Parse("D30526A7-E44C-4163-B8A7-E0452C7E12FA"))
            .First().Comments as List<Comment>)
            .AddRange(comments
                .Where(c => c.Id == Guid.Parse("8BAB8E58-5FB2-4E92-AE7F-643DF6D3D2A6") || 
                            c.Id == Guid.Parse("0401EAD8-CB5C-4BCF-8ABF-7F63FCCF3155") ||
                            c.Id == ToBeDeletedCommentId || 
                            c.Id == ToBeUpdatedCommentId));

        posts.Where(p => p.Id == ToBeUpdatedPostId)
            .First().Tags
            .Add(tags
                .Where(t => t.Id == Guid.Parse("3E07C1D3-01B8-4CC1-B32D-0E5813A0D2FF"))
                .First());

        (posts.Where(p => p.Id == ToBeDeletedPostId)
            .First().Tags
            as List<Tag>)
            .AddRange(tags
                .Where(t => t.Id == Guid.Parse("3E07C1D3-01B8-4CC1-B32D-0E5813A0D2FF") || 
                            t.Id == Guid.Parse("79121046-24BD-4518-813F-79878A48AC73")));

        comments.Where(c => c.Id == Guid.Parse("8BAB8E58-5FB2-4E92-AE7F-643DF6D3D2A6"))
            .First().Post = posts.Where(p => p.Id == Guid.Parse("D30526A7-E44C-4163-B8A7-E0452C7E12FA")).First();

        comments.Where(c => c.Id == Guid.Parse("0401EAD8-CB5C-4BCF-8ABF-7F63FCCF3155"))
            .First().Post = posts.Where(p => p.Id == Guid.Parse("D30526A7-E44C-4163-B8A7-E0452C7E12FA")).First();

        comments.Where(c => c.Id == Guid.Parse("0401EAD8-CB5C-4BCF-8ABF-7F63FCCF3155"))
            .First().ParentComment = comments.Where(p => p.Id == Guid.Parse("8BAB8E58-5FB2-4E92-AE7F-643DF6D3D2A6")).First();

        comments.Where(c => c.Id == ToBeUpdatedCommentId)
            .First().Post = posts.Where(p => p.Id == Guid.Parse("D30526A7-E44C-4163-B8A7-E0452C7E12FA")).First();

        comments.Where(c => c.Id == ToBeDeletedCommentId)
            .First().Post = posts.Where(p => p.Id == Guid.Parse("D30526A7-E44C-4163-B8A7-E0452C7E12FA")).First();

        votePosts.Where(vp => vp.PostId == Guid.Parse("2A9C5C84-032D-49D6-B43B-D4028679B8D9"))
            .First().Post = posts.Where(p => p.Id == Guid.Parse("2A9C5C84-032D-49D6-B43B-D4028679B8D9")).First();

        votePosts.Where(vp => vp.PostId == ToBeUpdatedPostId)
            .First().Post = posts.Where(p => p.Id == ToBeUpdatedPostId).First();

        voteComments.Where(vc => vc.CommentId == Guid.Parse("8BAB8E58-5FB2-4E92-AE7F-643DF6D3D2A6"))
            .First().Comment = comments.Where(p => p.Id == Guid.Parse("8BAB8E58-5FB2-4E92-AE7F-643DF6D3D2A6")).First();

        voteComments.Where(vc => vc.CommentId == ToBeUpdatedCommentId)
            .First().Comment = comments.Where(c => c.Id == ToBeUpdatedCommentId).First();

        context.Database.EnsureCreated();
        context.Tags.AddRange(tags);
        context.Posts.AddRange(posts);
        context.Comments.AddRange(comments);
        context.VotePosts.AddRange(votePosts);
        context.VoteComments.AddRange(voteComments);
        context.SaveChanges();
        return context;
    }

    public static void Destroy(BlogDbContext context)
    {
        context.Database.EnsureDeleted();
        context.Dispose();
    }
}
