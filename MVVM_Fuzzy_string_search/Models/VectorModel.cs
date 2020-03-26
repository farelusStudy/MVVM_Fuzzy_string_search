using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVVM_Fuzzy_string_search.Models
{
    class VectorModel
    {
        private static int[][] Word2Vec2(string word1, string word2)
        {
            int minLenght = (word1.Length < word2.Length) ? word1.Length : word2.Length;
            word1 = word1.ToLower();
            //word1 = word1.ToLower().Substring(0, minLenght);
            word2 = word2.ToLower().Substring(0, minLenght);
            string union = word1 + word2;
            List<char> set = new List<char>();

            foreach (var c in union)
            {
                set.Add(c);
            }
            set = set.Distinct().OrderBy(c => (int)c).ToList();
            int[] vec1 = new int[set.Count];
            int[] vec2 = new int[set.Count];

            for (int i = 0; i < set.Count; i++)
            {
                vec1[i] = (word1.Contains(set[i].ToString())) ? 1 : 0;
                vec2[i] = (word2.Contains(set[i].ToString())) ? 1 : 0;
            }
            return new int[][] { vec1, vec2 };
        }

        private static double CalcLength(int[] vec)
        {
            double length = 0;
            for (int i = 0; i < vec.Length; i++)
            {
                length += vec[i] * vec[i];
            }
            return Math.Sqrt(length);
        }

        private static double CalcMul(int[] vec1, int[] vec2)
        {
            if (vec1.Length != vec2.Length) throw new ArgumentException("Different array legths");
            double mul = 0;
            for (int i = 0; i < vec1.Length; i++)
            {
                mul += vec1[i] * vec2[i];
            }
            return mul;
        }

        private static double CalcCos(int[] vec1, int[] vec2)
        {
            if (vec1.Length != vec2.Length) throw new ArgumentException("Different array legths");

            double len1 = CalcLength(vec1);
            double len2 = CalcLength(vec2);
            double mul = CalcMul(vec1, vec2);
            return mul / (len1 * len2);
        }

        public static double CompareSenteces(string sent1, string sent2)
        {
            string[] s1 = sent1.Split(' ').ToArray();
            string[] s2 = sent2.Split(' ').ToArray();

            List<double> anses = new List<double>();
            double totalCos = 0;
            for (int i = 0; i < s1.Length; i++)
            {
                double tmpMax = 0;
                for (int j = 0; j < s2.Length; j++)
                {
                    int[][] vecs = Word2Vec2(s1[i], s2[j]);
                    double tmp = CalcCos(vecs[0], vecs[1]);
                    if (tmpMax < tmp) tmpMax = tmp;
                }
                totalCos += tmpMax;
            }
            return totalCos / s1.Length;
        }
    }
}
