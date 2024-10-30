using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using UserDataManagementAPI;
using UserDataManagementAPI.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var _config = builder.Configuration;

var connection = _config.GetConnectionString("dev");

builder.Services.AddDbContext<DboUsersContext>(options => options.UseMySql(connection, ServerVersion.AutoDetect(connection)));

builder.Services.AddControllers()
    .AddJsonOptions(opt =>
    {
        opt.JsonSerializerOptions.Converters.Add(new DateOnlyJsonConverter());
    });
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(opt => opt.MapType<DateOnly>(() => new OpenApiSchema
    {
        Type = "string",
        Format = "date",
        Example = new OpenApiString(DateTime.Today.ToString("yyyy-MM-dd"))
    })
); ;

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
