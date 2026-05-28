using MuseumApp.Data;

namespace MuseumApp.Services.Crud;

public static class GenericCrudService
{
    public static void Save(CrudTableSpec spec, object entity, bool isNew)
    {
        using var context = new MuseumDbContext();
        var set = context.GetType()
            .GetProperties()
            .FirstOrDefault(p => p.PropertyType.IsGenericType
                                 && p.PropertyType.GetGenericArguments()[0] == spec.EntityType)
            ?.GetValue(context);

        if (set == null)
            throw new InvalidOperationException("DbSet не найден для " + spec.EntityType.Name);

        if (isNew)
        {
            var addMethod = set.GetType().GetMethod("Add");
            addMethod?.Invoke(set, [entity]);
            context.SaveChanges();
            return;
        }

        var tracked = FindTracked(context, spec, entity);
        if (tracked == null)
            throw new InvalidOperationException("Запись не найдена.");

        foreach (var field in spec.Fields)
        {
            var prop = spec.EntityType.GetProperty(field.PropertyName);
            if (prop == null || !prop.CanWrite)
                continue;
            prop.SetValue(tracked, prop.GetValue(entity));
        }

        context.SaveChanges();
    }

    public static void Delete(CrudTableSpec spec, object entity)
    {
        using var context = new MuseumDbContext();
        var tracked = FindTracked(context, spec, entity);
        if (tracked == null)
            throw new InvalidOperationException("Запись не найдена.");

        context.Remove(tracked);
        context.SaveChanges();
    }

    public static string GetDisplayText(CrudTableSpec spec, object entity)
    {
        var prop = spec.EntityType.GetProperty(spec.DisplayProperty);
        var value = prop?.GetValue(entity);
        return value?.ToString() ?? "запись";
    }

    private static object? FindTracked(MuseumDbContext context, CrudTableSpec spec, object entity)
    {
        var keyValues = spec.KeyProperties
            .Select(k => spec.EntityType.GetProperty(k)?.GetValue(entity))
            .ToArray();

        var set = context.GetType()
            .GetProperties()
            .First(p => p.PropertyType.IsGenericType
                        && p.PropertyType.GetGenericArguments()[0] == spec.EntityType)
            .GetValue(context);

        var findMethod = set!.GetType().GetMethod("Find", [typeof(object[])]);
        return findMethod?.Invoke(set, [keyValues]);
    }
}
