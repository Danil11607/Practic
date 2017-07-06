using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Collections;
using Iter1;
/// <summary>
/// Дофига вопросов,начиная с того,как мы храним варианты ответа,ведь дам необходимо выцеплять их по подсчете,т.е нужно узнать детальное
/// устройство класса Question,т.е необходима реализация именно в классе Question было реализовано разделения на варинты ответа
/// работа на завтра:анализ всех случаев и заталкивание всего в БД.Спросить все насчет использовании БД.
namespace analyze
{
	enum type { Anon=1,HalfAnon,Public }
	enum answerType { single=1, multi, textSingle, textMulti };
	interface IAdd
	{
	    void AddAnonim();
        void AddPublic();
		void AddHalfAnonim();
	}
	interface IAnalyse
	{
		void AnalyseAnonim();
		void AnalysPublic();
		void AnalysHalfAnonim();

	}
	public  class change
	{
		
		public  class single {public static int count { get; set; } }
		public  class multi { public static int count { get; set; } }
		public  class textMulti { public static int count { get; set; } }
		public  class textSingle { public static int count { get; set; } }
	}
	class struct_poll :IAnalyse// для работы необходим доступ к классу с данными о опроснике,а именно
	{                          // количество вопросов,их тип и количество вариантов ответа для каждого
		                       // вопроса
		
		static int CountOfQuestions = 0;// нужно получать количество вопросов в опроснике
		answerType[] TypeArr = new answerType[CountOfQuestions];
		List<List<string>> ans = new List<List<string>>();
		change[] res = new change[CountOfQuestions];
		Question[] que;
		
		//Dictionary<string, int> dic = new Dictionary<string, int>(); 
		public struct_poll(List<List<string>> ans,Question[] que) { this.ans = ans; this.que = que; }
		void AnalyseAnonim()
		{
			foreach (var it in ans)
			{
				int count = 0;
				foreach (var iter in it)
				{
					//учитывается что первая строка вложенного листа,это id голосовавшего
					count++;
					if (count > 1)
					{
						switch (TypeArr[count - 1])
						{
							case answerType.single:
								
								break;
							case answerType.multi:

								break;
							case answerType.textMulti:
								
								break;
							case answerType.textSingle:
								res[count-1]
								break;

						}
					}
				}
			}
		}

	}

	class Add : IAdd
	{
		
		public Add()
		{

		}
		public void AddAnonim()
		{
			
		}

		public void AddHalfAnonim()
		{
			
		}

		public void AddPublic()
		{
			
		}
	}
	class Program
	{
		static void Main(string[] args)
		{
		}
	}
}
