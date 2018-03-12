using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using FeatureDashboard.Web.Core.Persistence;

namespace FeatureDashboard.Web.Pages.ApplicationEnvironments
{
    public class DeleteModel : PageModel
    {
        private readonly IFeaturesContext db;

        public DeleteModel(IFeaturesContext db) => this.db = db;

        [BindProperty]
        public ApplicationEnvironment ApplicationEnvironment { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            ApplicationEnvironment = await db.ApplicationEnvironments.FirstOrDefaultAsync(m => m.Id == id);

            if (ApplicationEnvironment == null)
            {
                return NotFound();
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            ApplicationEnvironment = await db.ApplicationEnvironments.FindAsync(id);

            if (ApplicationEnvironment != null)
            {
                db.ApplicationEnvironments.Remove(ApplicationEnvironment);
                await db.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
