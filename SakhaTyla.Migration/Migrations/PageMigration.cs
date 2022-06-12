using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using SakhaTyla.Core.Requests.Routes;
using SakhaTyla.Core.Requests.Pages;
using SakhaTyla.Migration.SourceDatabase;

namespace SakhaTyla.Migration.Migrations
{
    class PageMigration
    {
        private readonly SourceLoader _sourceLoader;
        private readonly IMediator _mediator;

        private readonly Dictionary<int, int> _pageIdMap = new();

        public PageMigration(SourceLoader sourceLoader, IMediator mediator)
        {
            _sourceLoader = sourceLoader;
            _mediator = mediator;
        }

        public async Task MigratePageData()
        {
            await MigratePages();
            await MigrateBlogs();
            await MigratePosts();
        }

        private async Task MigratePages()
        {
            var pages = await _sourceLoader.GetPagesAsync(false);
            foreach (var page in pages)
            {
                var createPage = new CreatePage()
                {
                    Type = Core.Enums.PageType.General,
                    Name = page.Title,
                    Route = new UpdateRoute()
                    {
                        Path = "pages/" + page.Synonym + (page.Lang != null ? "/" + page.Lang : ""),
                    },
                    Body = page.Contents,
                };
                await _mediator.Send(createPage);
            }
        }

        private async Task MigrateBlogs()
        {
            var blogs = await _sourceLoader.GetBlogsAsync();
            foreach (var blog in blogs)
            {
                var createPage = new CreatePage()
                {
                    Type = Core.Enums.PageType.Blog,
                    Name = blog.Title,
                    Route = new UpdateRoute()
                    {
                        Path = "blogs/" + blog.Synonym,
                    }
                };
                var createdPage = await _mediator.Send(createPage);
                _pageIdMap[blog.Id] = createdPage.Id;
            }
        }

        private async Task MigratePosts()
        {
            var posts = await _sourceLoader.GetPostsAsync();
            foreach (var post in posts)
            {
                var createPage = new CreatePage()
                {
                    Type = Core.Enums.PageType.Article,
                    Name = post.Title,
                    Route = new UpdateRoute()
                    {
                        Path = "blogs/" + post.BlogSynonym + "/" + post.Synonym,
                    },
                    Body = post.Contents,
                    ParentId = _pageIdMap[post.BlogId],
                    Preview = post.Preview,
                    PublicationDate = post.DatePublished.UtcDateTime,
                };
                await _mediator.Send(createPage);
            }
        }
    }
}
