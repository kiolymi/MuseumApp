using System.Windows;
using System.Windows.Controls;
using MuseumApp.Data;

namespace MuseumApp.Helpers;

public static class ComboLoadHelper
{
    public static void LoadEmployees(ComboBox combo, int selectedId)
    {
        try
        {
            var context = new MuseumDbContext();
            combo.ItemsSource = context.Employees
                .Select(e => new { Id = e.IdEmployee, Name = $"{e.LastName} {e.FirstName} {e.MiddleName}" })
                .ToList();
            combo.IsEnabled = true;
        }
        catch (Exception ex)
        {
            combo.ItemsSource = new[] { new { Id = selectedId, Name = $"Сотрудник #{selectedId}" } };
            combo.IsEnabled = false;
            MessageBox.Show("Список сотрудников недоступен: " + ex.Message);
        }

        combo.SelectedValue = selectedId;
    }

    public static bool TryLoadEmployeesForAdd(ComboBox combo)
    {
        try
        {
            var context = new MuseumDbContext();
            combo.ItemsSource = context.Employees
                .Select(e => new { Id = e.IdEmployee, Name = $"{e.LastName} {e.FirstName} {e.MiddleName}" })
                .ToList();
            if (combo.Items.Count > 0)
                combo.SelectedIndex = 0;
            return true;
        }
        catch (Exception ex)
        {
            MessageBox.Show("Нет доступа к таблице сотрудников: " + ex.Message);
            return false;
        }
    }

    public static void LoadBranches(ComboBox combo, int selectedId)
    {
        try
        {
            var context = new MuseumDbContext();
            combo.ItemsSource = context.Branches
                .Select(b => new { Id = b.IdBranch, Name = b.BranchName })
                .ToList();
            combo.IsEnabled = true;
        }
        catch (Exception ex)
        {
            combo.ItemsSource = new[] { new { Id = selectedId, Name = $"Филиал #{selectedId}" } };
            combo.IsEnabled = false;
            MessageBox.Show("Список филиалов недоступен: " + ex.Message);
        }

        combo.SelectedValue = selectedId;
    }

    public static bool TryLoadBranchesForAdd(ComboBox combo)
    {
        try
        {
            var context = new MuseumDbContext();
            combo.ItemsSource = context.Branches
                .Select(b => new { Id = b.IdBranch, Name = b.BranchName })
                .ToList();
            if (combo.Items.Count > 0)
                combo.SelectedIndex = 0;
            return true;
        }
        catch (Exception ex)
        {
            MessageBox.Show("Нет доступа к таблице филиалов: " + ex.Message);
            return false;
        }
    }

    public static void LoadCountries(ComboBox combo, int selectedId)
    {
        try
        {
            var context = new MuseumDbContext();
            combo.ItemsSource = context.Countries
                .Select(c => new { Id = c.IdCountry, Name = c.CountryName })
                .ToList();
            combo.IsEnabled = true;
        }
        catch (Exception ex)
        {
            combo.ItemsSource = new[] { new { Id = selectedId, Name = $"Страна #{selectedId}" } };
            combo.IsEnabled = false;
            MessageBox.Show("Список стран недоступен: " + ex.Message);
        }

        combo.SelectedValue = selectedId;
    }

    public static bool TryLoadCountriesForAdd(ComboBox combo)
    {
        try
        {
            var context = new MuseumDbContext();
            combo.ItemsSource = context.Countries
                .Select(c => new { Id = c.IdCountry, Name = c.CountryName })
                .ToList();
            if (combo.Items.Count > 0)
                combo.SelectedIndex = 0;
            return true;
        }
        catch (Exception ex)
        {
            MessageBox.Show("Нет доступа к таблице стран: " + ex.Message);
            return false;
        }
    }

    public static void LoadPositions(ComboBox combo, int selectedId)
    {
        try
        {
            var context = new MuseumDbContext();
            combo.ItemsSource = context.Positions
                .Select(p => new { Id = p.IdPosition, Name = p.PositionName })
                .ToList();
            combo.IsEnabled = true;
        }
        catch (Exception ex)
        {
            combo.ItemsSource = new[] { new { Id = selectedId, Name = $"Должность #{selectedId}" } };
            combo.IsEnabled = false;
            MessageBox.Show("Список должностей недоступен: " + ex.Message);
        }

        combo.SelectedValue = selectedId;
    }

    public static bool TryLoadPositionsForAdd(ComboBox combo)
    {
        try
        {
            var context = new MuseumDbContext();
            combo.ItemsSource = context.Positions
                .Select(p => new { Id = p.IdPosition, Name = p.PositionName })
                .ToList();
            if (combo.Items.Count > 0)
                combo.SelectedIndex = 0;
            return true;
        }
        catch (Exception ex)
        {
            MessageBox.Show("Нет доступа к таблице должностей: " + ex.Message);
            return false;
        }
    }

    public static void LoadVisitors(ComboBox combo, int selectedId)
    {
        try
        {
            var context = new MuseumDbContext();
            combo.ItemsSource = context.Visitors
                .Select(v => new { Id = v.IdVisitor, Name = $"{v.LastName} {v.FirstName}" })
                .ToList();
            combo.IsEnabled = true;
        }
        catch (Exception ex)
        {
            combo.ItemsSource = new[] { new { Id = selectedId, Name = $"Посетитель #{selectedId}" } };
            combo.IsEnabled = false;
            MessageBox.Show("Список посетителей недоступен: " + ex.Message);
        }

        combo.SelectedValue = selectedId;
    }

    public static bool TryLoadVisitorsForAdd(ComboBox combo)
    {
        try
        {
            var context = new MuseumDbContext();
            combo.ItemsSource = context.Visitors
                .Select(v => new { Id = v.IdVisitor, Name = $"{v.LastName} {v.FirstName}" })
                .ToList();
            if (combo.Items.Count > 0)
                combo.SelectedIndex = 0;
            return true;
        }
        catch (Exception ex)
        {
            MessageBox.Show("Нет доступа к таблице посетителей: " + ex.Message);
            return false;
        }
    }
}
