using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using Microsoft.Extensions.Options;
using ArgusVision.API.Configuration;
using ArgusVision.API.Interfaces;
using ArgusVision.API.Models.Groq;

namespace ArgusVision.API.Services
{
    public class GroqService : IGroqService
    {
        private readonly HttpClient _httpClient;
        private readonly GroqSettings _groqSettings;
        private readonly IPromptService _promptService;
        private readonly IConversationMemoryService _memoryService;

        public GroqService(
            IHttpClientFactory httpClientFactory,
            IOptions<GroqSettings> groqOptions,
            IPromptService promptService,
            IConversationMemoryService memoryService)
        {
            _httpClient = httpClientFactory.CreateClient();
            _groqSettings = groqOptions.Value;
            _promptService = promptService;
            _memoryService = memoryService;
        }

        public async Task<string> SendMessageAsync(string message)
        {

            string systemPrompt =
                await _promptService.GetSystemPromptAsync();

            var messages = new List<GroqMessage>();

            messages.Add(new GroqMessage
            {
                Role = "system",
                Content = systemPrompt
            });

            messages.AddRange(_memoryService.GetMessages());

            messages.Add(new GroqMessage
            {
                Role = "user",
                Content = message
            });

            var request = new GroqRequest
            {
                Model = _groqSettings.Model,
                Messages = messages
            };

            string json = JsonSerializer.Serialize(request);

            var content = new StringContent(
                json,
                Encoding.UTF8,
                "application/json");

            _httpClient.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue(
                    "Bearer",
                    _groqSettings.ApiKey);

            _memoryService.AddMessage(
                "user",
                message);

            HttpResponseMessage response =
                await _httpClient.PostAsync(
                    "https://api.groq.com/openai/v1/chat/completions",
                    content);

            //response.EnsureSuccessStatusCode();

            if (!response.IsSuccessStatusCode)
            {
                string erro = await response.Content.ReadAsStringAsync();

                throw new Exception(
                    $"Erro Groq: {response.StatusCode}\n{erro}");
            }

            string responseJson =
                await response.Content.ReadAsStringAsync();

            GroqResponse? groqResponse =
                JsonSerializer.Deserialize<GroqResponse>(
                    responseJson,
                    new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    });

            string resposta = groqResponse?
                .Choices?
                .FirstOrDefault()?
                .Message?
                .Content
                ?? "Nenhuma resposta recebida.";

            _memoryService.AddMessage(
                "assistant",
                resposta);

            return resposta;
        }
    }
}