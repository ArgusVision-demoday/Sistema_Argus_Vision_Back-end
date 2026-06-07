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

        public GroqService(
            IHttpClientFactory httpClientFactory,
            IOptions<GroqSettings> groqOptions)
        {
            _httpClient = httpClientFactory.CreateClient();
            _groqSettings = groqOptions.Value;
        }

        public async Task<string> SendMessageAsync(string message)
        {
            var request = new GroqRequest
            {
                Model = _groqSettings.Model,
                Messages = new List<GroqMessage>
                {
                    new()
                    {
                        Role = "user",
                        Content = message
                    }
                }
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

            return groqResponse?
                .Choices?
                .FirstOrDefault()?
                .Message?
                .Content
                ?? "Nenhuma resposta recebida.";
        }
    }
}