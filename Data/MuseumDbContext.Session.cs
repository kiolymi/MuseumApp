using Microsoft.EntityFrameworkCore;
using MuseumApp.Helpers;

namespace MuseumApp.Data;

public partial class MuseumDbContext
{
    public MuseumDbContext()
    {
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseNpgsql(SessionUser.GetConnectionString());
        }
    }
}
