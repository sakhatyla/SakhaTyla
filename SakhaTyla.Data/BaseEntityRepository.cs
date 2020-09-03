using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
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
            ((DataContext)Context).SavedChanges += OnSavedChanges;
        }

        private void OnSavingChanges(object sender, DataContext.SaveEventArgs e)
        {
            var entityEntries = Context.ChangeTracker.Entries()
                .Where(e => e.State == EntityState.Modified || e.State == EntityState.Added || e.State == EntityState.Deleted)
                .Where(e => e.Entity is T)
                .ToList();

            if (entityEntries.Count > 0)
            {
                foreach (var entityEntry in entityEntries)
                {
                    var entity = (T)entityEntry.Entity;
                    if (entityEntry.State == EntityState.Added)
                    {
                        entity.CreationDate = entity.ModificationDate = DateTime.UtcNow;
                        entity.CreationUserId = entity.ModificationUserId = UserId;
                        e.AddedEntities.Add(entityEntry);
                    }
                    else if (entityEntry.State == EntityState.Deleted)
                    {
                        TrackChange(entityEntry, ChangeAction.Delete);
                    }
                    else if (entityEntry.State == EntityState.Modified)
                    {
                        entity.ModificationDate = DateTime.UtcNow;
                        entity.ModificationUserId = UserId;
                        TrackChange(entityEntry, ChangeAction.Update);
                    }
                }
            }
        }

        private void OnSavedChanges(object sender, DataContext.SaveEventArgs e)
        {
            if (e.AddedEntities.Count > 0)
            {
                foreach (var entry in e.AddedEntities)
                {
                    TrackChange(entry, ChangeAction.Add);
                }
            }
        }

        protected virtual void TrackChange(EntityEntry entityEntry, ChangeAction action)
        {

        }
    }
}
