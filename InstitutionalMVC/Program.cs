using DataAccessLayer.AutoMapper;
using Institutional;
using Institutional.Localization;
using InstitutionalMVC.Helper;
using InstitutionalMVC.ServiceExtancion;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Options;
using Service.Abstract;
using Service.Concrete;
using System.Globalization;

var builder = WebApplication.CreateBuilder(args);
builder.Services
    .AddRazorPages()
    .AddViewLocalization();
builder.Services.AddLocalization(options => options.ResourcesPath = "Resources");
builder.Services.AddSingleton<IActionContextAccessor, ActionContextAccessor>();
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
// Add services to the container.
builder.Services.AddMvc().AddViewLocalization().AddDataAnnotationsLocalization();
builder.Services.AddControllersWithViews();


//builder.Services.ConfiguratioSQLContext(builder.Configuration);
//builder.Services.ConfiguerRepostoryManager();
//builder.Services.ConfiguerServiceManager();
//builder.Services.ConfigureIdentity();


builder.Services.AddLocalization();
builder.Services.AddSingleton<LocalizationMiddleware>();
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSingleton<IStringLocalizerFactory, JsonStringLocalizerFactory>();
builder.Services.InitializeClientBaseAddress(builder.Configuration);
builder.Services.AddAutoMapper(typeof(MappingProfile).Assembly);
//builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(x => { x.LoginPath = "/Admin/Login/Index"; });
builder.Services.Configure<RequestLocalizationOptions>(options =>
{
    var supportedCultures = new[]
    {
        new CultureInfo("tr-TR"),
        new CultureInfo("en-US")
    };

    //options.DefaultRequestCulture = new RequestCulture(culture: supportedCultures[0], uiCulture: supportedCultures[0]);
    options.DefaultRequestCulture = new RequestCulture(new CultureInfo("tr-TR"));
    options.SupportedCultures = supportedCultures;
    options.SupportedUICultures = supportedCultures;
});
var app = builder.Build();
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    var localizationOptions = services.GetRequiredService<IOptions<RequestLocalizationOptions>>().Value;
    app.UseRequestLocalization(localizationOptions);
}
// Configure the HTTP request pipeline. 
if (!app.Environment.IsDevelopment())
{
	app.UseExceptionHandler("/Home/Error");
	// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
	app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseMiddleware<LocalizationMiddleware>();
app.UseRouting();
//app.UseAuthentication();
app.UseAuthorization();

app.UseEndpoints(endpoints => {
	endpoints.MapAreaControllerRoute(name: "Admin", areaName: "Admin", pattern: "Admin/{controller=Login}/{action=Index}/{id?}");
    endpoints.MapDefaultControllerRoute();
});

app.Run();
