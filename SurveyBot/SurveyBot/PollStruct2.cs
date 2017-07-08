using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PollStruct
{
    class Poll
    {
        public enum pollType { anon, halfanon, nonanon};
        public pollType type;
        private int Count;
        public string token;
        public string password;
        public List<Question> Questions;

        //Poll(string token, int numberOfQuestions, string password)
        //{
        //    this.numberOfQuestions = numberOfQuestions;
        //    this.token = token;
        //    questionArr = new Question[numberOfQuestions];
        //    for (int i = 0; i < numberOfQuestions; i++)
        //    {
        //        Question questionTemp = new Question(/*parse*/);
        //        questionArr[i] = questionTemp;
        //    }

        //}
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
        public string Content;
        public List<Answer> Answers;

        //Question(string question, questionType type, int numberOfAnswers)
        //{
        //    this.question = question;
        //    this.type = type;
        //    this.numberOfAnswers = numberOfAnswers;
        //    answerArr = new Answer[numberOfAnswers];
        //    for (int i = 0; i < numberOfAnswers; i++)
        //    {
        //        Answer answerTemp = new Answer(/*parse*/);
        //        answerArr[i] = answerTemp;
        //    }
        //}
    }
}