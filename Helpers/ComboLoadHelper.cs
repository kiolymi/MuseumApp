using System.Collections;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using MuseumApp.Data;

namespace MuseumApp.Helpers;

public static class ComboLoadHelper
{
    public static void LoadEmployees(ComboBox combo, int selectedId)
    {
        if (TryFillEmployees(combo))
            combo.IsEnabled = true;
        else
        {
            combo.ItemsSource = new[] { new { Id = selectedId, Name = $"Сотрудник #{selectedId}" } };
            combo.IsEnabled = false;
            MessageBox.Show("Не удалось загрузить список сотрудников.");
        }

        combo.SelectedValue = selectedId;
    }

    public static bool TryLoadEmployeesForAdd(ComboBox combo)
    {
        if (!TryFillEmployees(combo))
        {
            MessageBox.Show("Не удалось загрузить список сотрудников.");
            return false;
        }

        if (combo.Items.Count > 0)
            combo.SelectedIndex = 0;
        return true;
    }

    private static bool TryFillEmployees(ComboBox combo)
    {
        try
        {
            using var context = new MuseumDbContext();
            combo.ItemsSource = context.Employees
                .Select(e => new { Id = e.IdEmployee, Name = $"{e.LastName} {e.FirstName} {e.MiddleName}".Trim() })
                .ToList();
            return combo.Items.Count > 0;
        }
        catch
        {
            try
            {
                using var context = new MuseumDbContext();
                combo.ItemsSource = context.VwEmployeeDuties
                    .Where(e => e.IdEmployee != null)
                    .Select(e => new { Id = e.IdEmployee!.Value, Name = e.FullName ?? $"Сотрудник #{e.IdEmployee}" })
                    .ToList();
                return combo.Items.Count > 0;
            }
            catch
            {
                return false;
            }
        }
    }

    public static void LoadBranches(ComboBox combo, int selectedId)
    {
        if (TryFillBranches(combo))
            combo.IsEnabled = true;
        else
        {
            combo.ItemsSource = new[] { new { Id = selectedId, Name = $"Филиал #{selectedId}" } };
            combo.IsEnabled = false;
            MessageBox.Show("Не удалось загрузить список филиалов.");
        }

        combo.SelectedValue = selectedId;
    }

    public static bool TryLoadBranchesForAdd(ComboBox combo)
    {
        if (!TryFillBranches(combo))
        {
            MessageBox.Show("Не удалось загрузить список филиалов.");
            return false;
        }

        if (combo.Items.Count > 0)
            combo.SelectedIndex = 0;
        return true;
    }

    private static bool TryFillBranches(ComboBox combo)
    {
        try
        {
            using var context = new MuseumDbContext();
            combo.ItemsSource = context.Branches
                .Select(b => new { Id = b.IdBranch, Name = b.BranchName })
                .ToList();
            return combo.Items.Count > 0;
        }
        catch
        {
            return false;
        }
    }

    public static void LoadCountries(ComboBox combo, int selectedId)
    {
        if (TryFillCountries(combo))
            combo.IsEnabled = true;
        else
        {
            combo.ItemsSource = new[] { new { Id = selectedId, Name = $"Страна #{selectedId}" } };
            combo.IsEnabled = false;
            MessageBox.Show("Не удалось загрузить список стран.");
        }

        combo.SelectedValue = selectedId;
    }

    public static bool TryLoadCountriesForAdd(ComboBox combo)
    {
        if (!TryFillCountries(combo))
        {
            MessageBox.Show("Не удалось загрузить список стран.");
            return false;
        }

        if (combo.Items.Count > 0)
            combo.SelectedIndex = 0;
        return true;
    }

    private static bool TryFillCountries(ComboBox combo)
    {
        try
        {
            using var context = new MuseumDbContext();
            combo.ItemsSource = context.Countries
                .Select(c => new { Id = c.IdCountry, Name = c.CountryName })
                .ToList();
            return combo.Items.Count > 0;
        }
        catch
        {
            return false;
        }
    }

    public static void LoadPositions(ComboBox combo, int selectedId)
    {
        if (TryFillPositions(combo))
            combo.IsEnabled = true;
        else
        {
            combo.ItemsSource = new[] { new { Id = selectedId, Name = $"Должность #{selectedId}" } };
            combo.IsEnabled = false;
            MessageBox.Show("Не удалось загрузить список должностей.");
        }

        combo.SelectedValue = selectedId;
    }

    public static bool TryLoadPositionsForAdd(ComboBox combo)
    {
        if (!TryFillPositions(combo))
        {
            MessageBox.Show("Не удалось загрузить список должностей.");
            return false;
        }

        if (combo.Items.Count > 0)
            combo.SelectedIndex = 0;
        return true;
    }

