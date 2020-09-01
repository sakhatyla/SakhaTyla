using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using Cynosura.EF;
using SakhaTyla.Core.Entities;
using SakhaTyla.Core.Enums;
using SakhaTyla.Core.Security;

namespace SakhaTyla.Data
{
    public class BaseEntityRepository<T> : EntityRepository<T> where T : BaseEntity
    {
        private readonly IUserInfoProvider _userInfoProvider;

        protected int? UserId => _userInfoProvider.GetUserInfoAsync().Result?.UserId;

        public BaseEntityRepository(IDatabaseFactory databaseFactory, IUserInfoProvider userInfoProvider)
            : base(databaseFactory)
        {
            _userInfoProvider = userInfoProvider;

            ((DataContext)Context).SavingChanges += OnSavingChanges;
        }

        private void OnSavingChanges(object sender, EventArgs eventArgs)
        {
            var entities = Context.ChangeTracker.Entries()
                .Where(e => e.State == EntityState.Modified || e.State == EntityState.Added || e.State == EntityState.Deleted)
                .Where(e => e.Entity is T)
                .Select(e => new
                {
                    Entity = (T)e.Entity,
                    State = e.State
                })
                .ToList();

            if (entities.Count > 0)
            {
                foreach (var entity in entities)
                {
                    if (entity.State == EntityState.Added)
                    {
                        entity.Entity.CreationDate = entity.Entity.ModificationDate = DateTime.UtcNow;
                        entity.Entity.CreationUserId = entity.Entity.ModificationUserId = UserId;
                        TrackChange(entity.Entity, ChangeAction.Add);
                    }
                    else if (entity.State == EntityState.Deleted)
                    {
                        TrackChange(entity.Entity, ChangeAction.Delete);
                    }
                    else if (entity.State == EntityState.Modified)
                    {
                        entity.Entity.ModificationDate = DateTime.UtcNow;
                        entity.Entity.ModificationUserId = UserId;
                        TrackChange(entity.Entity, ChangeAction.Update);
                    }
                }
            }
        }

        protected virtual void TrackChange(BaseEntity entity, ChangeAction action)
        {

        }
    }
}
