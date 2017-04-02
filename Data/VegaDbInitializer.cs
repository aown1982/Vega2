using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Vega.Models.Entities;

namespace Vega.Data
{
    public class VegaDbInitializer
    {
        private static VegaContext context;

        public static void Initialize(IServiceProvider serviceProvider)
        {
            context = (VegaContext)serviceProvider.GetService(typeof(VegaContext));
            InitializeVega();
        }

        private static void InitializeVega()
        {
            if(!context.Features.Any())
            {
                Feature feature_01 = new Feature { Name="ABS Brakes" };
                Feature feature_02 = new Feature { Name = "Cruze Control" };
                Feature feature_03 = new Feature { Name = "Airbags" };
                Feature feature_04 = new Feature { Name = "Sunroof" };
                Feature feature_05 = new Feature { Name = "Crash Avoidance" };

                context.Features.Add(feature_01); context.Features.Add(feature_02);
                context.Features.Add(feature_03); context.Features.Add(feature_04);
                context.Features.Add(feature_05); 
            }

            if (!context.Makes.Any())
            {
                Make make_01 = new Make
                {
                    Name = "Holden",
                    Models = new List<Model>
                    {
                        new Model(){ Name = "Cruze", MakeId = 1},
                        new Model(){ Name = "Berlina", MakeId = 1},
                        new Model(){ Name = "Caprice", MakeId = 1},
                    }
                };

                Make make_02 = new Make
                {
                    Name = "Toyota",
                    Models = new List<Model>
                    {
                        new Model(){ Name = "Camry", MakeId = 2},
                        new Model(){ Name = "Corolla", MakeId = 2},
                        new Model(){ Name = "Yaris", MakeId = 2},
                    }
                };

                Make make_03 = new Make
                {
                    Name = "Honda",
                    Models = new List<Model>
                    {
                        new Model(){ Name = "Accord", MakeId = 3},
                        new Model(){ Name = "Civic", MakeId = 3},
                        new Model(){ Name = "City", MakeId = 3},
                    }
                };

                context.Makes.Add(make_01); context.Makes.Add(make_02);
                context.Makes.Add(make_03);
            }


            context.SaveChanges();
        }

    }
}
