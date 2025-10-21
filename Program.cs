using System.Net.NetworkInformation;
using System.Reflection;
using FuerzaG.Application.Services;
using FuerzaG.Domain.Entities;
using FuerzaG.Domain.Ports;
using FuerzaG.Domain.Services.Validations;
using FuerzaG.Infrastructure;
using FuerzaG.Infrastructure.Connection;
using FuerzaG.Infrastructure.Persistence;
using FuerzaG.Infrastructure.Security;
using FuerzaG.Models;

var builder = WebApplication.CreateBuilder(args);

//Authentication management
builder.Services
    .AddAuthentication("GForceAuth")
    .AddCookie("GForceAuth", options =>
    {
        options.Cookie.Name = "GForceCookie";
        options.LoginPath = "/Login";
        options.AccessDeniedPath = "/AccessDenied"; //TODO
        options.LogoutPath = "/Logout"; //TODO
        options.ExpireTimeSpan = TimeSpan.FromMinutes(30);
    });

builder.Services.AddAuthorization();

builder.Services.AddHttpContextAccessor();

string connectionString = builder.Configuration.GetConnectionString("PostgreSql")!;

var connectionManager = DatabaseConnectionManager.GetInstance(connectionString);

builder.Services.AddSingleton(connectionManager);

builder.Services.AddScoped<IDbConnectionFactory, PostgreSqlConnectionFactory>();

// Email Configuration
builder.Services.Configure<MailSettings>(builder.Configuration.GetSection("EmailSettings"));
builder.Services.AddScoped<IMailSender, SmtpEmailSender>();

// Services injection
builder.Services.AddScoped<OwnerService>();
builder.Services.AddScoped<ServiceService>();
builder.Services.AddScoped<TechnicianService>();
builder.Services.AddScoped<UserAccountService>();
builder.Services.AddScoped<LoginService>();

// Validators
builder.Services.AddScoped<IValidator<Owner>,  OwnerValidator>();
builder.Services.AddScoped<IValidator<UserAccount>,  AccountValidator>();
builder.Services.AddScoped<IValidator<Technician>, TechnicianValidator>();

// Login Repository by injection
builder.Services.AddScoped<ILoginRepository, LoginRepository>();
builder.Services.AddScoped<IPasswordService, PasswordService>();


// Add services to the container.
builder.Services.AddRazorPages();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
}

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapStaticAssets();
app.MapRazorPages()
    .WithStaticAssets();

app.Run();