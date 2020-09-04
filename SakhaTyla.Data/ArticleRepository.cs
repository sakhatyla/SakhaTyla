using System;
using System.Collections.Generic;
using System.Text;
using Cynosura.EF;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using SakhaTyla.Core.Entities;
using SakhaTyla.Core.Enums;
using SakhaTyla.Core.Security;

namespace SakhaTyla.Data
{
    public class ArticleRepository : TrackedEntityRepository<Article>
    {
        public ArticleRepository(IDatabaseFactory databaseFactory, IUserInfoProvider userInfoProvider)
            : base(databaseFactory, userInfoProvider)
        {
        }

        protected override void TrackChange(EntityEntry entityEntry, ChangeAction action)
        {
            if (action == ChangeAction.Update)
            {
                var article = (Article)entityEntry.Entity;
                if (article.IsDeleted)
                {
                    base.TrackChange(entityEntry, ChangeAction.Delete);
                    return;
                }
            }
            base.TrackChange(entityEntry, action);
        }
    }
}
