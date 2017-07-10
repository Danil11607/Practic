using System.Collections.Generic;

namespace PollStruct
{
    class Poll
    {
        public enum pollType { anon, halfanon, nonanon};
        public pollType type;
        private int count;
        public string token;
        public string password;
        public List<Question> Questions;

        public Poll(string token, int count, string password, pollType type)
        {
            this.count = count;
            this.token = token;
            this.type = type;
            List<Question> questions = new List<Question>();
        }
    }

    class Answer
    {
        public string Content;
        public Question skipTo;// убрать все пропуски

        public Answer(string answer, Question skipTo = null)
        {
            this.Content = answer;
            this.skipTo = skipTo;// убрать все пропуски
        }
    }

    class Question
    {
        public enum questionType { test, testMulti, testSkip, question };
        public questionType type;
        public string content;
        public int count;
        public List<Answer> Answers;

        public Question(string content, questionType type, int count)
        {
            this.content = content;
            this.type = type;
            this.count = count;
            List<Answer> Answers = new List<Answer>();
        }
    }
    class Program
    {
        public void Main()
        {
            PollReader pr = new PollReader();
            Poll poll = pr.ReadPoll();
        }
    }
}