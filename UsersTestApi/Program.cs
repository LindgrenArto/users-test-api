using UserTestApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

builder.Services.Configure<BookStoreDatabaseSettings>(
    builder.Configuration.GetSection("MongoDbSettings"));

builder.Services.AddScoped<IUserRepository, UserRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();  // Detailed error page in development
}

app.UseHttpsRedirection();

app.UseRouting();  // Explicit routing middleware

app.MapControllers();  // Map controller routes

app.Run();