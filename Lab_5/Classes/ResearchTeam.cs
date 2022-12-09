using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;

namespace Lab_5
{
    [Serializable]
    public class ResearchTeam : Team, IEnumerable, INotifyPropertyChanged
    {
        private string theme; 
        private TimeFrame timeLong;
        private List<Person> persons;
        private List<Paper> papers;
        
        public ResearchTeam() : this("Try programming", "Homyzhenko", TimeFrame.Long) { }
        public ResearchTeam(string theme, string name, TimeFrame timeFrame)
        {
            this.theme = theme;
            this.name = name;
            this.timeLong = timeFrame;
            this.persons = new List<Person>();
            this.papers = new List<Paper>();
        }
        public string Theme
        {
            get { return theme; }
            set { theme = value; }
        }
        public Team team
        {
            get
            {
                Team team = new Team(this.name, this.number);
                return team;
            }
            set
            {
                this.name = value.Name;
                this.number = value.Number;
            }
        }
        public DateTime data
        {
            get
            {
                return papers.Max(obj => obj.date);
            }
        }
        public TimeFrame GetTime()
        {
            return timeLong;
        }
        public Paper findPaper()
        {
            if (papers == null) return null;
            Paper answer = new Paper();
            foreach (Paper point in papers)
            {
                if (DateTime.Compare(point.date, answer.date) == 1)
                {
                    answer = point;
                }
            }
            return answer;
        }
        public List<Paper> Papers() { return papers; }
        public List<Person> Persons() { return persons; }
        public void AddPersons(params Person[] items)
        {
            foreach (Person person in items)
            {
                persons.Add(person);
            }
        }
        public bool this[TimeFrame frame]
        {
            get { return (frame == timeLong); }
        }

        public void AddPersons(List<Person> persons)
        {
            foreach (Person person in persons)
            {
                this.persons.Add(person);
            }
        }
        public void AddPapers(List<Paper> papers)
        {
            foreach (Paper paper in papers)
            {
                this.papers.Add(paper);
            }
        }
        public void addPapers(params Paper[] items)
        {
            foreach (Paper paper in items)
            {
                papers.Add(paper);
            }
        }
        public override string ToString()
        {
            string list = "---------------------------------\n";
            foreach (Paper paper in papers)
            {
                list += paper.ToString() + "---------------------------------\n";
            }

            return "Thheme:\t" + theme + "\nAuthor:\t" + name + "\nNumber:\t" + number
                + '\n' + list + "=================================\n";
        }
        public string ToShortString()
        {
            return "Thheme:\t" + theme + "\nAuthor:\t" + name + "\nNumber:\t" + number
                + "\n=================================\n";
        }

        public void sortByData()
        {
            papers.Sort();
        }
        public void sortByAuthor()
        {
            PaperComparerBySurname comparer = new PaperComparerBySurname();
            papers.Sort(comparer);
        }
        public void sortByTitle()
        {
            IComparer<Paper> comparer = (IComparer<Paper>)new Paper();
            papers.Sort(comparer);
        }

        IEnumerable<Person> GetEnumerator()
        {
            for (int i = 0; i < persons.Count; i++)
            {
                bool flag = false;
                Person tmp_person = (Person)persons[i];
                for (int j = 0; j < papers.Count; j++)
                {
                    Paper tmp_paper = (Paper)papers[j];
                    if (tmp_person == tmp_paper.author) flag = true;
                }
                if (!flag) {
                    yield return tmp_person; 
                }
            }
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return persons.GetEnumerator();
        }
        public new ResearchTeam DeepCopy()
        {
            try
            {
                MemoryStream stream = new MemoryStream();
                IFormatter formatter = new BinaryFormatter();
                formatter.Serialize(stream, this);
                stream.Seek(0, SeekOrigin.Begin);
                return (ResearchTeam)formatter.Deserialize(stream);
            }
            catch (Exception e)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Возникла ошибка копирования: " + e.Message);
                Console.ResetColor();
                return new ResearchTeam();
            }

        }
        public bool AddFromConsole()
        {
            try
            {
                Console.WriteLine("1) Input paper;\n2) Input person\n");
                int choice = Inputs.inputInt("");
                switch (choice)
                {
                    case 1:
                        this.addPapers(Inputs.inputPaper());
                        break;
                    case 2:
                        this.AddPersons(Inputs.inputPerson());
                        break;
                }
                return true;
            }
            catch (Exception e)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Возникла ошибка вводе: " + e.Message);
                Console.ResetColor();
                return false;
            }
        }
        public bool Save(string filename)
        {
            try
            {
                var formatter = new BinaryFormatter();
                using (var fs = new FileStream(filename, FileMode.OpenOrCreate))
                {
                    formatter.Serialize(fs, this);
                }
                return true;
            }
            catch (Exception e)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Возникла ошибка в экземплярном сохранении: " + e.Message);
                Console.ResetColor();
                return false;
            }
        }
        public static bool Save(string filename, ResearchTeam obj)
        {
            try
            {
                var formatter = new BinaryFormatter();
                using (var fs = new FileStream(filename, FileMode.OpenOrCreate))
                {
                    fs.Seek(0, SeekOrigin.Begin);
                    formatter.Serialize(fs, obj);
                }

                return true;
            }
            catch (Exception e)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Возникла ошибка в статическом сохранении: " + e.Message);
                Console.ResetColor();
                return false;
            }
        }
        public bool Load(string filename)
        {
            try
            {
                var formatter = new BinaryFormatter();
                using (var fs = new FileStream(filename, FileMode.OpenOrCreate))
                {
                    if (fs.Length != 0)
                    {
                        var tmp = (ResearchTeam)formatter.Deserialize(fs);
                        this.name = tmp.name;
                        this.timeLong = tmp.GetTime();
                        if (papers != null) papers.Clear();
                        papers = new List<Paper>();
                        if (tmp.papers != null) papers.AddRange(tmp.papers);
                        if (persons != null) persons.Clear();
                        persons = new List<Person>();
                        if (tmp.persons != null) persons.AddRange(tmp.persons);
                    }
                    return true;
                }
            }
            catch (Exception e)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Возникла ошибка в экземплярной загрузке: " + e.Message);
                Console.ResetColor();
                return false;
            }
        }
        public static bool Load(string filename, ResearchTeam rt)
        {
            try
            {
                var formatter = new BinaryFormatter();
                using (var fs = new FileStream(filename, FileMode.OpenOrCreate))
                {
                    fs.Seek(0, SeekOrigin.Begin);
                    var tmp = (ResearchTeam)formatter.Deserialize(fs);
                    rt.name = tmp.name;
                    rt.timeLong = tmp.GetTime();
                    if (rt.papers != null) rt.papers.Clear();
                    rt.papers = new List<Paper>();
                    if (tmp.papers != null) rt.papers.AddRange(tmp.papers);
                    if (rt.persons != null) rt.persons.Clear();
                    rt.persons = new List<Person>();
                    if (tmp.persons != null) rt.persons.AddRange(tmp.persons);
                }
                return true;
            }
            catch (Exception e)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Возникла ошибка в статической загрузке: " + e.Message);
                Console.ResetColor();
                return false;
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
        public void CirculationChanged(string value)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(value));
        }
        public void DateChanged(string value)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(value));
        }
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        protected bool SetField<T>(ref T field, T value, [CallerMemberName] string propertyName = null)
        {
            if (EqualityComparer<T>.Default.Equals(field, value)) return false;
            field = value;
            OnPropertyChanged(propertyName);
            return true;
        }
    }
}
