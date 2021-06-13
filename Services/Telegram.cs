using System;
using System.IO;
using System.Text;
using Telegram.Bot;
using Telegram.Bot.Args;

namespace logtg
{
    public class TelegramService
    {
        private static ITelegramBotClient botClient;
        public void ConfigureService(string token)
        {
            botClient = new TelegramBotClient(token);
            botClient.OnMessage += botclient_onmessage;
            botClient.StartReceiving();
        }
        public void SendMessage(string chatId, string text, string app)
        {
            var date = DateTime.Now.ToString("dd.MM.yyyy HH:mm:ss");
            if(text.Length > 2500) {
                botClient.SendTextMessageAsync(chatId, $"{date}\n\r⛔️Переполнение⛔️ [2000 из {text.Length}]\n\r{text.Substring(0, 2000)}");
                botClient.SendDocumentAsync(chatId, new Telegram.Bot.Types.InputFiles.InputOnlineFile(new MemoryStream(Encoding.UTF8.GetBytes(text)), "full.txt"));
            }
            else
            {
                botClient.SendTextMessageAsync(chatId, $"[{date}, {app}]\n\r{text}");
            }
        }
        private void botclient_onmessage(object sender, MessageEventArgs e)
        {
            botClient.SendTextMessageAsync(e.Message.Chat.Id, $"Ваш chatId:{e.Message.Chat.Id}");
        }

    }
}