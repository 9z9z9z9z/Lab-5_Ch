using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab_5
{
    [Serializable]
    public class Person: INameAndCopy, IComparable, IComparer<Person>
    {
        private string name;
        private string surename;
        private DateTime birthday;
        public Person() : this("Nikolay", "Homyzhenko", new DateTime(2003, 8, 8)) { }
        public Person(string name, string surename, DateTime birthday)
        {
            this.name = name;
            this.surename = surename;
            this.birthday = birthday;
        }
        public static bool operator ==(Person left, Person right)
        {
            if (right is null) return false;
            return ((left.name == right.name) && (left.surename == right.surename) 
                && (left.birthday == right.birthday)) ;
        }
        public static bool operator !=(Person left, Person right)
        {
            if (right is null) return false;
            return ((left.name != right.name) && (left.surename != right.surename)
                && (left.birthday != right.birthday));
        }
        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        public string Surename
        {
            get { return surename; }
            set { surename = value; }
        }
        public DateTime getBirth()
        {
            return birthday;
        }
        public void setBirth(int value)
        {
            birthday.AddYears(value);
        }
        public override string ToString()
        {
            return "Name:\t" + name + "\nSurename:\t" + surename + "\nBirthdate\t" + birthday.ToShortDateString();
        }
        public virtual string ToShortString()
        {
            return "Name:\t" + name + "\nSurename: " + surename;
        }
        public override bool Equals(object obj)
        {
            return this.Equals(obj as Person);
        }
        private bool Equals(Person obj)
        {
            if (obj is null) { return false; }
            else { return this.name == obj.Name && this.surename == obj.Surename &&
                    this.birthday == obj.birthday;  }
        }
        public override int GetHashCode()
        { 
            return HashCode.Combine(this.name, this.surename, this.birthday);
        }
        public virtual object DeepCopy()
        {
            Person person = new Person();
            person.name = this.name;
            person.surename = this.surename;
            person.birthday = this.birthday;
            return (object)person;
        }
        public int CompareTo(object? obj)
        {
            Person tmp = obj as Person;
            if (tmp == null) throw new InvalidCastException("\nInvalid type!\n");
            else return surename.CompareTo(tmp.surename);
        }
        public int Compare(Person leftPerson, Person rightPerson)
        {
            return leftPerson.surename.CompareTo(rightPerson.Surename);
        }
    }
}
