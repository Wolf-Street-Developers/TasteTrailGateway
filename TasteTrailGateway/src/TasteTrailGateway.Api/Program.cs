using Ocelot.DependencyInjection;
using Ocelot.Middleware;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        builder =>
        {
            builder.AllowAnyOrigin()  // Разрешить всем источникам
                   .AllowAnyMethod()  // Разрешить все HTTP методы
                   .AllowAnyHeader(); // Разрешить любые заголовки
        });
});

builder.Configuration.AddJsonFile("ocelot.json", false, reloadOnChange: true);
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