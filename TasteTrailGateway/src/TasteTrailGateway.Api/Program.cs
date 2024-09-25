using Ocelot.DependencyInjection;
using Ocelot.Middleware;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        builder =>
        {
            builder.AllowAnyOrigin()
                   .AllowAnyMethod()
                   .AllowAnyHeader();
        });
});

builder.Configuration.AddJsonFile("./Configuration/UserExperience.json", false, reloadOnChange: true);
builder.Configuration.AddJsonFile("./Configuration/OwnerExperience.json", false, reloadOnChange: true);
builder.Configuration.AddJsonFile("./Configuration/AdminDashboard.json", false, reloadOnChange: true);
builder.Configuration.AddJsonFile("./Configuration/Identity.json", false, reloadOnChange: true);
builder.Services.AddOcelot();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();

var app = builder.Build();

app.UseCors("AllowAll");

app.UseSwagger();
app.UseSwaggerUI();
app.MapControllers();

await app.UseOcelot();

app.Run();