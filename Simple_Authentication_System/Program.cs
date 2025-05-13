using Microsoft.Extensions.Configuration;
using Simple_Authentication_System_Api;
using Simple_Authentication_System_Api.Middleware;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddServices(builder.Configuration);
builder.Services.AddJwtAuthentication(builder.Configuration);

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddSwaggerServices();

int isSwaggerEnabled = builder.Configuration.GetValue<int>("PUBLISH_API_SWAGGER");


var app = builder.Build();

// Configure the HTTP request pipeline.
if (isSwaggerEnabled == 1)
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseMiddleware<ErrorHandlingMiddleware>();

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
