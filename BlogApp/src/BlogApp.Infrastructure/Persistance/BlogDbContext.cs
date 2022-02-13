﻿using BlogApp.Application.Common.Interfaces;
using BlogApp.Domain.Entites;
using BlogApp.Infrastructure.Persistance.Configuration;
using Microsoft.EntityFrameworkCore;

namespace BlogApp.Infrastructure.Persistance
{
    public class BlogDbContext : DbContext, IBlogDbContext
    {
        public BlogDbContext(DbContextOptions<BlogDbContext> options) : base(options) { }

        public DbSet<Post> Posts { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Tag> Tags { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new PostConfiguration());
            builder.ApplyConfiguration(new CommentConfiguration());
            builder.ApplyConfiguration(new TagConfiguration());

            base.OnModelCreating(builder);
        }
    }
}
