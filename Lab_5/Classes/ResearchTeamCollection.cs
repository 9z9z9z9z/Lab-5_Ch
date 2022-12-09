using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab_5
{
    public class ResearchTeamCollection<TKey>
    {
        public string nameCollection { get; set; }
        private Dictionary<TKey, ResearchTeam> dictionary;
        private KeySelector<TKey> keyGenerator;
        public event ResearchTeamChangedHandler<TKey> reseachTeamChanged;

        public ResearchTeamCollection(KeySelector<TKey> method)
        {
            keyGenerator = method;
            dictionary = new Dictionary<TKey, ResearchTeam>();
        }
        public void AddDefaults(int count)
        {
            for (int i = 0; i < count; i++)
            {
                ResearchTeam rt = Inputs.inputRT();
                dictionary.Add(keyGenerator(rt), rt);
            }
        }
        public void AddDefaults(params ResearchTeam[] rts)
        {
            foreach (ResearchTeam item in rts)
            {
                dictionary.Add(keyGenerator(item), item);
            }
        }
        public DateTime MaxDateTimeElement
        {
            get
            {
                if (dictionary == null) return new DateTime();
                else
                {
                    return dictionary.Values.Max(obj => obj.data);
                }
            }
        }
        public IEnumerable<IGrouping <TimeFrame, KeyValuePair <TKey, ResearchTeam> > > grouping
        {
            get
            {
                return dictionary.GroupBy(obj => obj.Value.GetTime());
            }
        }
        public IEnumerable<KeyValuePair<TKey, ResearchTeam>> TimeFrameGroup(TimeFrame value)
        {
            return dictionary.Where(obj => obj.Value.GetTime() == value);
        }
        override public string ToString()
        {
            string ans = "";
            foreach (KeyValuePair<TKey, ResearchTeam> item in dictionary)
            {
                ans += "=============================\nKey:\n" + item.Key.ToString() + "\t\t\tTeam:\n" 
                    + item.Value.ToString() + "=============================";
            }
            return ans;
        }
        public string ToShortString()
        {
            string ans = "";
            foreach (KeyValuePair<TKey, ResearchTeam> item in dictionary)
            {
                ans += "=============================\nKey:\n" + item.Key.ToString() + "\t\t\tTeam:\n"
                    + item.Value.ToShortString() + "=============================";
            }
            return ans;
        }
        public bool Remove(ResearchTeam rt)
        {
            bool ret = false;
            if (dictionary.ContainsValue(rt))
            {
                foreach ((TKey key, ResearchTeam value) in dictionary)
                {
                    if (dictionary[key] == rt)
                    {
                        dictionary.Remove(key);
                        ret = true;
                    }
                }
            }
            return ret;
        }
        public bool Replace(ResearchTeam rtold, ResearchTeam rtnew)
        {
            if (dictionary.ContainsValue(rtold))
            {
                foreach (KeyValuePair<TKey, ResearchTeam> item in dictionary)
                {
                    if (item.Value == rtold)
                    {
                        dictionary[item.Key] = rtnew;
                        ResearchTeamPropertyChanged(Revision.Replace, "None", item.Value.Number);
                        rtold.PropertyChanged -= PropertyChangeded;
                        rtnew.PropertyChanged += PropertyChangeded;
                        break;
                    }
                }
                return true;
            }
            else return false;
        }
        private void ResearchTeamPropertyChanged(Revision rev, string name, int number)
        {
            reseachTeamChanged?.Invoke(this, new ResearchTeamChangedEventArgs<TKey>(nameCollection, rev, name, number));
        }
        private void PropertyChangeded(object sourse, EventArgs args)
        {
            ResearchTeamPropertyChanged(Revision.Property, (args as ResearchTeamChangedEventArgs<string>).propertyName, ((ResearchTeam)sourse).Number);
        }

    }
}
