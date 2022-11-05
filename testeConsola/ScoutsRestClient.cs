using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace testeConsola
{
    public class ScoutsRestClient
    {
        public string? statusCode { get; set; }
        public string? description { get; set; }
        public string? requestLocation { get; set; }
        public Dictionary<string, string>? headers { get; set; }
    }
}