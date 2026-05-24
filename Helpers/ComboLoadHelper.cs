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
}
