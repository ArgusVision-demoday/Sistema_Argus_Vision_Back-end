namespace ArgusVision.API.Interfaces
{
    public interface IGroqService
    {
        Task<string> SendMessageAsync(string message);
    }
}