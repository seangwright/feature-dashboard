using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FeatureDashboard.Web.Core.Persistence;

namespace FeatureDashboard.Web.Pages.Applications
{
    public class EditModel : PageModel
    {
        private readonly FeaturesContext db;

        public EditModel(FeaturesContext db) => this.db = db;

        [BindProperty]
        public Application Application { get; set; }

        public IList<SelectListItem> ApplicationEnvironments { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Application = await db
                .Applications
                .Include(a => a.ApplicationEnvironment)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (Application == null)
            {
                return NotFound();
            }

            await PopulateFormData();

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                await PopulateFormData();

                return Page();
            }

            db.Attach(Application).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ApplicationExists(Application.Id))
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

        private bool ApplicationExists(int id) => db.Applications.Any(e => e.Id == id);

        private async Task PopulateFormData()
        {
            ApplicationEnvironments = await db
                .ApplicationEnvironments
                .AsNoTracking()
                .Select(ae => new SelectListItem
                {
                    Text = ae.Name,
                    Value = ae.Id.ToString()
                })
                .ToListAsync();
        }
    }
}
