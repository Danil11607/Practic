using System;
using Telegram.Bot;

namespace Debug
{
    public class DebugMessage
    {
        public enum MessageLevel { Error, Debug, Info }
        public static void SendDebug(string ClientName, string Text, MessageLevel Level)
        {
            switch (Level)
            {
                case MessageLevel.Debug:
                    Console.ForegroundColor = ConsoleColor.DarkGreen;
                    break;
                case MessageLevel.Error:
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    break;
                default:
                    Console.ForegroundColor = ConsoleColor.White;
                    break;
            }
            Console.WriteLine($"[Time:{DateTime.Now}] [{ClientName}] [{Level.ToString()}]:{Text}");
        }
    }
}
