using IsApi.Extensions;
using IsApi.Persistence;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCongigurationSettings(builder.Configuration);
var app = builder.Build();
	
app.UsePathBase("/iserver");
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.MapGet("/" , async (context) => await context.Response.WriteAsync("Welcome to identity server"));
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.AddAppConfiguration();
app.Run();
