using ArgusVision.API.Configuration;
using ArgusVision.API.Interfaces;
using ArgusVision.API.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<GroqSettings>(
    builder.Configuration.GetSection("Groq"));

builder.Services.AddScoped<IGroqService, GroqService>();

builder.Services.AddScoped<IPromptService, PromptService>();

builder.Services.AddSingleton<IConversationMemoryService,
                              ConversationMemoryService>();

builder.Services.AddHttpClient();

// Serviços
builder.Services.AddControllers();

// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddPolicy("FrontendPolicy",
        policy =>
        {
            policy.AllowAnyOrigin()
                  .AllowAnyHeader()
                  .AllowAnyMethod();
        });
});

var app = builder.Build();

// Swagger
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("FrontendPolicy");

app.UseAuthorization();

app.MapControllers();

app.Run();