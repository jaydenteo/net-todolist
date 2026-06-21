// IMPORTS
using backend.Data;
using backend.Services;

// APP SETUP
var builder = WebApplication.CreateBuilder(args);

// Register controllers (enables [ApiController], routing, model binding, etc.)
builder.Services.AddControllers();

// Enables Swagger/OpenAPI ednpoint discovery
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddOpenApi();

// DEPENDENCY INJECTION (DI)
// .NET will automatically create and provide these classes where needed

// Repository (DATA LAYER)
// Singleton = one instance for entire app lifetime
// Good for in-memory storage (shared state across requests)
builder.Services.AddSingleton<ITodoRepository, InMemoryTodoRepository>();

// Service (BUSINESS LAYER)
// Scoped = new instance per HTTP request
// Keeps business logic isolated from controllers and data storage
builder.Services.AddScoped<ITodoService, TodoService>();

// Health check endpoint support (/health)
builder.Services.AddHealthChecks();

// BUILD APP (finalizes configuration)
var app = builder.Build();

// Enable OpenAPI only in development
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

// Middleware pipeline (runs in order)

app.UseHttpsRedirection();
app.MapControllers();
app.MapHealthChecks("/health");

app.Run();
