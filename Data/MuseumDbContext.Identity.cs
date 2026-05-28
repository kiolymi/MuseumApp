using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace MuseumApp.Data;

public partial class MuseumDbContext
{
    partial void OnModelCreatingPartial(ModelBuilder modelBuilder)
    {
        foreach (var entityType in modelBuilder.Model.GetEntityTypes())
        {
            var pk = entityType.FindPrimaryKey();
            if (pk == null || pk.Properties.Count != 1)
                continue;

            var property = pk.Properties[0];
            if (property.ClrType != typeof(int))
                continue;

            if (property.ValueGenerated == ValueGenerated.Never)
                continue;

            modelBuilder.Entity(entityType.ClrType)
                .Property(property.Name)
                .ValueGeneratedNever();
        }
    }
}
