using FeatureDashboard.Web.Core.Persistence;
using FeatureDashboard.Web.Pages.ApplicationEnvironments;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FeatureDashboard.Web.Pages.Applications
{
    public class IndexModel : PageModel
    {
        private readonly IFeaturesContext db;

        public IndexModel(IFeaturesContext db) => this.db = db;

        public IList<Application> Applications { get; set; }

        public async Task OnGet()
        {
            Applications = await db
            .Applications
            .Include(a => a.ApplicationEnvironment)
            .AsNoTracking()
            .ToListAsync();
        }  
    }
}