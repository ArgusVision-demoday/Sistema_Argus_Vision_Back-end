namespace ArgusVision.API.Interfaces
{
    public interface IPromptService
    {
        Task<string> GetSystemPromptAsync();
    }
}