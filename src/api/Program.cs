using Example.InterviewQuestion004.Api.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddCors(options => options.AddPolicy("AngularClient", policy =>
    policy.WithOrigins(
            "http://localhost:4200",
            "http://127.0.0.1:4200",
            "http://localhost:4204",
            "http://127.0.0.1:4204")
        .AllowAnyHeader()
        .AllowAnyMethod()));

var app = builder.Build();

await using (var scope = app.Services.CreateAsyncScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    await dbContext.Database.MigrateAsync();
}

app.UseCors("AngularClient");
app.MapGet("/health", () => Results.Ok(new { status = "healthy" }));
app.MapControllers();

app.Run();

public partial class Program;
