using ArgusVision.API.Interfaces;
using ArgusVision.API.Models.Groq;

namespace ArgusVision.API.Services
{
    public class ConversationMemoryService
        : IConversationMemoryService
    {
        private readonly List<GroqMessage> _messages =
            new();

        public List<GroqMessage> GetMessages()
        {
            return _messages;
        }

        public void AddMessage(
            string role,
            string content)
        {
            _messages.Add(new GroqMessage
            {
                Role = role,
                Content = content
            });
        }

        public void ClearConversation()
        {
            _messages.Clear();
        }
    }
}