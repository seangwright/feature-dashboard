using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FeatureDashboard.Web.Core.Persistence;

namespace FeatureDashboard.Web.Pages.ApplicationEnvironments
{
    public class EditModel : PageModel
    {
        private readonly IFeaturesContext db;

        public EditModel(IFeaturesContext db) => this.db = db;

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

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            db.Attach(ApplicationEnvironment).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ApplicationEnvironmentExists(ApplicationEnvironment.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool ApplicationEnvironmentExists(int id) => db.ApplicationEnvironments.Any(e => e.Id == id);
    }
}
