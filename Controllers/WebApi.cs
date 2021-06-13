using System.Text;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.IO;
using Newtonsoft.Json;

namespace logtg.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WebApi : ControllerBase
    {
        private TelegramService Telegram;
        public WebApi(TelegramService tg)
        { 
            Telegram = tg;
        }
        [HttpPost]
        [Route("receive")]
        public IActionResult ReceiveFromWaapi([FromBody] dynamic str)
        {
            var json = JsonConvert.DeserializeObject(str.ToString());
            Console.WriteLine($"[{json.app}] {json.chatId}: {json.text}");
            Telegram.SendMessage(chatId: json.chatId.ToString(), text: json.text.ToString(), app: json.app.ToString());
            return Ok();
        }
    }
}
