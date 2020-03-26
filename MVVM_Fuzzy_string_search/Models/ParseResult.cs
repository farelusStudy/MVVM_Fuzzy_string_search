using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVVM_Fuzzy_string_search.Models
{
    class ParseResult
    {
        public List<Source> Sources { get; set; }
        public List<RequestResult> RequestResults { get; set; }
    }
}
