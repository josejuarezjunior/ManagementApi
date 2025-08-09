using Core.Interfaces;
using Infrastructure.Data;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.WithOrigins("http://localhost:4200")
            .AllowAnyHeader()
            .AllowAnyMethod();
    });

});

builder.Services.AddScoped<IMovieRepository, MovieRepository>();
builder.Services.AddScoped<IBookRepository, BookRepository>();
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddScoped<IUnitOfWork, UnitOfWOrk>();

var app = builder.Build();

app.UseCors();

app.MapControllers();

//app.MapGet("/", () => "Hello World!");
app.MapGet("/", () =>
{
    return Results.Redirect("api/movies");
});

app.Run();
