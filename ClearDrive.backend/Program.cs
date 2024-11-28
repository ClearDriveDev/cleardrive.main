using ClearDrive.backend.Context;
using ClearDrive.backend.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();

builder.Services.ConfigureCors();
builder.Services.ConfigureInMemoryContext();
builder.Services.ConfigureRepos();
builder.WebHost.UseUrls("http://0.0.0.0:7090");



var app = builder.Build();

using (var scope = app.Services.CreateAsyncScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<CASInMemoryContext>();

    dbContext.Database.EnsureCreated();
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseHttpsRedirection();

app.UseCors("CASCors");

app.UseAuthorization();

app.MapControllers();

app.Run();
