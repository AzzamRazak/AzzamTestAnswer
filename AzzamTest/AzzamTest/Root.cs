using System;
using System.Collections.Generic;
using System.Text;

namespace AzzamTest
{

    public class Recipient
    {
        public List<string> tags { get; set; }
        public string name { get; set; }
        public int id { get; set; }
    }

    public class Root
    {
        public List<Recipient> recipients { get; set; }
    }
}
