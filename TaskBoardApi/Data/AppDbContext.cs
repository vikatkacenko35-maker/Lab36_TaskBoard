using System.ComponentModel;
using System.Security.Cryptography.X509Certificates;
using Microsoft.EntityFrameworkCore;
using TaskBoardApi.Data;
using TaskBoardApi.Models;

namespace TaskBoardApi.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }
    public DbSet<TaskItem> Tasks { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<TaskItem>().HasData(
new TaskItem
{
    Id = 1,
    Title = "Изучить Asp",
    Description = "ляляляля",
    IsCompleted = true,
    CreatedAt = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc)
},
new TaskItem
{
    Id = 2,
    Title = "Изучить SqLite",
    Description = "ляляляля",
    IsCompleted = true,
    CreatedAt = new DateTime(2026, 1, 2, 0, 0, 0, DateTimeKind.Utc)
},
new TaskItem
{
    Id = 3,
    Title = "Изучить frontend",
    Description = "ляляляля",
    IsCompleted = false,
    CreatedAt = new DateTime(2026, 1, 3, 0, 0, 0, DateTimeKind.Utc)
},
new TaskItem
{
    Id = 4,
    Title = "Сдать итоговый проект",
    Description = "ляляляля",
    IsCompleted = false,
    CreatedAt = new DateTime(2026, 1, 4, 0, 0, 0, DateTimeKind.Utc)
}
        );
    }
}
