using BookCURD.Data;
using BookCURD.Model;
using BookCURD.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// 1. Add services to the container, including DbContext and custom services

// Add DbContext for Entity Framework Core
builder.Services.AddDbContext<BookContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("Connection")));

// Add custom authentication service (AuthService)
builder.Services.AddScoped<AuthService>();

// Add CORS policy to allow requests from React App (localhost:3000)
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllReactApps",
        policy => policy
            .WithOrigins("http://localhost:3000") // Allow the React frontend app
            .AllowAnyMethod()                     // Allow all HTTP methods (GET, POST, etc.)
            .AllowAnyHeader()                     // Allow all headers (Authorization, Content-Type, etc.)
            .AllowCredentials()                   // Allow cookies or credentials if needed
    );
});

// Add controllers
builder.Services.AddControllers();

// Add Swagger services (API documentation)
builder.Services.AddEndpointsApiExplorer(); // Needed for minimal API support
builder.Services.AddSwaggerGen();           // Registers Swagger generator for the app

var app = builder.Build();

// 2. Configure middleware pipeline

// Apply CORS middleware
app.UseCors("AllowAllReactApps");

// Only use Swagger in the development environment
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Ensure HTTP requests are routed to controllers
app.UseHttpsRedirection(); // Ensure HTTPS is used (optional, but recommended)
app.UseRouting();          // Enable routing

app.UseAuthentication();   // Enable authentication middleware (if applicable)
app.UseAuthorization();    // Enable authorization middleware

// 3. Seed initial data (Admin and User)
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<BookContext>();

    // Apply any pending migrations (if applicable)
    context.Database.Migrate(); // Ensure the database is created and any migrations are applied

    // Seed default users if none exist in the database
    if (!context.Users.Any())
    {
        var authService = scope.ServiceProvider.GetRequiredService<AuthService>();

        // Add initial admin and user
        context.Users.AddRange(new User
        {
            Username = "Ankit",
            Email = "ankit@admin.com",
            PasswordHash = authService.HashPassword("Ankit@123"), // Hashed password for admin
            Role = "Admin"
        }, new User
        {
            Username = "testuser",
            Email = "user@user.com",
            PasswordHash = authService.HashPassword("User@123"), // Hashed password for regular user
            Role = "User"
        });

        context.SaveChanges(); // Save seed data to the database
    }
}

// 4. Configure the endpoint routing for controllers
app.MapControllers(); // This ensures that the controllers are mapped and accessible

// 5. Run the application
app.Run(); // Start the app and listen for HTTP requests
