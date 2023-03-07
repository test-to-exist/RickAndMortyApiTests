using System;
using System.Collections.Generic;
using System.Text;

namespace RickAndMortyApiTests.DTO
{
    public class Location
    {
        public int id { get; set; }
        public string name { get; set; }
        public string type { get; set; }
        public string dimension { get; set; }
        public string[] residents { get; set; }
        public string url { get; set; }
        public DateTime created { get; set; }
    }
}
