using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Iter1
{
    class Poll
    {
        int Count;
        string pass;
        List<Question> Questions;
        
    }

    class Answer
    {
        string Content;
        //Question skipTo;
        
    }

    class Question
    {
        string Content;
        int Count;
        List<Answer> Answers;
    }
}
