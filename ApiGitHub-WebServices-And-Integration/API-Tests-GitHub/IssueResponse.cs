using System;
using System.Collections.Generic;
using System.Text;

namespace API_Tests_GitHub
{
    public class IssueResponse
    {
       
        public long id { get; set; }

        public long number { get; set; }

        public string title { get; set; }

        public string body { get; set; }
    }
}
