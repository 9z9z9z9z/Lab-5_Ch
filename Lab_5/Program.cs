using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;
using System.ComponentModel;

namespace Lab_5
{
    public delegate KeyValuePair<TKey, TValue> GenerateElement<TKey, TValue>(int j);
    public delegate TKey KeySelector<TKey>(ResearchTeam rt);
    public delegate void ResearchTeamChangedHandler<TKey>(object source, ResearchTeamChangedEventArgs<TKey> args);

    public class Program
    {
        
        static public KeyValuePair<int, string> generator(int i)
        {
            string value = (i * i * i * i * i * i * i).ToString();
            int key = i;
            return new KeyValuePair<int, string>(key, value);
        }

        static public KeySelector<string> keySelector = delegate (ResearchTeam rt)
        {
            return rt.GetHashCode().ToString();
        };
        static void Main(string[] args)
        {
            Console.ForegroundColor = ConsoleColor.White;
            List<Person> persons1 = new List<Person>(2);
            persons1.Add(new Person("Emil", "Markov", DateTime.Parse("14.11.2003")));
            persons1.Add(new Person("Kolya", "Homyzhenko", DateTime.Parse("11.06.2003")));
            List<Paper> paper1 = new List<Paper>(2);
            paper1.Add(new Paper("Title1", new Person("Egor", "Teterchev", DateTime.Parse("10.08.2003")), DateTime.Parse("10.08.2003")));
            paper1.Add(new Paper("Title2" ,new Person("Dima", "Panfilov", DateTime.Parse("09.07.2003")), DateTime.Parse("09.07.2003")));
            ResearchTeam t1 = new ResearchTeam("MIET", "SPinteh", TimeFrame.Long);
            t1.AddPersons(persons1);
            t1.AddPapers(paper1);

            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("========================= Task1 ==========================\n");
            Console.ForegroundColor = ConsoleColor.White;
            ResearchTeam deep_copy = t1.DeepCopy();
            Console.WriteLine("Origin: ");
            Console.WriteLine(t1.ToString());
            Console.WriteLine("Deep copy: ");
            Console.WriteLine(deep_copy.ToString());

            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("========================= Task2 ==========================\n");
            Console.ForegroundColor = ConsoleColor.White;

            Console.Write("Введите имя файла: ");
            ResearchTeam researchTeam = new ResearchTeam();
            string userFileName = Console.ReadLine() + ".bin";

            if (File.Exists(userFileName))
            {
                researchTeam.Load(userFileName);
            }
            else
            {
                Console.WriteLine("Файла с таким названием не найдено. Файл будет создан.");
                using (File.Create(userFileName)) ;
            }

            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("========================= Task3 ==========================\n");
            Console.ForegroundColor = ConsoleColor.White;

            Console.WriteLine(researchTeam.ToString());

            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("========================= Task4 ==========================\n");
            Console.ForegroundColor = ConsoleColor.White;

            researchTeam.AddFromConsole();
            researchTeam.Save(userFileName);
            Console.WriteLine(researchTeam.ToString());

            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("========================= Task5 ==========================\n");
            Console.ForegroundColor = ConsoleColor.White;

            ResearchTeam.Load(userFileName, researchTeam);
            researchTeam.AddFromConsole();
            ResearchTeam.Save(userFileName, researchTeam);

            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("========================= Task6 ==========================\n");
            Console.ForegroundColor = ConsoleColor.White;

            Console.WriteLine(researchTeam.ToString());

            Console.ForegroundColor = ConsoleColor.White;
        }
    }
}
