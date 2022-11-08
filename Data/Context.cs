using Microsoft.EntityFrameworkCore;
using Todo.API.Models;

namespace Todo.API.Data;

public class Context : DbContext
{
    public DbSet<TodoModel> Todos { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
        => options.UseSqlite("DataSource=app.db;Cache=Shared");
}