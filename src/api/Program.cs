using System.Diagnostics;
using System.Threading.RateLimiting;
using Example.InterviewQuestion004.Api.Data;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

const long maximumRequestBodyBytes = 3L * 1024 * 1024;

builder.WebHost.ConfigureKestrel(options =>
    options.Limits.MaxRequestBodySize = maximumRequestBodyBytes);
builder.Services.AddControllers();
builder.Services.AddProblemDetails(options =>
{
    options.CustomizeProblemDetails = context =>
    {
        context.ProblemDetails.Extensions["traceId"] =
            Activity.Current?.Id ?? context.HttpContext.TraceIdentifier;

        if (context.ProblemDetails.Status == StatusCodes.Status500InternalServerError)
        {
            context.ProblemDetails.Title = "An unexpected error occurred.";
            context.ProblemDetails.Detail = null;
        }
    };
});
builder.Services.AddRateLimiter(options =>
{
    options.RejectionStatusCode = StatusCodes.Status429TooManyRequests;
    options.OnRejected = (context, _) =>
    {
        var logger = context.HttpContext.RequestServices
            .GetRequiredService<ILoggerFactory>()
            .CreateLogger("RateLimiting");
        logger.LogWarning("Rate limit rejected {Method} {Path} with trace {TraceId}",
            context.HttpContext.Request.Method,
            context.HttpContext.Request.Path,
            Activity.Current?.Id ?? context.HttpContext.TraceIdentifier);
        return ValueTask.CompletedTask;
    };
    options.AddPolicy("candidate-write", httpContext =>
        RateLimitPartition.GetFixedWindowLimiter(
            httpContext.Connection.RemoteIpAddress?.ToString() ?? "unknown",
            _ => new FixedWindowRateLimiterOptions
            {
                PermitLimit = 20,
                Window = TimeSpan.FromMinutes(1),
                QueueLimit = 0,
                AutoReplenishment = true
            }));
});
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

app.UseExceptionHandler();
app.Use(async (context, next) =>
{
    context.Response.Headers.TryAdd("Content-Security-Policy",
        "default-src 'none'; frame-ancestors 'none'; base-uri 'none'");
    context.Response.Headers.TryAdd("X-Content-Type-Options", "nosniff");
    context.Response.Headers.TryAdd("X-Frame-Options", "DENY");
    context.Response.Headers.TryAdd("Referrer-Policy", "no-referrer");
    context.Response.Headers.TryAdd("Permissions-Policy", "camera=(), microphone=(), geolocation=()");
    await next();
});

await using (var scope = app.Services.CreateAsyncScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    await dbContext.Database.MigrateAsync();
}

app.UseCors("AngularClient");
app.UseRateLimiter();
app.MapGet("/health", () => Results.Ok(new { status = "healthy" }));
app.MapControllers();

app.Run();

public partial class Program;
