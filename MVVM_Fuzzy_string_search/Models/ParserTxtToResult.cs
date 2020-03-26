﻿using System;
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
        public static ParseResult Parse(string filePth)
        {
            string[] lines = File.ReadAllLines(filePth);
            Queue<string> Lines = new Queue<string>(lines);

            List<RequestResult> results = new List<RequestResult>();
            List<Source> sources = new List<Source>();


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
                Source tmpSource = sources.Where(s => s.Name == source).FirstOrDefault();
                if(tmpSource == null)
                {
                    tmpSource = new Source() { Name = source };
                    sources.Add(tmpSource);
                }
                RequestResult newRes = new RequestResult { Source = tmpSource, Url = url, Content = content };
                results.Add(newRes);
            }

            return new ParseResult() { Sources = sources, RequestResults = results };
        }
    }
}
