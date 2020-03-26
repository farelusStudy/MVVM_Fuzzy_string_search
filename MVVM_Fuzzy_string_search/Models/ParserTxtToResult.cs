using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVVM_Fuzzy_string_search.Models
{
    class ParserTxtToResult
    {
        public static List<RequestResult> Parse(string filePth)
        {
            string[] lines = File.ReadAllLines(filePth);
            Queue<string> Lines = new Queue<string>(lines);

            List<RequestResult> results = new List<RequestResult>();
            if (lines.Length == 0) return results;


            while (Lines.Count >= 3)
            {
                string source = Lines.Dequeue();
                string url = Lines.Dequeue();
                string content = "";

                string line = "";
                do
                {
                    Debug.WriteLine(line);
                    content += line;
                    if (Lines.Count > 0)
                    {
                        line = Lines.Dequeue();
                    }
                    else
                    {
                        line = "";
                    }
                } while (!String.IsNullOrWhiteSpace(line));

                RequestResult newRes = new RequestResult { Source = source, Url = url, Content = content };
                results.Add(newRes);
            }

            return results;
        }
    }
}
