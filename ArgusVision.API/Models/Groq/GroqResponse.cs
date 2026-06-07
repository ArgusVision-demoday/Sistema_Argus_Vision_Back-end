using System.Text.Json.Serialization;

namespace ArgusVision.API.Models.Groq
{
    public class GroqResponse
    {
        [JsonPropertyName("choices")]
        public List<Choice> Choices { get; set; } = new();
    }

    public class Choice
    {
        [JsonPropertyName("message")]
        public GroqMessage Message { get; set; } = new();
    }
}