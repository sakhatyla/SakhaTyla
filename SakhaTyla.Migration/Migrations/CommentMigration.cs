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
using SakhaTyla.Core.Infrastructure;
using SakhaTyla.Data;
using SakhaTyla.Migration.Data;

namespace SakhaTyla.Migration.Migrations
{
    class CommentMigration
    {
        private readonly SourceLoader _sourceLoader;
        private readonly IMediator _mediator;
        private readonly IEntityRepository<User> _userRepository;
        private readonly IEntityRepository<Page> _pageRepository;
        private readonly DataContext _dataContext;

        public CommentMigration(SourceLoader sourceLoader, 
            IMediator mediator, 
            IEntityRepository<User> userRepository,
            IEntityRepository<Page> pageRepository,
            DataContext dataContext)
        {
            _sourceLoader = sourceLoader;
            _mediator = mediator;
            _userRepository = userRepository;
            _pageRepository = pageRepository;
            _dataContext = dataContext;
        }

        public async Task MigrateComments()
        {
            var comments = await _sourceLoader.GetCommentsAsync();
            var commentsByPage = comments.GroupBy(e => "blogs/" + e.BlogSynonym + "/" + e.PostSynonym);
            foreach (var pageComments in commentsByPage)
            {
                var page = await _pageRepository.GetEntities()
                    .FirstAsync(e => e.Type == Core.Enums.PageType.Article && e.Route.Path == pageComments.Key);
                var createOrUpdateComments = new CreateOrUpdateComments()
                {
                    ContainerId = page.CommentContainerId,
                    Comments = new List<CreateOrUpdateComment>(),
                };
                foreach (var comment in pageComments)
                {
                    var user = await _userRepository.GetEntities()
                        .FirstAsync(e => e.Email == comment.UserEmail);
                    
                    var createComment = new CreateOrUpdateComment()
                    {
                        TextSource = comment.TextSource,
                        Text = comment.Text,
                        AuthorId = user.Id,
                        CreationDate = comment.DateCreated.UtcDateTime,
                        ModificationDate = comment.DateModified.UtcDateTime,
                    };
                    createOrUpdateComments.Comments.Add(createComment);
                }
                await CreateCommentsAsync(createOrUpdateComments);
                await CountComments(createOrUpdateComments.ContainerId);
            }            
        }

        private async Task CreateCommentsAsync(CreateOrUpdateComments createOrUpdateComments)
        {
            var format = CommonHelper.MultiplyString("0", CommonHelper.GetDigitCount(createOrUpdateComments.Comments.Count));
            var i = createOrUpdateComments.Comments.Count;
            foreach (var comment in createOrUpdateComments.Comments.OrderBy(c => c.CreationDate))
            {
                var treeOrder = "/" + i.ToString(format);
                await CreateCommentAsync(createOrUpdateComments.ContainerId, "/", treeOrder, comment);
                i--;
            }
        }

        private Task CreateCommentAsync(int containerId, string treePath, string treeOrder, CreateOrUpdateComment comment)
        {
            var idEntity = _dataContext.Set<IdEntity>().FromSqlInterpolated($@"
insert into Comments
(
ContainerId,
AuthorId,
TextSource,
Text,
CreationDate,
ModificationDate,
TreePath,
TreeOrder
)
values
(
{containerId},
{comment.AuthorId},
{comment.TextSource},
{comment.Text},
{comment.CreationDate},
{comment.ModificationDate},
{treePath},
{treeOrder}
)

declare @commentId int = @@IDENTITY
select @commentId as Id
").AsEnumerable().FirstOrDefault();
            return Task.CompletedTask;
        }

        private async Task CountComments(int commentContainerId)
        {
            await _dataContext.Database.ExecuteSqlInterpolatedAsync($"update cc set CommentCount = (select count(*) from Comments c where c.ContainerId = cc.Id) from CommentContainers cc where cc.Id={commentContainerId}");
        }
    }

    public class CreateOrUpdateComments
    {
        public int ContainerId { get; set; }

        public List<CreateOrUpdateComment> Comments { get; set; } = null!;
    }

    public class CreateOrUpdateComment
    {
        public int? AuthorId { get; set; }

        public string TextSource { get; set; } = null!;

        public string Text { get; set; } = null!;

        public DateTime CreationDate { get; set; }

        public DateTime ModificationDate { get; set; }
    }
}
