using Lab_5.Classes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lab_5
{
    public class ResearchTeamChangedEventArgs<TKey> : EventArgs
    {
        public string collectionName { get; set; }
        public Revision revision { get; set; }
        public string propertyName { get; set; }
        public int numberReg { get; set; }

        public ResearchTeamChangedEventArgs(string collName, Revision rev, string propName, int regNum)
        {
            this.collectionName = collName;
            this.revision = rev;
            this.propertyName = propName;
            this.numberReg = regNum;
        }
        public override string ToString()
        {
            string ret = "";
            ret += "Change in collectiong:\t" + collectionName + '\n';
            ret += "Change type:\t" + revision.ToString() + '\n';
            ret += "Change in property:\t" + propertyName + '\n';
            ret += "Number of change:\t" + numberReg.ToString() + '\n';
            return ret;
        }
    }
}
