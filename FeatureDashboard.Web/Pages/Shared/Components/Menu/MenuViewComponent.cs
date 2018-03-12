using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace FeatureDashboard.Web.Pages.Shared.Components.Menu
{
    public class MenuViewComponent : ViewComponent
    {
        private readonly IActionDescriptorCollectionProvider actionDescriptorCollectionProvider;

        public MenuViewComponent(IActionDescriptorCollectionProvider actionDescriptorCollectionProvider) => this.actionDescriptorCollectionProvider = actionDescriptorCollectionProvider;

        public IViewComponentResult Invoke()
        {
            var routes = actionDescriptorCollectionProvider.ActionDescriptors.Items;

            var result = routes
                .Where(actionDescriptor => actionDescriptor is PageActionDescriptor)
                .Select(actionDescriptor => actionDescriptor as PageActionDescriptor)
                .Where(pageActionDescriptor => pageActionDescriptor.RouteValues["page"].Contains("Index", StringComparison.InvariantCultureIgnoreCase))
                .Where(pageActionDescriptor => pageActionDescriptor.RelativePath.Contains("Pages", StringComparison.InvariantCultureIgnoreCase))
                .Where(pageActionDescriptor => !pageActionDescriptor.AttributeRouteInfo.Template.Contains("Index", StringComparison.InvariantCultureIgnoreCase))
                .Select(pageActionDescriptor =>
                {
                    var info = new RouteInformation();

                    string fullRouteName = pageActionDescriptor.RouteValues["page"];

                    int finalRoutePartIndex = fullRouteName.LastIndexOf('/');

                    string mainRoutePart = finalRoutePartIndex == -1
                        ? fullRouteName.Substring(1, fullRouteName.Length - 1)
                        : fullRouteName.Substring(1, finalRoutePartIndex);

                    int secondRoutePartIndex = mainRoutePart.IndexOf('/');

                    string firstRoutePart = secondRoutePartIndex == -1
                        ? mainRoutePart
                        : mainRoutePart.Substring(0, secondRoutePartIndex);

                    info.Path = $"/{pageActionDescriptor.AttributeRouteInfo.Template}";
                    info.Name = Regex.Replace(firstRoutePart, "(\\B[A-Z])", " $1");

                    return info;
                });

            return View(result);
        }

        private string PascalToKebabCase(string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                return value;
            }

            return Regex.Replace(
                value,
                "(?<!^)([A-Z][a-z]|(?<=[a-z])[A-Z])",
                "-$1",
                RegexOptions.Compiled)
                .Trim()
                .ToLower();
        }
    }
}
