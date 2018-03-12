using FeatureDashboard.Web.Pages.ApplicationEnvironments;
using FeatureDashboard.Web.Pages.Applications;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Threading;
using System.Threading.Tasks;

namespace FeatureDashboard.Web.Core.Persistence
{
    public interface IFeaturesContext
    {
        DbSet<ApplicationEnvironment> ApplicationEnvironments { get; }
        DbSet<Application> Applications { get; }

        EntityEntry Attach(object entity);

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken));
        Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default(CancellationToken));
    }
}
