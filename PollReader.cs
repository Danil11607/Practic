using System;
using System.IO;
using PollStruct;
using Newtonsoft.Json;

namespace PollStruct
{
    class PollReader
    {
        public Poll ReadPoll()
        {
            StreamReader sr = new StreamReader("test.txt");
            string text = "";
            while (!text.Contains("Type"))
                text = sr.ReadLine();
            string[] tempText = text.Split('=');
            int type = Convert.ToInt32(tempText[1].Trim());
            Poll.pollType enmType = (Poll.pollType)type;

            while (!text.Contains("Pass"))
                text = sr.ReadLine();
            tempText = text.Split('=');
            string password = tempText[1].Trim();
            text = "";
            while (!text.Contains("QCount"))
                text = sr.ReadLine();
            tempText = text.Split('=');
            int numberOfQuestions = Convert.ToInt32(tempText[1].Trim());

            Poll poll = new Poll("test", "abc", numberOfQuestions);


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
                    Question.questionType qType = (Question.questionType)Enum.Parse(typeof(Question.questionType), qType1);

                    Question question = new Question(content: questionContent, type: qType, count: aCount, pollId: poll.Id);
                    poll.Questions.Add(question);

                    int j = 1;
                    while (!text.StartsWith("}"))
                    {
                        text = sr.ReadLine();
                        if (text.StartsWith($"A{j}"))
                        {
                            string tempAnswer = text.Substring(text.IndexOf(':') + 1).Trim();
                            Answer answer = new Answer(content: tempAnswer, questionId: question.Id);
                            poll.Questions[i - 1].Answers.Add(answer);
                            j++;
                        }

                    }
                    j = 1;
                    i++;
                }
            }
            //foreach (var it in poll.Questions)
            //    Console.WriteLine(it);
            //Console.WriteLine(JsonConvert.SerializeObject(poll));
            sr.Close();
            return poll;
        }

        public Poll ReadJsonPoll()
        {
            StreamReader sr = new StreamReader("JsonTest.txt");
            string allText = sr.ReadToEnd();
            Poll poll = JsonConvert.DeserializeObject<Poll>(allText);

            return poll;
        }
    }
}