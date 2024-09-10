using Hangfire;
using Hangfire.Storage.SQLite;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddHangfire(configuration => configuration
            .UseSimpleAssemblyNameTypeSerializer()
            .UseRecommendedSerializerSettings()
            .UseSQLiteStorage());
builder.Services.AddHangfireServer();
builder.Services.AddControllers();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.UseHangfireDashboard("/dashboard");
app.Run();