    private static bool TryFillPositions(ComboBox combo)
    {
        try
        {
            using var context = new MuseumDbContext();
            combo.ItemsSource = context.Positions
                .Select(p => new { Id = p.IdPosition, Name = p.PositionName })
                .ToList();
            return combo.Items.Count > 0;
        }
        catch
        {
            return false;
        }
    }

    public static void LoadVisitors(ComboBox combo, int selectedId)
    {
        if (TryFillVisitors(combo))
            combo.IsEnabled = true;
        else
        {
            combo.ItemsSource = new[] { new { Id = selectedId, Name = $"Посетитель #{selectedId}" } };
            combo.IsEnabled = false;
            MessageBox.Show("Не удалось загрузить список посетителей.");
        }

        combo.SelectedValue = selectedId;
    }

    public static bool TryLoadVisitorsForAdd(ComboBox combo)
    {
        if (!TryFillVisitors(combo))
        {
            MessageBox.Show("Не удалось загрузить список посетителей.");
            return false;
        }

        if (combo.Items.Count > 0)
            combo.SelectedIndex = 0;
        return true;
    }

    private static bool TryFillVisitors(ComboBox combo)
    {
        try
        {
            using var context = new MuseumDbContext();
            combo.ItemsSource = context.Visitors
                .Select(v => new { Id = v.IdVisitor, Name = $"{v.LastName} {v.FirstName}".Trim() })
                .ToList();
            return combo.Items.Count > 0;
        }
        catch
        {
            return false;
        }
    }

    public static bool TryLoadMuseumsForAdd(ComboBox combo) => TryFillAndSelect(combo, () =>
    {
        using var context = new MuseumDbContext();
        return context.Museums.Select(m => new { Id = m.IdMuseum, Name = m.Name }).ToList();
    });

    public static void LoadMuseums(ComboBox combo, int selectedId)
    {
        if (TryFillMuseums(combo))
            combo.IsEnabled = true;
        else
        {
            combo.ItemsSource = new[] { new { Id = selectedId, Name = $"Музей #{selectedId}" } };
            combo.IsEnabled = false;
        }

        combo.SelectedValue = selectedId;
    }

    private static bool TryFillMuseums(ComboBox combo)
    {
        try
        {
            using var context = new MuseumDbContext();
            combo.ItemsSource = context.Museums.Select(m => new { Id = m.IdMuseum, Name = m.Name }).ToList();
            return combo.Items.Count > 0;
        }
        catch
        {
            return false;
        }
    }

    public static bool TryLoadAdressesForAdd(ComboBox combo) => TryFillAndSelect(combo, () =>
    {
        using var context = new MuseumDbContext();
        return context.Adresses.Select(a => new { Id = a.IdAddress, Name = $"{a.City}, {a.Street}, {a.House}" }).ToList();
    });

    public static void LoadAdresses(ComboBox combo, int selectedId)
    {
        if (TryFillAdresses(combo))
            combo.IsEnabled = true;
        else
        {
            combo.ItemsSource = new[] { new { Id = selectedId, Name = $"Адрес #{selectedId}" } };
            combo.IsEnabled = false;
        }

        combo.SelectedValue = selectedId;
    }

    private static bool TryFillAdresses(ComboBox combo)
    {
        try
        {
            using var context = new MuseumDbContext();
            combo.ItemsSource = context.Adresses
                .Select(a => new { Id = a.IdAddress, Name = $"{a.City}, {a.Street}, {a.House}" })
                .ToList();
            return combo.Items.Count > 0;
        }
        catch
        {
            return false;
        }
    }

    public static bool TryLoadStoragesForAdd(ComboBox combo) => TryFillAndSelect(combo, () =>
    {
        using var context = new MuseumDbContext();
        return context.Storages.Select(s => new { Id = s.IdStorage, Name = s.StorageName }).ToList();
    });

    public static void LoadStorages(ComboBox combo, int selectedId)
    {
        if (TryFillStorages(combo))
            combo.IsEnabled = true;
        else
        {
            combo.ItemsSource = new[] { new { Id = selectedId, Name = $"Хранилище #{selectedId}" } };
            combo.IsEnabled = false;
        }

        combo.SelectedValue = selectedId;
    }

    private static bool TryFillStorages(ComboBox combo)
    {
        try
        {
            using var context = new MuseumDbContext();
            combo.ItemsSource = context.Storages.Select(s => new { Id = s.IdStorage, Name = s.StorageName }).ToList();
            return combo.Items.Count > 0;
        }
        catch
        {
            return false;
        }
    }

