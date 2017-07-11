using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Struct911;

namespace ConsoleApp1
{
    class Poll
    {
        public int Id { get; set; }
        public enum pollType { anon, halfanon, nonanon };
        public pollType Type { get; set; }
        public bool TrueAnon { get; set; }
        public int Count { get; set; }
        public string Token { get; set; }
        public string Password { get; set; }
        public List<Question> Questions { get; set; }

        public Poll() { List<Question> Questions = new List<Question>(); }

        public Poll(string token = null, string password = null, int count = 0, pollType type = default(pollType), bool trueAnon = false)
        {
            this.Token = token;
            this.Password = password;
            this.Count = count;
            this.Type = type;
            this.TrueAnon = trueAnon;
            Questions = new List<Question>();
        }
    }
    
    class Question
    {
        public enum questionType { test, testMulti, testSkip, question };
        public questionType Type { get; set; }
        public List<Answer> Answers { get; set; }
        public int Id { get; set; }
        public string Content { get; set; }
        public int Count { get; set; }
        public string Image { get; set; }

        public int? PollId { get; set; }
        public Poll PollParent { get; set; }

        public Question() { List<Answer> Answers = new List<Answer>(); }

        public Question(string content = null, questionType type = default(questionType), int count = 0,
            string image = null, Poll pollParent = null, int pollId = 0)
        {
            this.Content = content;
            this.Type = type;
            this.Count = count;
            this.Image = image;
            this.PollParent = pollParent;
            this.PollId = pollId;
            Answers = new List<Answer>();
        }
    }

    class Answer
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public int SkipTo { get; set; }

        public int? QuestionId { get; set; }
        public Question QuestionParent { get; set; }

        public Answer() { }

        public Answer(string content = null, int skipTo = -1, Question questionParent = null, int questionId = 0)
        {
            this.Content = content;
            this.SkipTo = skipTo;
            this.QuestionParent = questionParent;
            this.QuestionId = questionId;
        }
    }

    class PushData
    {
        public int Id { get; set; }
        public string Token { get; set; }
        public int UserId { get; set; }
        public int AnswerId { get; set; }
        public string Text { get; set; } = null;
    }

    class Context : DbContext
    {
        public Context() : base("DefaultConnection")
        {
            Database.SetInitializer<Context>(new MyContextInitializer());
        }

        public DbSet<Poll> Polls { get; set; }

        //public DbSet<Question> Questions { get; set; }

        //public DbSet<Answer> Answers { get; set; }

        public DbSet<PushData> PushDatas { get; set; }
    }

    class MyContextInitializer : CreateDatabaseIfNotExists<Context>
    {
        protected override void Seed(Context db)
        {
            //db.Polls.Add(new Poll("token", "pass", 3, Poll.pollType.anon, false));

            //db.SaveChanges();
        }
    }

    class Add
    {
        Poll OurPoll;
        public Add(Poll OurPoll)
        {
            this.OurPoll = OurPoll;
        }

        public void AddResultsToDb(int UserId, int AnswerId, string Text)
        {
            var pushData = new PushData { UserId = UserId, Token = OurPoll.Token, AnswerId = AnswerId, Text = Text };
            using (Context db = new Context())
            {
                db.PushDatas.Add(pushData);
                db.SaveChanges();
            }
        }

        public void AddPollsToDb()
        {
            using (Context db = new Context())
            {
                db.Polls.Add(OurPoll);
                db.SaveChanges();
            }
        }
    }
}
