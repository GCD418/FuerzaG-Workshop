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

using System.Globalization;
using Microsoft.AspNetCore.Localization;

var builder = WebApplication.CreateBuilder(args);
// Razor Pages + localización
builder.Services.AddRazorPages()
    .AddViewLocalization()
    .AddDataAnnotationsLocalization();

// Mensajes del model binding en ESPAÑOL
builder.Services.Configure<Microsoft.AspNetCore.Mvc.MvcOptions>(options =>
{
    var m = options.ModelBindingMessageProvider;
    m.SetValueIsInvalidAccessor(v => $"El valor '{v}' no es válido.");
    m.SetAttemptedValueIsInvalidAccessor((v, f) => $"El valor '{v}' no es válido para {f}.");
    m.SetMissingBindRequiredValueAccessor(f => $"El valor para '{f}' es obligatorio.");
    m.SetMissingKeyOrValueAccessor(() => "Este campo es obligatorio.");
    m.SetUnknownValueIsInvalidAccessor(f => $"El valor para '{f}' es inválido.");
    m.SetValueMustBeANumberAccessor(f => $"El campo '{f}' debe ser numérico.");
});

var culture = new CultureInfo("es-BO");
//Session management
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

builder.Services.AddDataProtection();

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
builder.Services.AddScoped<IValidator<Service>,  ServiceValidator>();
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

app.UseSession();

app.UseAuthorization();

app.MapStaticAssets();
app.MapRazorPages()
    .WithStaticAssets();

app.Run();