    public static bool TryLoadExhibitionsForAdd(ComboBox combo) => TryFillAndSelect(combo, () =>
    {
        using var context = new MuseumDbContext();
        return context.Exhibitions.Select(e => new { Id = e.IdExhibition, Name = e.ExhibitionName }).ToList();
    });

    public static bool TryLoadExhibitsForAdd(ComboBox combo) => TryFillAndSelect(combo, () =>
    {
        using var context = new MuseumDbContext();
        return context.Exhibits.Select(e => new { Id = e.IdExhibit, Name = e.Name }).ToList();
    });

    public static void LoadExhibits(ComboBox combo, int selectedId)
    {
        if (TryFillExhibits(combo))
            combo.IsEnabled = true;
        else
        {
            combo.ItemsSource = new[] { new { Id = selectedId, Name = $"Экспонат #{selectedId}" } };
            combo.IsEnabled = false;
        }

        combo.SelectedValue = selectedId;
    }

    private static bool TryFillExhibits(ComboBox combo)
    {
        try
        {
            using var context = new MuseumDbContext();
            combo.ItemsSource = context.Exhibits.Select(e => new { Id = e.IdExhibit, Name = e.Name }).ToList();
            return combo.Items.Count > 0;
        }
        catch
        {
            return false;
        }
    }

    public static bool TryLoadMaterialsForAdd(ComboBox combo) => TryFillAndSelect(combo, () =>
    {
        using var context = new MuseumDbContext();
        return context.Materials.Select(m => new { Id = m.IdMaterial, Name = m.MaterialName }).ToList();
    });

    public static bool TryLoadReasonsForAdd(ComboBox combo) => TryFillAndSelect(combo, () =>
    {
        using var context = new MuseumDbContext();
        return context.Reasons.Select(r => new { Id = r.IdReason, Name = r.ReasonDescription }).ToList();
    });

    public static void LoadReasons(ComboBox combo, int selectedId)
    {
        if (TryFillReasons(combo))
            combo.IsEnabled = true;
        else
        {
            combo.ItemsSource = new[] { new { Id = selectedId, Name = $"Причина #{selectedId}" } };
            combo.IsEnabled = false;
        }

        combo.SelectedValue = selectedId;
    }

    private static bool TryFillReasons(ComboBox combo)
    {
        try
        {
            using var context = new MuseumDbContext();
            combo.ItemsSource = context.Reasons.Select(r => new { Id = r.IdReason, Name = r.ReasonDescription }).ToList();
            return combo.Items.Count > 0;
        }
        catch
        {
            return false;
        }
    }

    public static bool TryLoadEventsForAdd(ComboBox combo) => TryFillAndSelect(combo, () =>
    {
        using var context = new MuseumDbContext();
        return context.Events.Select(e => new { Id = e.IdEvent, Name = e.EventName }).ToList();
    });

    public static void LoadEvents(ComboBox combo, int selectedId)
    {
        if (TryFillEvents(combo))
            combo.IsEnabled = true;
        else
        {
            combo.ItemsSource = new[] { new { Id = selectedId, Name = $"Мероприятие #{selectedId}" } };
            combo.IsEnabled = false;
        }

        combo.SelectedValue = selectedId;
    }

    private static bool TryFillEvents(ComboBox combo)
    {
        try
        {
            using var context = new MuseumDbContext();
            combo.ItemsSource = context.Events.Select(e => new { Id = e.IdEvent, Name = e.EventName }).ToList();
            return combo.Items.Count > 0;
        }
        catch
        {
            return false;
        }
    }

    public static bool TryLoadCompaniesForAdd(ComboBox combo) => TryFillAndSelect(combo, () =>
    {
        using var context = new MuseumDbContext();
        return context.Companies.Select(c => new { Id = c.IdCompany, Name = c.CompanyName }).ToList();
    });

    public static void LoadCompanies(ComboBox combo, int selectedId)
    {
        if (TryFillCompanies(combo))
            combo.IsEnabled = true;
        else
        {
            combo.ItemsSource = new[] { new { Id = selectedId, Name = $"Компания #{selectedId}" } };
            combo.IsEnabled = false;
        }

        combo.SelectedValue = selectedId;
    }

    private static bool TryFillCompanies(ComboBox combo)
    {
        try
        {
            using var context = new MuseumDbContext();
            combo.ItemsSource = context.Companies.Select(c => new { Id = c.IdCompany, Name = c.CompanyName }).ToList();
            return combo.Items.Count > 0;
        }
        catch
        {
            return false;
        }
    }

