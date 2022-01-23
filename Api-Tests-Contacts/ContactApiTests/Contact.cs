using System;
using System.Collections.Generic;
using System.Text;

namespace ContactApiTests
{
    public class Contact
    {
        public int id { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string email { get; set; }
        public string phone { get; set; }
        public string dateCtreated { get; set; }
        public string comments { get; set; }
    }
}
