using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Countries
{
    internal class CountryInfo
    {
        public CountryName name { get; set; } = null!;
        public string region { get; set; } = null!;
        public string subregion { get; set; } = null!;
        public List<double> latlng { get; set; } = null!;
        public double area { get; set; }
        public int population { get; set; } = 0!;
    }

    internal class CountryName
    {
        public string common { get; set; } = null!;
    }
}
