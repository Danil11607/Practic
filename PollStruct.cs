using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Iter1
{
    class Poll
    {
        int numberOfQuestions;
        string tag;
        bool asked = false;

        Poll(string tag, int numberOfQuestions)
        {
            this.numberOfQuestions = numberOfQuestions;
            this.tag = tag;
            Question[] questionArr = new Question[numberOfQuestions];
            for (int i = 0; i < numberOfQuestions; i++)
            {
                Question questionTemp = new Question(/*parse*/);
                questionArr[i] = questionTemp;
            }

        }
    }

    class Answer
    {
        enum answerType { single, multi, textSingle, textMulti };
        string answer;
        answerType type = new answerType();
        int skipTo;

        Answer(string answer, answerType type, int skipTo = 0)
        {
            this.answer = answer;
            this.type = type;
            this.skipTo = skipTo;
        }
    }

    class Question
    {
        string question;
        int numberOfAnswers;
        bool asked = false;

        Question(string question, int numberOfAnswers)
        {
            this.question = question;
            this.numberOfAnswers = numberOfAnswers;
            Answer[] answerArr = new Answer[numberOfAnswers];
            for (int i = 0; i < numberOfAnswers; i++)
            {
                Answer answerTemp = new Answer(/*parse*/);
                answerArr[i] = answerTemp;
            }
        }
    }
}
