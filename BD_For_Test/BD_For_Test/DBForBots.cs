using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PollStruct;


using System.Data.Entity;

namespace BD_For_Test
{
	class CreateBDForQuestions
	{
		Poll pl;
		class UserContext : DbContext
		{
			public UserContext()
				: base("DbConnection")
			{ }

			public DbSet<Node> DBQuestions { get; set; }
		}
		public CreateBDForQuestions(Poll pl)
		{
			this.pl = pl;
		}

		public void create()
		{
			Question que;
			List<PollStruct.Answer> answer = new List<Answer>();
			using (UserContext D = new UserContext())
			{

				for (int i = 0; i < pl.Questions.Count; i++)
				{
					que = pl.Questions[i];
					for (int j = 0; j < pl.Questions[i].Answers.Count; j++)
					{
						answer.Add(pl.Questions[i].Answers[j]);

					}
					Node nd = new Node { que = que, Answers = answer };
					D.DBQuestions.Add(nd);
					D.SaveChanges();
					answer = new List<Answer>();
				}
			}
		}
 }
	
		class Node
	{
		public int Id { get; set; }
		public Question que { get; set; }
		public List<PollStruct.Answer> Answers { get; set; }
	}
	
	
	class DBForBots
	{
		static void Main(string[] args)
		{
			PollReader pr = new PollReader();
			Poll poll = pr.ReadPoll();
			CreateBDForQuestions BD = new CreateBDForQuestions(poll);
			BD.create();

		}
	}
}


