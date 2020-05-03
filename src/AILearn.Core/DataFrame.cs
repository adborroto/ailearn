using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AILearn.Core
{
    public class DataFrame
    {
        public DataFrame(object[][] data)
        {
            Data = data;
        }

        public int ItemCount { get { return Data.Length; } }

        public object[][] Data { get; }

        public object Get(int column, int row)
        {
            return Data[column][row];
        }
        public double GetDouble(int column, int row)
        {
            return (double)Data[column][row];
        }

        public double[][] GetDoubleRange(int from, int to)
        {
            return GetRange<double>(from, to, x => double.Parse(x.ToString()));
        }

        public T[][] GetRange<T>(int from, int to, Func<object, T> map)
        {
            var list = new List<T[]>();
            for (int i = 0; i < Data.Length; i++)
            {
                var newArray = Data[i].Skip(from).Take(to - from).Select(x => map(x)).ToArray();
                list.Add(newArray);
            }
            return list.ToArray();
        }

        public static double[][] GetDummies(object[] values)
        {
            List<double[]> list = new List<double[]>();

            var set = new HashSet<string>();
            foreach (var v in values)
            {
                set.Add(v.ToString());
            }
            var features = set.ToList();

            foreach (var v in values)
            {
                var index = features.FindIndex(x => x == v.ToString());
                var dummy = new double[set.Count];
                dummy[index] = 1;
                list.Add(dummy);
            }
            return list.ToArray();
        }

        public static DataFrame ReadCSV(string path, char splitter = ';', bool header = true)
        {
            var i = 0;
            using (var reader = new StreamReader(path))
            {
                List<string[]> list = new List<string[]>();
                while (!reader.EndOfStream)
                {
                    i++;
                    var line = reader.ReadLine().Trim();
                    if (i == 1 && header)
                        continue;
                    var values = line.Split(splitter);
                    list.Add(values);
                }
                return new DataFrame(list.ToArray());
            }
        }

    }
}
