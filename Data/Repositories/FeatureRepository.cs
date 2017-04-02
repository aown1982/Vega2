﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Vega.Data.Abstract;
using Vega.Models.Entities;

namespace Vega.Data.Repositories
{
    public class FeatureRepository : EntityBaseRepository<Feature>, IFeatureRepository
    {
        public FeatureRepository(VegaContext context) : base(context)
        {

        }
    }
}
