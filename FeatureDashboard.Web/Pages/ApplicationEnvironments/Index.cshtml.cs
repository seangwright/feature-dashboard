using FeatureDashboard.Web.Core.Persistence;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FeatureDashboard.Web.Pages.ApplicationEnvironments
{
    public class IndexModel : PageModel
    {
        private readonly FeaturesContext db;

        public IndexModel(FeaturesContext db) => this.db = db;

        public IList<ApplicationEnvironment> ApplicationEnvironments { get;set; }

        public async Task OnGetAsync() => 
            ApplicationEnvironments = await db
            .ApplicationEnvironments
            .ToListAsync();
    }
}