    public static bool TryLoadShopsForAdd(ComboBox combo) => TryFillAndSelect(combo, () =>
    {
        using var context = new MuseumDbContext();
        return context.Shops.Select(s => new { Id = s.IdShop, Name = s.ShopName }).ToList();
    });

    public static void LoadShops(ComboBox combo, int selectedId)
    {
        if (TryFillShops(combo))
            combo.IsEnabled = true;
        else
        {
            combo.ItemsSource = new[] { new { Id = selectedId, Name = $"Магазин #{selectedId}" } };
            combo.IsEnabled = false;
        }

        combo.SelectedValue = selectedId;
    }

    private static bool TryFillShops(ComboBox combo)
    {
        try
        {
            using var context = new MuseumDbContext();
            combo.ItemsSource = context.Shops.Select(s => new { Id = s.IdShop, Name = s.ShopName }).ToList();
            return combo.Items.Count > 0;
        }
        catch
        {
            return false;
        }
    }

    public static bool TryLoadProductsForAdd(ComboBox combo) => TryFillAndSelect(combo, () =>
    {
        using var context = new MuseumDbContext();
        return context.Products.Select(p => new { Id = p.IdProduct, Name = p.ProductName }).ToList();
    });

    public static void LoadProducts(ComboBox combo, int selectedId)
    {
        if (TryFillProducts(combo))
            combo.IsEnabled = true;
        else
        {
            combo.ItemsSource = new[] { new { Id = selectedId, Name = $"Товар #{selectedId}" } };
            combo.IsEnabled = false;
        }

        combo.SelectedValue = selectedId;
    }

    private static bool TryFillProducts(ComboBox combo)
    {
        try
        {
            using var context = new MuseumDbContext();
            combo.ItemsSource = context.Products.Select(p => new { Id = p.IdProduct, Name = p.ProductName }).ToList();
            return combo.Items.Count > 0;
        }
        catch
        {
            return false;
        }
    }

    public static bool TryLoadAuthorsForAdd(ComboBox combo) => TryFillAndSelect(combo, () =>
    {
        using var context = new MuseumDbContext();
        return context.Authors.Select(a => new { Id = a.IdAuthor, Name = $"{a.LastName} {a.FirstName}".Trim() }).ToList();
    });

    public static bool TryLoadLinkCombos(ComboBox cmb1, ComboBox cmb2, string linkKind)
    {
        try
        {
            using var context = new MuseumDbContext();
            switch (linkKind)
            {
                case "AuthorEx":
                    cmb1.ItemsSource = context.Authors.Select(a => new { Id = a.IdAuthor, Name = $"{a.LastName} {a.FirstName}".Trim() }).ToList();
                    cmb2.ItemsSource = context.Exhibits.Select(e => new { Id = e.IdExhibit, Name = e.Name }).ToList();
                    break;
                case "ExhibitionHall":
                    cmb1.ItemsSource = context.Exhibitions.Select(e => new { Id = e.IdExhibition, Name = e.ExhibitionName }).ToList();
                    cmb2.ItemsSource = context.Halls.Select(h => new { Id = h.IdHall, Name = h.HallName }).ToList();
                    break;
                case "ExhibitionExhibit":
                    cmb1.ItemsSource = context.Exhibitions.Select(e => new { Id = e.IdExhibition, Name = e.ExhibitionName }).ToList();
                    cmb2.ItemsSource = context.Exhibits.Select(e => new { Id = e.IdExhibit, Name = e.Name }).ToList();
                    break;
                case "ExhibitMaterial":
                    cmb1.ItemsSource = context.Exhibits.Select(e => new { Id = e.IdExhibit, Name = e.Name }).ToList();
                    cmb2.ItemsSource = context.Materials.Select(m => new { Id = m.IdMaterial, Name = m.MaterialName }).ToList();
                    break;
                default:
                    return false;
            }

            if (cmb1.Items.Count > 0) cmb1.SelectedIndex = 0;
            if (cmb2.Items.Count > 0) cmb2.SelectedIndex = 0;
            return cmb1.Items.Count > 0 && cmb2.Items.Count > 0;
        }
        catch
        {
            MessageBox.Show("Не удалось загрузить списки для связи.");
            return false;
        }
    }

    private static bool TryFillAndSelect(ComboBox combo, Func<IEnumerable> load)
    {
        try
        {
            combo.ItemsSource = load();
            if (combo.Items.Count > 0)
                combo.SelectedIndex = 0;
            return combo.Items.Count > 0;
        }
        catch
        {
            return false;
        }
    }
}
