using ArgusVision.API.Configuration;
using ArgusVision.API.Interfaces;
using ArgusVision.API.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<GroqSettings>(
    builder.Configuration.GetSection("Groq"));

builder.Services.AddScoped<IGroqService, GroqService>();

// Serviços
builder.Services.AddControllers();

// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Swagger
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();