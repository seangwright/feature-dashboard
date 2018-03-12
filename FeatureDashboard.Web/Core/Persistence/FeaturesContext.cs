using FeatureDashboard.Web.Pages.ApplicationEnvironments;
using FeatureDashboard.Web.Pages.Applications;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace FeatureDashboard.Web.Core.Persistence
{
    public class FeaturesContext : DbContext, IFeaturesContext
    {
        public DbSet<ApplicationEnvironment> ApplicationEnvironments { get; set; }
        public DbSet<Application> Applications { get; set; }

        public FeaturesContext(DbContextOptions options) : base(options)
        {

        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            EnsureDateFieldsSet();

            return base.SaveChangesAsync(cancellationToken);
        }

        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default(CancellationToken))
        {
            EnsureDateFieldsSet();

            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }

        private void EnsureDateFieldsSet()
        {
            DateTime saveTime = DateTime.UtcNow;

            var addedEntities = ChangeTracker
                .Entries()
                .Where(e => e.State == EntityState.Added)
                .Where(e => e.Properties.Any(p => p.Metadata.Name == "DateCreated"));

            var modifiedEntities = ChangeTracker
                .Entries()
                .Where(e => e.State == EntityState.Modified)
                .Where(e => e.Properties.Any(p => p.Metadata.Name == "DateModified"));

            foreach (var entry in addedEntities)
            {
                entry.Property("DateCreated").CurrentValue = saveTime;
                entry.Property("DateModified").CurrentValue = saveTime;
            }

            foreach (var entry in modifiedEntities)
            {
                entry.Property("DateModified").CurrentValue = saveTime;
            }
        }
    }
}
