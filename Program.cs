using FuerzaG.Application.Services;
using FuerzaG.Infrastructure.Connection;

var builder = WebApplication.CreateBuilder(args);

string connectionString = builder.Configuration.GetConnectionString("PostgreSql")!;

var connectionManager = DatabaseConnectionManager.GetInstance(connectionString);

builder.Services.AddSingleton(connectionManager);

builder.Services.AddScoped<IDbConnectionFactory, PostgreSqlConnectionFactory>();


// Services injection
builder.Services.AddScoped<OwnerService>();
builder.Services.AddScoped<ServiceService>();
builder.Services.AddScoped<TechnicianService>();

// Add services to the container.
builder.Services.AddRazorPages();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
}

app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();
app.MapRazorPages()
    .WithStaticAssets();

app.Run();