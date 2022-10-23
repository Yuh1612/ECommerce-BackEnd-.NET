using ELDesk.Shared.Middlewares;
using Microsoft.Extensions.FileProviders;

namespace ECommerce.Products.API.Extensions
{
    public static class ApplicationBuilderExtensions
    {
        public static void ConfigureDefault(this IApplicationBuilder app)
        {
            app.UseHttpsRedirection();

            app.UseDeveloperExceptionPage();

            app.UseMiddleware<ExceptionMiddleware>();

            // global cors policy // TODO: Enable all origin as we moved to aws gateway
            app.UseCors(x => x
                .AllowAnyMethod()
                .AllowAnyHeader()
                .SetIsOriginAllowed(origin => true) // allow any origin
                .AllowCredentials()
                .WithExposedHeaders("*")
            ); // allow credentials

            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "Resources")),
                RequestPath = "/images"
            });
            app.UseDirectoryBrowser(new DirectoryBrowserOptions
            {
                FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "Resources")),
                RequestPath = "/images"
            });

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}