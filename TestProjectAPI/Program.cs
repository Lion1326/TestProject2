using Microsoft.EntityFrameworkCore;
using TestProject.Infrastructure;
using TestProject.Services;

var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
                      policy =>
                      {
                          policy.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin();

                      });
});
ConfigurationManager configuration = builder.Configuration;

builder.Services.AddMemoryCache();

builder.Services.AddInfrastructure(configuration);

builder.Services.AddApp();


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();




var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
