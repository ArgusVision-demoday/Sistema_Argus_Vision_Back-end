using Microsoft.AspNetCore.Mvc;
using ArgusVision.API.DTOs;

namespace ArgusVision.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ChatController : ControllerBase
    {
        [HttpPost]
        public ActionResult<ChatResponse> EnviarMensagem(ChatRequest request)
        {
            ChatResponse response = new ChatResponse
            {
                Resposta = $"Você disse: {request.Mensagem}"
            };

            return Ok(response);
        }
    }
}
