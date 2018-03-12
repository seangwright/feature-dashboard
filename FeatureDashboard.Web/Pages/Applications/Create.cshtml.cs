using FeatureDashboard.Web.Core.Persistence;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FeatureDashboard.Web.Pages.Applications
{
    public class CreateModel : PageModel
    {
        private readonly IFeaturesContext db;

        public CreateModel(IFeaturesContext db) => this.db = db;

        public IEnumerable<SelectListItem> ApplicationEnvironments { get; set; }

        [BindProperty]
        public Application Application { get; set; }

        public async Task OnGetAsync() => await PopulateFormData();

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                await PopulateFormData();

                return Page();
            }

            db.Applications.Add(Application);
            await db.SaveChangesAsync();

            return RedirectToPage("./Index");
        }

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