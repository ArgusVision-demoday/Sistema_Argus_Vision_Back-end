using ArgusVision.API.Interfaces;

namespace ArgusVision.API.Services
{
    public class GroqService : IGroqService
    {
        public Task<string> SendMessageAsync(string message)
        {
            return Task.FromResult($"Elion recebeu: {message}");
        }
    }
}