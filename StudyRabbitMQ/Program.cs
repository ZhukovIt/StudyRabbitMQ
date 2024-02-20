using StudyRabbitMQ;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddSingleton<UserRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseRouting();
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
    endpoints.MapGet("/", (HttpContext ctx) => ctx.Response.WriteAsync("Server is launch!"));
});

app.Run();
