using UniversityCommunity.Data.EntityFramework;
using UniversityCommunity.Business.Configuraions;
using ELECTRACORE.Business.Utilities.Api.Extensions;
using UniversityCommunity.Business.Session;

try
{
    var builder = WebApplication.CreateBuilder(args);
    var services = builder.Services;
    services.AddHttpContextAccessor();

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

    AppHttpContext.Configure(app.Services.GetRequiredService<IHttpContextAccessor>());

    app.UseHttpsRedirection();
    app.UseStaticFiles();
    app.UseSession();

    app.UseRouting();
    app.UseStatusCodePagesWithReExecute("/Customer/ErrorPage", "?code=");

    app.MapControllerRoute(
        name: "default",
        pattern: "{controller=Community}/{action=CommunityList}");

    app.Run();

}
catch (Exception ex)
{

    throw;
}