using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SakhaTyla.Migration.SourceDatabase;
using SakhaTyla.Core.Requests.Comments;
using Cynosura.Core.Data;
using SakhaTyla.Core.Entities;

namespace SakhaTyla.Migration.Migrations
{
    class CommentMigration
    {
        private readonly SourceLoader _sourceLoader;
        private readonly IMediator _mediator;
        private readonly IEntityRepository<User> _userRepository;
        private readonly IEntityRepository<Page> _pageRepository;

        public CommentMigration(SourceLoader sourceLoader, 
            IMediator mediator, 
            IEntityRepository<User> userRepository,
            IEntityRepository<Page> pageRepository)
        {
            _sourceLoader = sourceLoader;
            _mediator = mediator;
            _userRepository = userRepository;
            _pageRepository = pageRepository;
        }

        public async Task MigrateComments()
        {
            var comments = await _sourceLoader.GetCommentsAsync();
            foreach (var comment in comments)
            {
                // TODO: set creation date and modification date
                var user = await _userRepository.GetEntities()
                    .FirstAsync(e => e.Email == comment.UserEmail);
                var path = "blogs/" + comment.BlogSynonym + "/" + comment.PostSynonym;
                var page = await _pageRepository.GetEntities()
                    .FirstAsync(e => e.Type == Core.Enums.PageType.Article && e.Route.Path == path);
                var createComment = new CreateComment()
                {
                    TextSource = comment.TextSource,
                    AuthorId = user.Id,
                    ContainerId = page.CommentContainerId,
                };
                await _mediator.Send(createComment);
            }
        }
    }
}
