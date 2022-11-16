using ELDesk.Shared.Middlewares;
using Microsoft.Extensions.FileProviders;

namespace ECommerce.Products.API.Extensions
{
    public static class ApplicationBuilderExtensions
    {
        public static void ConfigureDefault(this IApplicationBuilder app)
        {
            //app.UseHttpsRedirection();
            app.UseRouting();
            app.UseDeveloperExceptionPage();

            app.UseMiddleware<ExceptionMiddleware>();

            app.UseCors(x => x
                            .AllowAnyOrigin()
                            .AllowAnyMethod()
                            .AllowAnyHeader()
                            .SetIsOriginAllowed(origin => true) // allow any origin
                            .WithExposedHeaders("*")
                        );

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

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}