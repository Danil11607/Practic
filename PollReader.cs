using System;
using System.IO;
using static PollStruct.Poll;
using static PollStruct.Question;

namespace PollStruct
{
    class PollReader
    {
        static StreamReader sr = new StreamReader("Test.txt");
        public string text;
        public Poll ReadPoll()
        {
            while (!text.Contains("Type"))
                text = sr.ReadLine();
            string[] tempText = text.Split('=');
            int type = Convert.ToInt32(tempText[1].Trim());
            pollType enmType = (pollType)type;

            while (!text.Contains("Pass"))
                text = sr.ReadLine();
            tempText = text.Split('=');
            string password = tempText[2].Trim();

            while (!text.Contains("QCount"))
                text = sr.ReadLine();
            tempText = text.Split('=');
            int numberOfQuestions = Convert.ToInt32(tempText[1].Trim());

            Poll poll = new Poll("Test", numberOfQuestions, password, enmType );


            int i = 1;
            while (!sr.EndOfStream)
            {
                text = sr.ReadLine();
                if (text.StartsWith($"Q{i}"))
                {
                    string questionContent = text.Substring(text.IndexOf(':') + 1);

                    while (!text.StartsWith("ACount"))
                        text = sr.ReadLine();
                    int aCount = Convert.ToInt32(text.Substring(text.IndexOf('=') + 1));

                    while (!text.StartsWith("QType"))
                        text = sr.ReadLine();
                    string qType1 = text.Substring(text.IndexOf('=') + 1);
                    questionType qType = (questionType)Enum.Parse(typeof(questionType), qType1);

                    Question question = new Question(questionContent, qType, aCount);
                    poll.Questions.Add(question);

                    int tempSkipTo = -1;

                    int j = 1;
                    while (!text.StartsWith("}"))
                    {
                        text = sr.ReadLine();
                        if (text.StartsWith($"A{j}"))
                        {
                            if (text.Contains($"A{j}->"))
                            {
                                string[] tempStrings = text.Split(':');
                                tempSkipTo = Convert.ToInt32(tempStrings[0].Substring(text.IndexOf('>')).Trim()); 
                            }

                            string tempAnswer = text.Substring(text.IndexOf(':')).Trim();
                            Answer answer;
                            if (tempSkipTo != -1)
                                answer = new Answer(tempAnswer, tempSkipTo);
                            else
                                answer = new Answer(tempAnswer);

                            poll.Questions[i].Answers.Add(answer);
                            j++;
                        }

                    }
                    j = 1;
                    i++;
                }
            }
            return poll;
        }
    }
}