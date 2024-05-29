using UniversityCommunity.Data.EntityFramework;
using UniversityCommunity.Business.Configuraions;
using ELECTRACORE.Business.Utilities.Api.Extensions;

try
{
    var builder = WebApplication.CreateBuilder(args);
    var services = builder.Services;
    builder.Services.AddControllersWithViews();
    services.AddDIServices();

    builder.Services.AddDbContext<UniversityCommunityContext>();
    builder.Services.AddSession();



    var emailConfig = builder.Configuration
            .GetSection("EmailConfiguration")
            .Get<EmailConfiguration>();
    builder.Services.AddSingleton(emailConfig);

    var app = builder.Build();
    if (!app.Environment.IsDevelopment())
    {
        app.UseExceptionHandler("/Home/Error");
        app.UseHsts();
    }

    app.UseHttpsRedirection();
    app.UseStaticFiles();
    app.UseSession();

    app.UseRouting();
    app.UseStatusCodePagesWithReExecute("/Customer/ErrorPage", "?code=");

    app.MapControllerRoute(
        name: "default",
        pattern: "{controller=Authentication}/{action=Login}");

    app.Run();

}
catch (Exception ex)
{

    throw;
}

