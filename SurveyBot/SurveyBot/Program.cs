using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using System.Threading;
using Debug;
using PollStruct;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

namespace SurveyBot
{
    class Program
    {
        static List<Session> Sesions = new List<Session>();
        static List<string> TokenContainer = new List<string>();
        static TelegramBotClient Bot = new TelegramBotClient("343759311:AAGwKYHxEX4kC2DvLlXpKovytJKCDq1Inwk");
        static void Main(string[] args)
        {
            TokenContainer.Add("abc");
            Bot.StartReceiving();
            Bot.OnMessage += MessageParse;
            Console.ReadKey(); 
        }

        private static void MessageParse(object sender, Telegram.Bot.Args.MessageEventArgs e)
        {
            var x = new Session(e.Message.Chat, null);
            var Sesion = x.ContatinSession(Sesions);
            if (Sesion!=null)
            {
                Sesions.Remove(Sesion);
                Sesion.Answers.Add(e.Message.Text);
                if (Sesion.Questions.Count == 0)
                {
                    return;
                }
                SendQuestion(Sesion.Questions, Sesion.Chat);
                Sesions.Add(Sesion);
                return;
            }
            string[] msg = e.Message.Text.Split(' ');
            switch (msg[0])
            {
                case "/join":
                    if (msg.Count() != 2)
                    {
                        DebugMessage.SendDebug("MainClient", "Wrong Arguments", DebugMessage.MessageLevel.Error);
                        Bot.SendTextMessageAsync(e.Message.Chat.Id, "Wrong Arguments");
                        break;
                    }
                    if (TokenContainer.Contains(msg[1]))
                        InitiateSesion(e.Message.Chat, msg[1]);
                    break;
            }
        }
        
        private static void InitiateSesion(Chat User, string token)
        {
            
            var x = new Poll();
            {
                var Q1 = new Question();
                Q1.Answers = new List<Answer>();
                Q1.Answers.Add(new Answer("gde"));
                Q1.Answers.Add(new Answer("moi"));
                Q1.Answers.Add(new Answer("parser"));
                Q1.Answers.Add(new Answer("cherti"));
                Q1.Answers.Add(new Answer("?"));
                Q1.Content = "Ya vas sprashivayu:";
                var Q2 = new Question();
                Q2.Content = "Parser rabotaet hrenovo kto nakosyachil?";
                Q2.Answers = new List<Answer>();
                Q2.Answers.Add(new Answer("tot kto sdelal"));
                Q2.Answers.Add(new Answer("cherti"));
                Q2.Answers.Add(new Answer("sozdatel'"));
                Q2.Answers.Add(new Answer("seva"));
                x.Questions = new List<Question>();
                x.Questions.Add(Q1);
                x.Questions.Add(Q2);
            }
            Queue<Question> Questions = new Queue<Question>();
            foreach (var question in x.Questions)
            {
                Questions.Enqueue(question);
                
            }
            var ses = new Session(User, Questions);
            SendQuestion(ses.Questions, ses.Chat);
            Sesions.Add(ses);
        }
        
        private static int SendQuestion(Queue<Question> Questions, Chat chat)
        {
            List<KeyboardButton> KButtons = new List<KeyboardButton>();
            if (Questions.Count == 0)
                return 0;
            var question = Questions.Dequeue();
            foreach (var answer in question.Answers)
                KButtons.Add(new KeyboardButton(answer.Content));
            Bot.SendTextMessageAsync(chat.Id, question.Content, ParseMode.Default, false, false, 0, new ReplyKeyboardMarkup(KButtons.ToArray(), true, true));
            return 1;
        }

    }
    class Session
    {
        public List<string> Answers;
        public readonly Chat Chat;
        public Queue<Question> Questions;

        public Session(Chat user, Queue<Question> questions)
        {
            Answers = new List<string>();
            Chat = user;
            if (questions == null)
            { Questions = new Queue<Question>(); return; }
            Questions = new Queue<Question>(questions);
            
        }

        public Session ContatinSession(List<Session> x)
        {
            foreach (var item in x)
                if (item.Chat.Username == this.Chat.Username)
                {
                    return item;
                }
            return null;
        }
    }
}
