using Microsoft.Extensions.FileProviders;
using StudentManagement.Application;
using StudentManagement.Infrastructure;
using StudentManagement.Infrastructure.Persistence;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddPolicy("FrontendPolicy", policy =>
    {
        policy
            .WithOrigins(
                "http://localhost:3000",
                "http://127.0.0.1:3000")
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

var app = builder.Build();
var webClientPublicPath = Path.GetFullPath(Path.Combine(builder.Environment.ContentRootPath, "..", "..", "web-client", "public"));
var webClientFileProvider = new PhysicalFileProvider(webClientPublicPath);

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    dbContext.Database.EnsureCreated();
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseExceptionHandler("/error");
app.UseCors("FrontendPolicy");
var defaultFilesOptions = new DefaultFilesOptions();
defaultFilesOptions.DefaultFileNames.Clear();
defaultFilesOptions.DefaultFileNames.Add("dashboard.html");
defaultFilesOptions.FileProvider = webClientFileProvider;
app.UseDefaultFiles(defaultFilesOptions);
app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = webClientFileProvider
});
app.UseAuthorization();

app.MapControllers();

app.MapGet("/error", () => Results.Problem("An unexpected error occurred."));
app.MapGet("/health", () => Results.Ok(new { status = "Healthy" }));

app.Run();
