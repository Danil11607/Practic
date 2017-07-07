using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using System.Threading;
using Debug;

namespace SurveyBot
{
    class Program
    {
        static TelegramBotClient Bot = new TelegramBotClient("343759311:AAGwKYHxEX4kC2DvLlXpKovytJKCDq1Inwk");
        static void Main(string[] args)
        {
            Bot.StartReceiving();
            Bot.OnMessage += MessageParse;
            Console.ReadKey(); 
        }

        private static void MessageParse(object sender, Telegram.Bot.Args.MessageEventArgs e)
        {
            string[] msg = e.Message.Text.Split(' ');
            switch (msg[0])
            {
                case "/join":
                    if (msg.Count() != 2)
                    {
                        DebugMessage.SendDebug("MainClient", "Wrong Arguments", DebugMessage.MessageLevel.Error);
                        Bot.SendTextMessageAsync(e.Message.Chat.Id, "Wrong Arguments");
                    }
                    break;
            }
        }
    }
}
