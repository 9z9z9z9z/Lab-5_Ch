using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab_5
{
    [Serializable]
    public class Team : INameAndCopy
    {
        protected string name;
        protected int number; 

        public Team(string name, int number)
        {
            this.name = name;
            this.number = number;
        }
        public Team() : this("WoT", 54) {}
        public string Name {
            get { return name; }
            set { this.name = value; }
        }
        public int Number
        {
            get { return number; }
            set {
                if (value <= 0) { throw new ArgumentOutOfRangeException("\n ERROR!\n\t Invalid argument! \n"); }
                else { this.number = value; }
            }
        }
        public static bool operator ==(Team left, Team right)
        {
            if (right is null) return false;
            if (left.GetHashCode() != right.GetHashCode()) return false;
            return left.Equals(right);
        }
        public static bool operator !=(Team left, Team right)
        {
            if (right is null) return true;
            if (left.GetHashCode() != right.GetHashCode()) return true;
            return left.Equals(right);
        }
        public override bool Equals(object? obj)
        {
            return Equals(obj as Team);
        }
        private bool Equals(Team team)
        {
            if (team is null) return false;
            return this.Name == team.Name && this.Number == team.Number;
        }
        public int GetHashCode(){
            return HashCode.Combine(name.GetHashCode(), number.GetHashCode());
        }
        public object DeepCopy()
        {
            Team tmp = new Team();
            tmp.name = this.name;
            tmp.number = this.number;
            return (object)tmp;
        }
    }
}
