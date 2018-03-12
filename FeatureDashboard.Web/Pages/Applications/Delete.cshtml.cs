using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using FeatureDashboard.Web.Core.Persistence;

namespace FeatureDashboard.Web.Pages.Applications
{
    public class DeleteModel : PageModel
    {
        private readonly IFeaturesContext db;

        public DeleteModel(IFeaturesContext db) => this.db = db;

        [BindProperty]
        public Application Application { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Application = await db.Applications
                .Include(a => a.ApplicationEnvironment)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (Application == null)
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

            Application = await db.Applications.FindAsync(id);

            if (Application != null)
            {
                db.Applications.Remove(Application);
                await db.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
