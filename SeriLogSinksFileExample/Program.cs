using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

string template = "[{Timestamp:yyyy/MM/dd - HH:mm:ss zzz}] [{Level:u3}] {Message:lj} {NewLine} {Exception}";
string path = "SeriLog/log.txt";

Log.Logger = new LoggerConfiguration()
    .WriteTo
    .File(path, outputTemplate: template, rollingInterval: RollingInterval.Day, fileSizeLimitBytes: 1024, rollOnFileSizeLimit: true, retainedFileTimeLimit: null)
    .MinimumLevel.Warning()
    .CreateLogger();

builder.Host.UseSerilog();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();
