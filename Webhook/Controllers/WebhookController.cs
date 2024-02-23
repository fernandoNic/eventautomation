using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using Webhook.Model;
using Webhook.Services;

namespace Webhook.Controllers
{
    [ApiController]
    [Route("api/demo/[controller]")]
    public class WebhookController : ControllerBase
    {
        private readonly ILogger<WebhookController> _logger;
        private readonly KafkaService _kafkaService;

        public WebhookController(ILogger<WebhookController> logger, KafkaService kafkaService)
        {
            _logger = logger;
            _kafkaService = kafkaService;
        }

        [HttpPost(Name = "PublishMessage")]
        public ActionResult PostMessage([FromBody] Evento request)
        {
            var message = JsonSerializer.Serialize(request);

            _kafkaService.publish(message);
            return Ok();
        }
    }
}