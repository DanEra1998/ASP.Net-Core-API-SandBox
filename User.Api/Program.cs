// Import Entity Framework Core
using Microsoft.EntityFrameworkCore;
// Import our Data folder so we can reference AppDbContext
using User.Api.Data;

// Creates the "builder" — this is where you CONFIGURE your app
// args = command line arguments passed when running the app
var builder = WebApplication.CreateBuilder(args);

// Registers controllers so the app knows to look for your Controllers/ folder
builder.Services.AddControllers();

// Enables API endpoint discovery — needed for Swagger to work
builder.Services.AddEndpointsApiExplorer();

// Registers Swagger — this generates the UI at /swagger for testing your API
builder.Services.AddSwaggerGen();

// Registers AppDbContext as a service with EF Core
// This is what makes _db available in your controllers via dependency injection
// GetConnectionString("DefaultConnection") looks for "DefaultConnection" in your config
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

// Finalizes all the configuration above and creates the actual app
var app = builder.Build();

// Only enable Swagger in Development mode, not in production
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(); // serves the UI at /swagger
}

// Enables authorization middleware (needed even if not using auth yet)
app.UseAuthorization();

// Tells the app to route incoming HTTP requests to your controllers
app.MapControllers();

// Starts the app and begins listening for HTTP requests
app.Run();