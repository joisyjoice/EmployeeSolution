using EmployeeSolution.Helpers;
using EmployeeSolution.Services;
using static System.Net.WebRequestMethods;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// add services to DI container
{
    var services = builder.Services;
    services.AddCors();
    services.AddControllers();
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();

    // configure strongly typed settings object
    services.Configure<AppSettings>(builder.Configuration.GetSection("AppSettings"));

    // configure DI for application services
    services.AddScoped<IEmployeeService, EmployeeService>();
}

//builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
//configure HTTP request pipeline
{
    // global cors policy
    app.UseCors(x => x
        .AllowAnyOrigin()
        .AllowAnyMethod()
        .AllowAnyHeader());

// custom jwt auth middleware
  app.UseMiddleware<JwtMiddleware>();
    app.UseHttpsRedirection();

    app.UseAuthorization();
    app.MapControllers();
}



//app.MapControllers();
//app.Run("https://localhost:44386");
app.Run();
