using ContactsApp.BLL.Interfaces;
using ContactsApp.BLL.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace ContactsApp.WEB
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient<IContactManager, ContactManager>();
            services.AddTransient<IContactRepository, ContactRepository>();
            services.AddControllersWithViews();
            services.AddRazorPages();
            services.AddMvc();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }
        
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseCookiePolicy();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
           {
               endpoints.MapControllerRoute(
                   name: "default",
                   pattern: "{controller=Account}/{action=ContactsList}");
           });
        }
    }
}
