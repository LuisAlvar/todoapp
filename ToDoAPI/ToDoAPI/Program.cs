using Microsoft.EntityFrameworkCore;
using ToDoAPI.Data;
using ToDoAPI.Models;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<AppDbContext>(opt => opt.UseSqlite(builder.Configuration.GetConnectionString("SqliteConnection")));


// Add services to the container.
var app = builder.Build();

// Configure the HTTP request pipeline.
//app.UseHttpsRedirection();

app.MapGet("api/todo", async (AppDbContext context) =>
{
    var items = await context.ToDos.ToListAsync();

    return Results.Ok(items);

});

app.MapGet("api/todo/{id}", async (AppDbContext context, int id) => 
{
  var item = await context.ToDos.FirstOrDefaultAsync(obj => obj.Id == id);

  return Results.Ok(item);
});

app.MapPost("api/todo", async (AppDbContext context, ToDo data) => 
{
    await context.ToDos.AddAsync(data);

    await context.SaveChangesAsync();

    return Results.Created($"api/todo/{data.Id}", data);
});

app.MapPut("api/todo/{id}", async (AppDbContext context, int id, ToDo data) => 
{
    var result = await context.ToDos.FirstOrDefaultAsync(obj => obj.Id == id);

    if (result == null)
    {
        return Results.NotFound();
    }

    result.ToDoName = data.ToDoName;

    await context.SaveChangesAsync();

    return Results.NoContent();
});

app.MapDelete("api/todo/{id}", async (AppDbContext context, int id) => 
{
    var result = await context.ToDos.FirstOrDefaultAsync(obj => obj.Id == id);

    if (result == null)
    {
        return Results.NotFound();
    }

    context.ToDos.Remove(result);

    await context.SaveChangesAsync();

    return Results.NoContent();
});

app.Run();