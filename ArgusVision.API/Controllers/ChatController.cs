using Microsoft.AspNetCore.Mvc;
using ArgusVision.API.DTOs;
using ArgusVision.API.Interfaces;

namespace ArgusVision.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ChatController : ControllerBase
    {
        private readonly IGroqService _groqService;

        public ChatController(IGroqService groqService)
        {
            _groqService = groqService;
        }

        [HttpPost]
        public async Task<ActionResult<ChatResponse>> EnviarMensagem(ChatRequest request)
        {
            string respostaIA = await _groqService.SendMessageAsync(request.Mensagem);

            ChatResponse response = new ChatResponse
            {
                Resposta = respostaIA
            };

            return Ok(response);
        }
    }
}
