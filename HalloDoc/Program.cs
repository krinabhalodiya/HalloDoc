using HalloDoc.Entity.DataContext;
using HallodocMVC.Repository.Admin.Repository;
using HallodocMVC.Repository.Admin.Repository.Interface;
using HelloDocAdmin.Repositories;
using AspNetCoreHero.ToastNotification;
using AspNetCoreHero.ToastNotification.Extensions;
using HalloDoc.Entity.Models;
using HallodocMVC.Repository.Patient.Repository;
using HallodocMVC.Repository.Patient.Repository.Interface;

var builder = WebApplication.CreateBuilder(args);
var emailConfig = builder.Configuration
        .GetSection("EmailConfiguration")
        .Get<EmailConfiguration>();
builder.Services.AddSingleton(emailConfig);
// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<HalloDocContext>();
builder.Services.AddHttpContextAccessor();
builder.Services.AddSession();
builder.Services.AddScoped<IAdminDashBoardRepository, AdminDashBoardRepository>();
builder.Services.AddScoped<IAdminDashBoardActionsRepository, AdminDashBoardActionsRepository>();
builder.Services.AddScoped< IComboboxRepository,ComboboxRepository>();
builder.Services.AddScoped< ILoginRepository, LoginRepository>();
builder.Services.AddScoped< IJwtService, JwtService>();
builder.Services.AddScoped<IProviderRepository, ProviderRepository>();
builder.Services.AddScoped<IMyProfileRepository, MyProfileRepository>();
builder.Services.AddScoped<IPatientDashboard, PatientDashboard>();
builder.Services.AddScoped<IProfile, Profile>();
builder.Services.AddScoped<IRoleAccessRepository, RoleAccessRepository>();
builder.Services.AddScoped<ICreateRequest, CreateRequest>();
builder.Services.AddScoped<IPartnersRepository, PartnersRepository>();
builder.Services.AddScoped<ISchedulingRepository, SchedulingRepository>();
builder.Services.AddNotyf(config => { config.DurationInSeconds = 3; config.IsDismissable = true; config.Position = NotyfPosition.TopRight; });
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
app.UseSession();
app.UseRouting();
app.UseNotyf();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=LandingPage}/{id?}");

app.Run();
