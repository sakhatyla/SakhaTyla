using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Duende.IdentityServer.EntityFramework.Entities;
using Duende.IdentityServer.EntityFramework.Extensions;
using Duende.IdentityServer.EntityFramework.Interfaces;
using Duende.IdentityServer.EntityFramework.Options;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Microsoft.Extensions.Options;
using Cynosura.EF;
using SakhaTyla.Core.Entities;
using SakhaTyla.Core.Infrastructure;

namespace SakhaTyla.Data
{
    public class DataContext : IdentityDbContext<User, Role, int>, IPersistedGrantDbContext
    {
        private readonly IOptions<OperationalStoreOptions> _operationalStoreOptions;

        public event EventHandler<SaveEventArgs>? CustomSavingChanges;

        public event EventHandler<SaveEventArgs>? CustomSavedChanges;

        public DataContext(DbContextOptions<DataContext> options, 
            IOptions<OperationalStoreOptions> operationalStoreOptions)
            : base(options)
        {
            _operationalStoreOptions = operationalStoreOptions;
        }

        public DbSet<PersistedGrant> PersistedGrants { get; set; } = null!;

        public DbSet<DeviceFlowCodes> DeviceFlowCodes { get; set; } = null!;

        public DbSet<Key> Keys { get; set; } = null!;

        public override int SaveChanges()
        {
            var e = new SaveEventArgs();
            OnSavingChanges(e);
            var result = base.SaveChanges();
            OnSavedChanges(e);
            return result;
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            var e = new SaveEventArgs();
            OnSavingChanges(e);
            var result = await base.SaveChangesAsync(cancellationToken);
            OnSavedChanges(e);
            return result;
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ConfigurePersistedGrantContext(_operationalStoreOptions.Value);

            var assemblies = CoreHelper.GetPlatformAndAppAssemblies();
            builder.ApplyAllConfigurations(assemblies);

            // Specify all DateTime properties as Utc when read from database
            var dateTimeConverter = new ValueConverter<DateTime, DateTime>(
                v => v, v => DateTime.SpecifyKind(v, DateTimeKind.Utc));

            foreach (var entityType in builder.Model.GetEntityTypes())
            {
                foreach (var property in entityType.GetProperties())
                {
                    if (property.ClrType == typeof(DateTime) || property.ClrType == typeof(DateTime?))
                        property.SetValueConverter(dateTimeConverter);
                }
            }
        }

        protected virtual void OnSavingChanges(SaveEventArgs e)
        {
            CustomSavingChanges?.Invoke(this, e);
        }

        protected virtual void OnSavedChanges(SaveEventArgs e)
        {
            CustomSavedChanges?.Invoke(this, e);
        }

        Task<int> IPersistedGrantDbContext.SaveChangesAsync() => SaveChangesAsync();

        public class SaveEventArgs
        {
            public List<EntityEntry> AddedEntities { get; } = new List<EntityEntry>();
        }
    }
}
