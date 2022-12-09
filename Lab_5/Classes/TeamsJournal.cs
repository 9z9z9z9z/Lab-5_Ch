using System;
using System.Collections.Generic;
using System.Text;

namespace Lab_5
{
    class TeamsJournal
    {
        private List<TeamsJournalEntry> changeList = new List<TeamsJournalEntry>();

        public void AddChanges(object obj, EventArgs eventArgs)
        {
            ResearchTeamChangedEventArgs<string> even = eventArgs as ResearchTeamChangedEventArgs<string>;
            changeList.Add(new TeamsJournalEntry(even.collectionName, even.revision, even.propertyName, even.numberReg));
        }

        public override string ToString()
        {
            string ret = "";
            foreach (var item in changeList)
            {
                ret += item.ToString();
            }
            return ret;
        }
    }
}
