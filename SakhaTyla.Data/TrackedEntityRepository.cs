using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Cynosura.EF;
using SakhaTyla.Core.Entities;
using SakhaTyla.Core.Enums;
using SakhaTyla.Core.Security;
using SakhaTyla.Core.Infrastructure;

namespace SakhaTyla.Data
{
    public class TrackedEntityRepository<T> : BaseEntityRepository<T> where T : TrackedEntity
    {
        public TrackedEntityRepository(IDatabaseFactory databaseFactory, IUserInfoProvider userInfoProvider) 
            : base(databaseFactory, userInfoProvider)
        {
        }

        protected override void TrackChange(EntityEntry entityEntry, ChangeAction action)
        {
            var entity = (T)entityEntry.Entity;
            var entityType = entity.GetType();
            var from = action != ChangeAction.Add ?
                JsonSerializer.Serialize(entityEntry.OriginalValues.ToObject(), JsonSerializerHelper.JsonSerializerOptions) :
                "";
            var to = action != ChangeAction.Delete ?
                JsonSerializer.Serialize(entityEntry.CurrentValues.ToObject(), JsonSerializerHelper.JsonSerializerOptions) :
                "";
            var change = new EntityChange(entityType.Name)
            {
                EntityId = entity.Id,
                Action = action,
                From = from,
                To = to,
                CreationDate = DateTime.UtcNow,
                CreationUserId = UserId,
            };
            if (action == ChangeAction.Add)
            {
                Context.Database.ExecuteSqlRaw("insert into [EntityChanges] ([EntityName], [EntityId], [Action], [From], [To], [CreationDate], [CreationUserId]) " +
                    "values ({0}, {1}, {2}, {3}, {4}, {5}, {6})",
                    entityType.Name, entity.Id, action, from, to, DateTime.UtcNow, UserId!);
            }
            else
            {
                Context.Set<EntityChange>().Add(change);
            }
        }
    }
}
