using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManifestConverter
{

    public class ManifestTable 
    {
        public string Name { get; set; }
        public List<ManifestRow> Data { get; set; }
    }
    public class ManifestRow
    {
        public string id { get; set; }
        public string json { get; set; }
    }
}
