using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVVM_Fuzzy_string_search.Models
{
    class Source
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<RequestResult> RequestResults { get; set; }
        public Source()
        {
            RequestResults = new List<RequestResult>();
        }
    }
}
