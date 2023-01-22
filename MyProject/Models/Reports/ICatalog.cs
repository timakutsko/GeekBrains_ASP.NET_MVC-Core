using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyProject.Models.Reports
{
    internal interface ICatalog
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public DateTime CreationDate { get; set; }
    }
}
