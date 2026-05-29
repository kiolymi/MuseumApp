using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using MuseumApp.Data.Entities;

namespace MuseumApp.Data;

public partial class MuseumDbContext
{
    public virtual DbSet<AuthorExLink> AuthorExLinks { get; set; }
    public virtual DbSet<ExhibitionExhibitLink> ExhibitionExhibitLinks { get; set; }
    public virtual DbSet<ExhibitionHallLink> ExhibitionHallLinks { get; set; }
    public virtual DbSet<ExhibitMaterialLink> ExhibitMaterialLinks { get; set; }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder)
    {
        ConfigureLinkTables(modelBuilder);
        ConfigureManualPrimaryKeys(modelBuilder);
    }

    private static void ConfigureLinkTables(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AuthorExLink>(entity =>
        {
            entity.HasKey(e => new { e.IdAuthor, e.IdExh }).HasName("author_ex_pkey");
            entity.ToTable("author_ex");
            entity.Property(e => e.IdAuthor).HasColumnName("id_author");
            entity.Property(e => e.IdExh).HasColumnName("id_exh");
        });

        modelBuilder.Entity<ExhibitionExhibitLink>(entity =>
        {
            entity.HasKey(e => new { e.IdExhibition, e.IdExhibit }).HasName("exhibition_exhibits_pkey");
            entity.ToTable("exhibition_exhibits");
            entity.Property(e => e.IdExhibition).HasColumnName("id_exhibition");
            entity.Property(e => e.IdExhibit).HasColumnName("id_exhibit");
        });

        modelBuilder.Entity<ExhibitionHallLink>(entity =>
        {
            entity.HasKey(e => new { e.IdExhibition, e.IdHall }).HasName("exhibition_halls_pkey");
            entity.ToTable("exhibition_halls");
            entity.Property(e => e.IdExhibition).HasColumnName("id_exhibition");
            entity.Property(e => e.IdHall).HasColumnName("id_hall");
        });

        modelBuilder.Entity<ExhibitMaterialLink>(entity =>
        {
            entity.HasKey(e => new { e.IdExhibit, e.IdMaterial }).HasName("exhibit_materials_pkey");
            entity.ToTable("exhibit_materials");
            entity.Property(e => e.IdExhibit).HasColumnName("id_exhibit");
            entity.Property(e => e.IdMaterial).HasColumnName("id_material");
        });
    }

    private static void ConfigureManualPrimaryKeys(ModelBuilder modelBuilder)
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
