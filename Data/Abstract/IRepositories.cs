using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Vega.Models.Entities;

namespace Vega.Data.Abstract
{
    public interface IMakeRepository : IEntityBaseRepository<Make> { }

    public interface IModelRepository : IEntityBaseRepository<Model> { }

    public interface IFeatureRepository : IEntityBaseRepository<Feature> { }
}
