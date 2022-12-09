using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab_5.Classes
{
    internal class PersonComparer:IComparer<Person>
    {
        public int Compare(Person leftPerson, Person rightPerson)
        {
            return string.Compare(leftPerson.Surename, rightPerson.Surename);
        }
    }
}
