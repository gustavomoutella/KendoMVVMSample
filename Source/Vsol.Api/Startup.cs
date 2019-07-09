using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using Vsol.Api.Shared.Infra.Data;
using System.Net;
using Microsoft.AspNetCore.Diagnostics;
using Newtonsoft.Json;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.PlatformAbstractions;
using System.Globalization;
using System.IO;
using Microsoft.AspNetCore;
using System.Reflection;

namespace Vsol.Api
{
    public class Startup
    {
        private IServiceCollection _services;
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        private void InitializeAppSettings()
        {
            var siteSection = Configuration.GetSection("Site");
            AppSettings.Site.UrlSite = "http://localhost:4300/";// siteSection.GetValue<string>("UrlSite");
            AppSettings.Site.UrlApi = "http://localhost:49200/";// siteSection.GetValue<string>("UrlApi");

            AppSettings.ConnectionStrings.DefaultConnection = "Server=.\\SQLEXPRESS; Database=VSol; Persist Security Info=True; Integrated Security=SSPI;";// Configuration.GetConnectionString("DefaultConnection");
        }

        private IList<string> GetPolicies()
        {
            var policies = new List<string>();
            var fields = typeof(Policies).GetFields().ToList();

            foreach (var field in fields)
            {
                policies.Add(field.GetValue(field).ToString());
            }

            return policies;
        }

        private static void ConfigurePolicies(AuthorizationOptions options)
        {
            var policies = typeof(Policies).GetFields().ToList();

            foreach (var constant in policies)
            {
                var permission = constant.GetValue(constant).ToString();

                options.AddPolicy(permission,
                    policy => policy.Requirements.Add(new PermissionRequirement(permission)));
            }
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            app.UseDeveloperExceptionPage();
            app.UseBrowserLink();

            app.UseStaticFiles();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });

            app.UseAuthentication();

            app.UseCors(x =>
            {
                x.AllowAnyHeader();
                x.AllowAnyMethod();
                x.AllowAnyOrigin();
            });

            app.UseExceptionHandler(errorApp =>
            {
                errorApp.Run(async context =>
                {
                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    context.Response.ContentType = "application/json";

                    var ex = context.Features.Get<IExceptionHandlerFeature>();
                    if (ex != null)
                    {
                        var err = JsonConvert.SerializeObject(new
                        {
                            Message = ex.Error.Message,
                            Stacktrace = ex.Error.StackTrace
                        });

                        await context.Response.Body.WriteAsync(Encoding.ASCII.GetBytes(err), 0, err.Length).ConfigureAwait(false);
                    }
                });
            });

            loggerFactory.AddConsole();
        }

        public void ConfigureServices(IServiceCollection services)
        {
            _services = services;

            // AppSettings
            InitializeAppSettings();

            services.AddMvc();

            // Add framework services.
            //services.AddMvc(config =>
            //{
            //    var policy = new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build();
            //    config.Filters.Add(new AuthorizeFilter(policy));
            //})
            //.AddJsonOptions(x =>
            //{
            //    x.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            //    x.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
            //});
            //
            services.AddCors();
            services.AddAuthorization(options => ConfigurePolicies(options));

            services.AddAuthentication(options =>
            {
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddOAuthValidation()
            .AddOpenIdConnectServer(options =>
            {
                options.Provider = new AuthorizationProvider();
                options.TokenEndpointPath = "/connect/token";
                options.AllowInsecureHttp = true;
                options.SigningCredentials.AddEphemeralKey();
            });

            // Registrando as dependências.
            services.AddScoped<IAuthorizationHandler, PermissionHandler>();
            IoC.Register(services);
        }
    }

    public class PermissionRequirement : IAuthorizationRequirement
    {
        public string Permission { get; set; }

        public PermissionRequirement(string permission)
        {
            Permission = permission;
        }
    }
}