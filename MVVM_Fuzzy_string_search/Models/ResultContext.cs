using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVVM_Fuzzy_string_search.Models
{
    class ResultContext : DbContext
    {
        public ResultContext() : base("DefaultConnection")
        {
        }

        public DbSet<RequestResult> RequestResults { get; set; }
        public DbSet<Source> Sources { get; set; }
    }
}
