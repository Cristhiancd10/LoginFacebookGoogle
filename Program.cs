using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using EjemploFacebook.Data;
var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("EjemploFacebookContextDbConnection") ?? throw new InvalidOperationException("Connection string 'EjemploFacebookContextDbConnection' not found.");

builder.Services.AddDbContext<EjemploFacebookContextDb>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<EjemploFacebookContextDb>();

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddAuthentication().AddFacebook(option =>
{
    option.AppId = "1893965451062847";
    option.AppSecret = "bc98780c7a75d0ed53ead2eac9829512";
}
);

builder.Services.AddAuthentication().AddGoogle(option =>
{
    option.ClientId = "429984605423-sua6tf605g7vtf4ffhltuud5tdvvcsmt.apps.googleusercontent.com";
    option.ClientSecret = "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";
}
);

var app = builder.Build();

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
app.UseAuthentication();;

app.UseAuthorization();

app.MapRazorPages();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
