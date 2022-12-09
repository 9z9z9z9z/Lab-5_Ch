using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab_5
{

    public class TestCollections<TKey, TValue>
    {
        private List<TKey> keys;
        private List<string> values;
        private Dictionary<TKey, TValue> keyDict;
        private Dictionary<string, TValue> strDict;

        GenerateElement<TKey, TValue> generateElement;

        public TestCollections(int size, GenerateElement<TKey, TValue> method)
        {
            keys = new List<TKey>(size);
            values = new List<string>(size);
            keyDict = new Dictionary<TKey, TValue>(size);
            strDict = new Dictionary<string, TValue>(size);
            generateElement = method;
            for (int i = 0; i < size; i++)
            {
                KeyValuePair<TKey, TValue> keyValue = generateElement(i);
                keys.Add(keyValue.Key);
                values.Add(keyValue.Value.ToString());
                strDict.Add(keyValue.Key.ToString(), keyValue.Value);
                keyDict.Add(keyValue.Key, keyValue.Value);
            }
        }

        public void searchInKeyList()
        {
            TKey first = keys[0];
            TKey middle = keys[keys.Count / 2];
            TKey last = keys[keys.Count - 1];
            TKey nonExist = generateElement(keys.Count).Key;
            Stopwatch sw = new Stopwatch();
            Console.WriteLine("Search in TKey List");
            sw.Start();
            keys.Contains(first);
            sw.Stop();
            Console.WriteLine("First:\t" + sw.Elapsed);

            sw.Restart();
            keys.Contains(middle);
            sw.Stop();
            Console.WriteLine("Middle:\t" + sw.Elapsed);

            sw.Restart();
            keys.Contains(last);
            sw.Stop();
            Console.WriteLine("Last:\t" + sw.Elapsed);

            sw.Restart();
            keys.Contains(nonExist);
            sw.Stop();
            Console.WriteLine("NonExist:\t" + sw.Elapsed);
        }
        public void searchInValueList()
        {
            string first = values[0];
            string middle = values[values.Count / 2];
            string last = values[values.Count - 1];
            string nonExist = generateElement(keys.Count).Value.ToString();
            Stopwatch sw = new Stopwatch();
            Console.WriteLine("Search in TValue List");
            sw.Start();
            values.Contains(first);
            sw.Stop();
            Console.WriteLine("First:\t" + sw.Elapsed);

            sw.Restart();
            values.Contains(middle);
            sw.Stop();
            Console.WriteLine("Middle:\t" + sw.Elapsed);

            sw.Restart();
            values.Contains(last);
            sw.Stop();
            Console.WriteLine("Last:\t" + sw.Elapsed);

            sw.Restart();
            values.Contains(nonExist);
            sw.Stop();
            Console.WriteLine("Nonexist:\t" + sw.Elapsed);
        }
        public void searchInKeyDict()
        {
            TKey first = keyDict.Keys.ElementAt(0);
            TKey middle = keyDict.Keys.ElementAt(keyDict.Count / 2);
            TKey last = keyDict.Keys.ElementAt(keyDict.Count - 1);
            TKey nonExist = generateElement(keyDict.Count).Key;
            Stopwatch sw = new Stopwatch();
            Console.WriteLine("Search in Key Dictionary");
            sw.Start();
            keyDict.ContainsKey(first);
            sw.Stop();
            Console.WriteLine("First:\t" + sw.Elapsed);

            sw.Restart();
            keyDict.ContainsKey(middle);
            sw.Stop();
            Console.WriteLine("Middle:\t" + sw.Elapsed);

            sw.Restart();
            keyDict.ContainsKey(last);
            sw.Stop();
            Console.WriteLine("Last:\t" + sw.Elapsed);
            
            sw.Restart();
            keyDict.ContainsKey(nonExist);
            sw.Stop();
            Console.WriteLine("NonExist:\t" + sw.Elapsed);
        }
        public void searchInStrDict()
        {
            string first = strDict.Keys.ElementAt(0);
            string middle = strDict.Keys.ElementAt(strDict.Count / 2);
            string last = strDict.Keys.ElementAt(strDict.Count - 1);
            string nonExist = generateElement(strDict.Count).Key.ToString();
            Stopwatch sw = new Stopwatch();
            Console.WriteLine("Search in String Dictionary");
            sw.Start();
            strDict.ContainsKey(first);
            sw.Stop();
            Console.WriteLine("First:\t" + sw.Elapsed);

            sw.Restart();
            strDict.ContainsKey(middle);
            sw.Stop();
            Console.WriteLine("Middle:\t" + sw.Elapsed);

            sw.Restart();
            strDict.ContainsKey(last);
            sw.Stop();
            Console.WriteLine("Last:\t" + sw.Elapsed);

            sw.Restart();
            strDict.ContainsKey(nonExist);
            sw.Stop();
            Console.WriteLine("NonExist:\t" + sw.Elapsed);
        }
    }
}
