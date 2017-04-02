using System;
using System.Collections.Generic;

namespace Vega.Models.Entities
{
    public class Make : IEntityBase
    {
        public Make()
        {
            Models = new List<Model>();
        }
        public int Id { get ; set ; }
        public string Name { get; set; }
        public ICollection<Model> Models { get; set; }
    }


}