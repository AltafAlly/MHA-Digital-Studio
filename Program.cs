using Microsoft.EntityFrameworkCore;
using MHAWebDesign.Data;
using MHAWebDesign.Models;
using MHAWebDesign.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

var connectionString =
    builder.Configuration.GetConnectionString("DefaultConnection");

if (string.IsNullOrWhiteSpace(connectionString))
{
    throw new InvalidOperationException(
        "Database connection string 'DefaultConnection' is missing."
    );
}

builder.Services.AddDbContext<MhaDbContext>(
    options => options.UseSqlServer(connectionString)
);

builder.Services.Configure<EmailSettings>(
    builder.Configuration.GetSection("EmailSettings")
);

builder.Services.AddScoped<IEmailService, EmailService>();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

/*
 * ============================================================
 * CANONICAL DOMAIN + LEGACY URL REDIRECTS
 * ============================================================
 *
 * Official domain:
 * https://mhadigital.co.za
 *
 * Examples:
 *
 * https://www.mhadigital.co.za/services
 * -> https://mhadigital.co.za/services
 *
 * https://mhadigital.co.za/Home/Services
 * -> https://mhadigital.co.za/services
 */

var legacyRoutes =
    new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
    {
        ["/Home"] = "/",
        ["/Home/Index"] = "/",
        ["/Home/Services"] = "/services",
        ["/Home/Portfolio"] = "/work",
        ["/Portfolio"] = "/work",
        ["/Home/About"] = "/about",
        ["/Home/Contact"] = "/contact",
        ["/Home/Vela"] = "/work/vela",
        ["/Home/Inventory"] = "/work/inventory"
    };

app.Use(async (context, next) =>
{
    var host = context.Request.Host.Host;

    var isProductionHost =
        host.Equals(
            "mhadigital.co.za",
            StringComparison.OrdinalIgnoreCase
        )
        ||
        host.Equals(
            "www.mhadigital.co.za",
            StringComparison.OrdinalIgnoreCase
        );

    var isWww =
        host.Equals(
            "www.mhadigital.co.za",
            StringComparison.OrdinalIgnoreCase
        );

    var isSafeMethod =
        HttpMethods.IsGet(context.Request.Method)
        ||
        HttpMethods.IsHead(context.Request.Method);

    if (isProductionHost && isSafeMethod)
    {
        var requestedPath =
            context.Request.Path.Value ?? "/";

        var normalizedPath =
            requestedPath.Length > 1
                ? requestedPath.TrimEnd('/')
                : requestedPath;

        var hasLegacyRoute =
            legacyRoutes.TryGetValue(
                normalizedPath,
                out var cleanPath
            );

        var hasTrailingSlash =
            requestedPath.Length > 1 &&
            requestedPath.EndsWith('/');

        if (isWww || hasLegacyRoute || hasTrailingSlash)
        {
            var targetPath =
                hasLegacyRoute
                    ? cleanPath!
                    : normalizedPath;

            if (string.IsNullOrWhiteSpace(targetPath))
            {
                targetPath = "/";
            }

            var canonicalUrl =
                $"https://mhadigital.co.za" +
                $"{targetPath}" +
                $"{context.Request.QueryString}";

            context.Response.StatusCode =
                StatusCodes.Status301MovedPermanently;

            context.Response.Headers.Location =
                canonicalUrl;

            return;
        }
    }

    await next();
});

app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

/*
 * ============================================================
 * PUBLIC-FACING ROUTES
 * ============================================================
 */

app.MapControllerRoute(
    name: "home",
    pattern: "",
    defaults: new
    {
        controller = "Home",
        action = "Index"
    })
    .WithStaticAssets();

app.MapControllerRoute(
    name: "services",
    pattern: "services",
    defaults: new
    {
        controller = "Home",
        action = "Services"
    })
    .WithStaticAssets();

app.MapControllerRoute(
    name: "work",
    pattern: "work",
    defaults: new
    {
        controller = "Home",
        action = "Portfolio"
    })
    .WithStaticAssets();

app.MapControllerRoute(
    name: "about",
    pattern: "about",
    defaults: new
    {
        controller = "Home",
        action = "About"
    })
    .WithStaticAssets();

app.MapControllerRoute(
    name: "contact",
    pattern: "contact",
    defaults: new
    {
        controller = "Home",
        action = "Contact"
    })
    .WithStaticAssets();

app.MapControllerRoute(
    name: "vela",
    pattern: "work/vela",
    defaults: new
    {
        controller = "Home",
        action = "Vela"
    })
    .WithStaticAssets();

app.MapControllerRoute(
    name: "inventory",
    pattern: "work/inventory",
    defaults: new
    {
        controller = "Home",
        action = "Inventory"
    })
    .WithStaticAssets();

/*
 * Fallback route.
 * Keep this because the application may still require
 * normal controller/action routing internally.
 */
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();

app.Run();