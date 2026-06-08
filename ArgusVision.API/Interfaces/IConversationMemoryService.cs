using ArgusVision.API.Models.Groq;

namespace ArgusVision.API.Interfaces
{
    public interface IConversationMemoryService
    {
        List<GroqMessage> GetMessages();

        void AddMessage(
            string role,
            string content);

        void ClearConversation();
    }
}