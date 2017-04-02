using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Vega.Models.Entities
{
    public class Model : IEntityBase
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int MakeId { get; set; }
        public Make Make { get; set; }
    }
}
