using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab_5
{
    [Serializable]
    public class Paper : IComparable, IComparer<Paper>
    {
        public string title { get; set; }
        public Person author { get; set; }
        public DateTime date { get; set; }
        public Paper(string title, Person author, DateTime date)
        {
            this.title = title;
            this.author = author;
            this.date = date;
        }
        public Paper() : this("Try programming", new Person(), new DateTime(2003, 8, 8)) { }
        public override string ToString()
        {
            return "Title:\t" + title + "\nAuthor:\n" + author.ToShortString() + "\ndate:\t" + date.ToShortDateString() + '\n';
        }
        public virtual object DeepCopy()
        {
            Paper tmp = new Paper();
            tmp.title = title;
            tmp.author = author;
            tmp.date = date;
            return tmp;
        }

        public int CompareTo(object obj)
        {
            Paper tmp = obj as Paper;
            if (tmp == null) throw new InvalidCastException("\nInvalid type!\n");
            else return this.date.CompareTo(tmp.date);
        }
        public int Compare(Paper leftPaper, Paper rightPaper)
        {
            return string.Compare(leftPaper.title, rightPaper.title);
        }        
    }
}
