using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab_5
{
    public class PaperComparer : IComparer<Paper>
    {
        public int Compare(Paper leftPaper, Paper rightPaper)
        {
            return DateTime.Compare(leftPaper.date, rightPaper.date);
        }
    }

    public class PaperComparerBySurname : IComparer<Paper>
    {
        public int Compare(Paper leftPaper, Paper rightPaper)
        {
            return leftPaper.author.Surename.CompareTo(rightPaper.author.Surename);
        }
    }
}
