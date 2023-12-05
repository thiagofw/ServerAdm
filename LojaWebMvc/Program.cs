
using LojaWebMvc.Data;
using LojaWebMvc.Services;
using LojaWebMvc.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using MySql.EntityFrameworkCore.Extensions;
using System.Globalization;
using Microsoft.AspNetCore.Localization;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddEntityFrameworkMySQL()
    .AddDbContext<VsproContext>(options =>
    {
        options.UseMySQL(builder.Configuration.GetConnectionString("LojaContext"));
    });
    
// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddTransient<IDepartmentService, DepartmentService>();
builder.Services.AddTransient<ISellerService, SellerService>();
builder.Services.AddTransient<ISalesRecordService, SalesRecordService>();
builder.Services.AddTransient<IServerService, ServerService>();
builder.Services.AddTransient<ILoginService, LoginService>();

var app = builder.Build();
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    SeedingService.Initialize(services);
}

/*var enUS = new CultureInfo("en-US");
var localizationOptions = new RequestLocalizationOptions
{
    DefaultRequestCulture = new RequestCulture(enUS),
    SupportedCultures = new List<CultureInfo> {enUS},
    SupportedUICultures = new List<CultureInfo> {enUS}
};*/
//app.UseRequestLocalization(localizationOptions);
// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{

    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
