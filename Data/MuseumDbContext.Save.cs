using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace MuseumApp.Data;

public partial class MuseumDbContext
{
    public override int SaveChanges()
    {
        AssignPrimaryKeysForNewEntities();
        return base.SaveChanges();
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        AssignPrimaryKeysForNewEntities();
        return base.SaveChangesAsync(cancellationToken);
    }

    private void AssignPrimaryKeysForNewEntities()
    {
        foreach (var entry in ChangeTracker.Entries().Where(e => e.State == EntityState.Added))
        {
            var pk = entry.Metadata.FindPrimaryKey();
            if (pk == null || pk.Properties.Count != 1)
                continue;

            var property = pk.Properties[0];
            if (property.ClrType != typeof(int))
                continue;

            var propEntry = entry.Property(property.Name);
            if (propEntry.CurrentValue is int id && id != 0)
                continue;

            var entityType = entry.Metadata.ClrType;
            if (entityType == null)
                continue;

            propEntry.CurrentValue = GetNextPrimaryKey(entityType, property.Name);
        }
    }

    private int GetNextPrimaryKey(Type entityType, string propertyName)
    {
        var method = GetType()
            .GetMethod(nameof(GetNextPrimaryKeyCore), System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)!
            .MakeGenericMethod(entityType);
        return (int)method.Invoke(this, [propertyName])!;
    }

    private int GetNextPrimaryKeyCore<TEntity>(string propertyName) where TEntity : class =>
        (Set<TEntity>()
            .AsNoTracking()
            .Select(e => (int?)EF.Property<int>(e, propertyName))
            .Max() ?? 0) + 1;
}
