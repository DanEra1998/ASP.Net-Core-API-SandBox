// Import Entity Framework Core — this gives us DbContext, DbSet, etc.
using Microsoft.EntityFrameworkCore;

// Import our Models folder so we can reference UserEntity
using User.Api.Models;

// This class lives in the User.Api.Data namespace
namespace User.Api.Data;

// AppDbContext inherits from DbContext (provided by EF Core)
// DbContext is the core class that manages the database connection
// and lets you query and save data. Think of it as the "database manager"
public class AppDbContext : DbContext
{
    // This is the CONSTRUCTOR — it runs when AppDbContext is first created
    // DbContextOptions contains config like the connection string (from appsettings.json)
    // ": base(options)" passes those options UP to the parent DbContext class
    // EF Core needs this to know HOW to connect to your Postgres database
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    // DbSet represents a TABLE in your database
    // DbSet<UserEntity> = the "Users" table, where each row is a UserEntity object
    // "Users" is the property name = what you'll call it in code e.g. _db.Users
    // "=> Set<UserEntity>()" is EF Core's way of saying "give me that table"
    public DbSet<UserEntity> Users => Set<UserEntity>();
}