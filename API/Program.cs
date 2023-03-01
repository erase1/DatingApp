using API.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

//Add Services to Container
builder.Services.AddControllers();
builder.Services.AddDbContext<DataContext>(opt => 
{
    opt.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")); //DefaultConnection defined in from appsettings.json file
});
var app = builder.Build();

// Configure the HTTP request pipeline.
app.MapControllers();

app.Run();
