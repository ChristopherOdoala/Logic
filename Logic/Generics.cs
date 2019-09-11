using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Logic
{
    public static class MyGenerics
    {
        public static bool Check<T>(this int[] sequence, int[] source)
        {
            int j = 0;
            for (int i = 1; i <= source.Count(); i++)
            {
                var res = sequence.Contains(source[j]);
                j += 1;
                if (res)
                    return true;
            }
            return false;
        }
        
        public static int Unique<T>(this int[] sequence, int[] comparer)
        {
            if (comparer.Count() > 0)
            {
                foreach (var set in comparer)
                {
                    int j = 0;
                    for (int i = 1; i <= sequence.Count(); i++)
                    {
                        var res = Equals(set, sequence[j]);
                        if (!res)
                            return sequence[j];
                        j++;
                    }
                }
                return 0;
            }

            else
                return sequence[0];
        }

        public static bool EqualsZero<T>(this int value)
        {
            if (value == 0)
                return true;
            else
                return false;
        }

        public static List<int> UniqueList<T>(this int[] sequence, int[] comparer)
        {
            var result = new List<int> { };
            if (comparer.Count() > 0)
            {
                foreach (var comp in comparer)
                {
                    foreach (var seq in sequence)
                    {
                        var res = Equals(seq, comp);
                        if (!res)
                            result.Add(seq);
                    }
                }
                return result;
            }
            
            return sequence.ToList();
        }

    }
}
