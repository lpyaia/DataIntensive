using DataIntensive.Api.Infrastructure.Data.EF.Maps;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace DataIntensive.Api.Infrastructure.Data.EF
{
    public class DataIntensiveEntityFrameworkContext : DbContext
    {
        public DataIntensiveEntityFrameworkContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            foreach (var et in from et in modelBuilder.Model.GetEntityTypes()
                               where et.ClrType.IsSubclassOf(typeof(BaseEntity))
                               select et)
            {
                et.FindProperty(nameof(BaseEntity.Id))!.ValueGenerated = Microsoft.EntityFrameworkCore.Metadata.ValueGenerated.Never;
            }

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(DataIntensiveEntityFrameworkContext).Assembly);
        }
    }
}