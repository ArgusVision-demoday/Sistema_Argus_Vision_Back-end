using ArgusVision.API.Interfaces;

namespace ArgusVision.API.Services
{
    public class PromptService : IPromptService
    {
        private readonly IWebHostEnvironment _environment;

        public PromptService(IWebHostEnvironment environment)
        {
            _environment = environment;
        }

        public async Task<string> GetSystemPromptAsync()
        {
            string path = Path.Combine(
                _environment.ContentRootPath,
                "Prompts",
                "ElionSystemPrompt.txt");

            return await File.ReadAllTextAsync(path);
        }
    }
}