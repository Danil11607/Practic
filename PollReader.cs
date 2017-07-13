using System;
using System.IO;
using PollStruct;
using Newtonsoft.Json;

namespace PollReader
{
    class PollReader
    {
        public Poll ReadPollTxt(string name) //тут переделаешь ага
        {
            StreamReader sr = new StreamReader(name + ".txt"); //тут тоже
            string text = "";
            while (!text.Contains("Type"))
                text = sr.ReadLine();
            Poll.PollType enmType = (Poll.PollType)type;
            string pType1 = text.Substring(text.IndexOf('=') + 1);
            Poll.PollType pType = (Poll.PollType)Enum.Parse(typeof(Poll.PollType), pType1);

            while (!text.Contains("Pass"))
                text = sr.ReadLine();
            tempText = text.Split('=');
            string password = tempText[1].Trim();
			text = "";
            while (!text.Contains("QCount"))
                text = sr.ReadLine();
            tempText = text.Split('=');
            int numberOfQuestions = Convert.ToInt32(tempText[1].Trim());

            Poll poll = new Poll(password: "abc", count: numberOfQuestions);

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
                    Question.QuestionType qType = (Question.QuestionType)Enum.Parse(typeof(Question.QuestionType), qType1);

                    Question question = new Question(content: questionContent, type: qType, count: aCount, pollId:poll.Id, pollParent: poll);
                    poll.Questions.Add(question);

                    int j = 1;
                    while (!text.StartsWith("}"))
                    {
                        text = sr.ReadLine();
                        if (text.StartsWith($"A{j}"))
                        {
                            string tempAnswer = text.Substring(text.IndexOf(':') + 1).Trim();
                            Answer answer = new Answer(content: tempAnswer, questionId: question.Id, questionParent: question, questionId: question.Id);
                            poll.Questions[i-1].Answers.Add(answer);
                            j++;
                        }
                    }
                    j = 1;
                    i++;
                }
            }
            sr.Close();
            return poll;
        }

        public Poll ReadPollJson()
        {
            StreamReader sr = new StreamReader("JsonTest.txt"); //и вот тут
            Poll poll = JsonConvert.DeserializeObject<Poll>(allText);
            return poll;
        }
    }
}