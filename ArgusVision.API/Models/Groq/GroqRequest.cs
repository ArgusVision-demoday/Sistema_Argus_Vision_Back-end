using System.Text.Json.Serialization;

namespace ArgusVision.API.Models.Groq
{
    public class GroqRequest
    {
        [JsonPropertyName("model")]
        public string Model { get; set; } = string.Empty;

        [JsonPropertyName("messages")]
        public List<GroqMessage> Messages { get; set; } = new();
    }
}