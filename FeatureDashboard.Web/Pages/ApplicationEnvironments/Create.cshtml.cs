using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using FeatureDashboard.Web.Core.Persistence;

namespace FeatureDashboard.Web.Pages.ApplicationEnvironments
{
    public class CreateModel : PageModel
    {
        private readonly IFeaturesContext db;

        public CreateModel(IFeaturesContext db) => this.db = db;

        public IActionResult OnGet() => Page();

        [BindProperty]
        public ApplicationEnvironment ApplicationEnvironment { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            db.ApplicationEnvironments.Add(ApplicationEnvironment);
            await db.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}