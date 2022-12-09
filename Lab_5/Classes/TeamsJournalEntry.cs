using System;
using System.Collections.Generic;
using System.Text;

namespace Lab_5
{
    class TeamsJournalEntry
    {
        public string name { get; set; }
        public Revision revision { get; set; }
        public string causeRT { get; set; }
        public int regNum { get; set; }

        public TeamsJournalEntry(string collName, Revision rev, string cause, int regNumber)
        {
            this.name = collName;
            this.revision = rev;
            this.causeRT = cause;
            this.regNum = regNumber;
        }
        public override string ToString()
        {
            return "Change in collection:\t" + name + "\nType of change:\t" + revision + 
                "\nChange in property:\t" + causeRT + "\nRegistration number:\t " + regNum;
        }
    }
}
