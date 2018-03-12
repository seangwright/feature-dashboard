using FeatureDashboard.Web.Pages.ApplicationEnvironments;
using System;
using System.ComponentModel.DataAnnotations;

namespace FeatureDashboard.Web.Pages.Applications
{
    public class Application
    {
        public int Id { get; set; }

        [Required]
        [MinLength(1)]
        public string Name { get; set; }

        [Required]
        [MinLength(2)]
        public string Url { get; set; }

        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "An Application Environment is required")]
        public int ApplicationEnvironmentId { get; set; }

        [Display(Name = "Application Environment")]
        public ApplicationEnvironment ApplicationEnvironment { get; set; }

        public DateTime DateCreated { get; set; }
        public DateTime DateModified { get; set; }
    }
}
