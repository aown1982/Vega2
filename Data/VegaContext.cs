using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Vega.Models.Entities;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Vega.Data
{
    public class VegaContext :DbContext
    {
        public DbSet<Model> Models { get; set; }
        public DbSet<Make> Makes { get; set; }
        public DbSet<Feature> Features { get; set; }


        public VegaContext(DbContextOptions options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.Restrict;
            }

            modelBuilder.Entity<Model>().ToTable("Model");
            modelBuilder.Entity<Make>().ToTable("Make");
            modelBuilder.Entity<Feature>().ToTable("Feature");

            modelBuilder.Entity<Model>().
            HasOne(m => m.Make)
            .WithMany(m => m.Models)
            .HasForeignKey(m=>m.MakeId);

            base.OnModelCreating(modelBuilder);
        }


    }
}
