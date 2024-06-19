using Application;
using Application.Filters;
using Core;
using Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllersWithViews(options =>
{
    options.Filters.Add<GlobalActionAsyncFilter>();
    options.Filters.Add<ExceptionFilter>();
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddContext(builder.Configuration);
builder.Services.AddAppAuthorizations(builder.Configuration);
builder.Services.AddAppServices();
builder.Services.AddRepositories();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();
app.UseCors();
await app.MigrateDatabase(builder.Services);

app.Run();
