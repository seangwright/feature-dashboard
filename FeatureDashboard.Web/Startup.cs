using FeatureDashboard.Web.Core.Authentication;
using FeatureDashboard.Web.Core.Persistence;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace FeatureDashboard.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration) => Configuration = configuration;

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services
                .Configure<CookiePolicyOptions>(options =>
                {
                    // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                    options.CheckConsentNeeded = context => true;
                    options.MinimumSameSitePolicy = SameSiteMode.None;
                });

            services
                .AddAuthentication(sharedOptions =>
                {
                    sharedOptions.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                    sharedOptions.DefaultChallengeScheme = OpenIdConnectDefaults.AuthenticationScheme;
                })
                .AddAzureAd(options => Configuration.Bind("AzureAd", options))
                .AddCookie();

            services
                .AddMvc(options =>
                {
                    var policy = new AuthorizationPolicyBuilder()
                        .RequireAuthenticatedUser()
                        .Build();

                    options.Filters.Add(new AuthorizeFilter(policy));
                })
                .AddRazorPagesOptions(options =>
                {
                    options.Conventions.AddFolderRouteModelConvention("/", pageRouteModel =>
                    {
                        foreach (var selector in pageRouteModel.Selectors)
                        {
                            var attributeRouteModel = selector.AttributeRouteModel;
                            attributeRouteModel.Template = ToKebabCase(attributeRouteModel.Template);
                        }

                        string ToKebabCase(string s)
                        {
                            return Regex
                                .Replace(s, "([a-z])([A-Z])", "$1-$2", RegexOptions.Compiled)
                                .ToLower();
                        }
                    });
                    options.Conventions.AllowAnonymousToFolder("/account");
                })
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_1)
                .AddFeatureFolders();

            services
                .AddRouting(options =>
                {
                    options.LowercaseUrls = true;
                });

            services
                .AddDbContext<FeaturesContext>(options =>
                {
                    string connectionString = Configuration.GetConnectionString("FeaturesDashboard");

                    options.UseSqlServer(connectionString);
                });

            services
                .AddScoped<IFeaturesContext>(provider => provider.GetService<FeaturesContext>());
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseAuthentication();

            app.UseMvc();
        }
    }
}
