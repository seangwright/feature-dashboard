using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FeatureDashboard.Web.Pages.ApplicationEnvironments
{
    public class ApplicationEnvironment
    {
        public int Id { get; set; }
        public string Name { get; set; }

        [Display(Name = "Is Production")]
        public bool IsProduction { get; set; }
    }
}
