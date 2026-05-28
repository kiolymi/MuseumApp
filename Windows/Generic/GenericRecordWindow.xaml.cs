using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using MuseumApp.Helpers;
using MuseumApp.Navigation;
using MuseumApp.Services.Crud;

namespace MuseumApp.Windows.Generic;

public partial class GenericRecordWindow : Window
{
    private readonly CrudTableSpec _spec;
    private readonly object? _existing;
    private readonly Dictionary<string, FrameworkElement> _controls = new();

    public GenericRecordWindow(TableId tableId, object? existing = null)
    {
        if (!CrudRegistry.TryGet(tableId, out _spec))
            throw new ArgumentException("Таблица не поддерживает универсальный CRUD: " + tableId);

        _existing = existing;
        InitializeComponent();
        Title = existing == null ? "Добавление" : "Изменение";
        BuildForm();
    }

    private void BuildForm()
    {
        var isEdit = _existing != null;
        foreach (var field in _spec.Fields)
        {
            var panel = new StackPanel { Margin = new Thickness(0, 0, 0, 10) };
            panel.Children.Add(new TextBlock { Text = field.Label, FontWeight = FontWeights.SemiBold });

            FrameworkElement input;
            if (field.Kind == CrudFieldKind.ForeignKey)
            {
                var combo = new ComboBox { MinWidth = 320, Height = 28 };
                if (field.ForeignKey is { } fk)
                {
                    if (!FkComboLoader.TryLoad(combo, fk))
                        MessageBox.Show($"Не удалось загрузить список: {field.Label}");
                }

                if (isEdit && field.ReadOnlyOnEdit)
                    combo.IsEnabled = false;

                input = combo;
            }
            else
            {
                var textBox = new TextBox { MinWidth = 320, Height = 28 };
                if (isEdit && field.ReadOnlyOnEdit)
                    textBox.IsReadOnly = true;
                input = textBox;
            }

            panel.Children.Add(input);
            fieldsPanel.Children.Add(panel);
            _controls[field.PropertyName] = input;

            if (_existing != null)
                SetControlValue(field, GetPropertyValue(_existing, field.PropertyName));
            else if (field.Kind == CrudFieldKind.DateOnly && input is TextBox dateBox)
                dateBox.Text = DateOnly.FromDateTime(DateTime.Today).ToString("yyyy-MM-dd");
            else if (field.Kind == CrudFieldKind.ForeignKey && input is ComboBox { Items.Count: > 0 } combo)
                combo.SelectedIndex = 0;
        }
    }

    private static object? GetPropertyValue(object entity, string propertyName) =>
        entity.GetType().GetProperty(propertyName)?.GetValue(entity);

    private void SetControlValue(CrudFieldSpec field, object? value)
    {
        if (!_controls.TryGetValue(field.PropertyName, out var control))
            return;

        if (control is ComboBox combo)
        {
            if (value != null)
                combo.SelectedValue = Convert.ToInt32(value);
            return;
        }

        if (control is not TextBox textBox)
            return;

        textBox.Text = value switch
        {
            null => "",
            DateOnly d => d.ToString("yyyy-MM-dd"),
            DateTime dt => dt.ToString("yyyy-MM-dd HH:mm"),
            TimeOnly t => t.ToString("HH:mm"),
            _ => Convert.ToString(value, CultureInfo.CurrentCulture) ?? ""
        };
    }

    private void btnSave_Click(object sender, RoutedEventArgs e)
    {
        try
        {
            var entity = _existing ?? Activator.CreateInstance(_spec.EntityType)
                ?? throw new InvalidOperationException("Не удалось создать запись.");

            foreach (var field in _spec.Fields)
            {
                var value = ReadFieldValue(field);
                var prop = _spec.EntityType.GetProperty(field.PropertyName);
                if (prop == null)
                    continue;
                prop.SetValue(entity, value);
            }

            GenericCrudService.Save(_spec, entity, _existing == null);
            DialogResult = true;
            Close();
        }
        catch (Exception ex)
        {
            MessageBox.Show(DbExceptionHelper.GetMessage(ex), "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
        }
    }

    private object? ReadFieldValue(CrudFieldSpec field)
    {
        if (!_controls.TryGetValue(field.PropertyName, out var control))
            return null;

        if (field.Kind == CrudFieldKind.ForeignKey)
        {
            if (control is not ComboBox combo)
                return null;
            if (combo.SelectedValue == null)
            {
                if (field.IsNullable)
                    return null;
                throw new InvalidOperationException($"Выберите значение: {field.Label}");
            }
            return Convert.ToInt32(combo.SelectedValue);
        }

        if (control is not TextBox textBox)
            return null;

        var text = textBox.Text.Trim();
        return field.Kind switch
        {
            CrudFieldKind.String => string.IsNullOrEmpty(text) ? throw new InvalidOperationException($"Заполните: {field.Label}") : text,
            CrudFieldKind.NullableString => string.IsNullOrEmpty(text) ? null : text,
            CrudFieldKind.Int => InputHelper.ParseInt(text),
            CrudFieldKind.NullableInt => InputHelper.ParseNullableInt(text),
            CrudFieldKind.Decimal => InputHelper.ParseDecimal(text),
            CrudFieldKind.NullableDecimal => string.IsNullOrWhiteSpace(text) ? null : InputHelper.ParseDecimal(text),
            CrudFieldKind.Double => double.Parse(text, CultureInfo.CurrentCulture),
            CrudFieldKind.NullableDouble => InputHelper.ParseNullableDouble(text),
            CrudFieldKind.DateOnly => DateOnly.Parse(text),
            CrudFieldKind.NullableDateOnly => string.IsNullOrWhiteSpace(text) ? null : DateOnly.Parse(text),
            CrudFieldKind.DateTime => DateTime.Parse(text, CultureInfo.CurrentCulture),
            CrudFieldKind.NullableDateTime => string.IsNullOrWhiteSpace(text) ? null : DateTime.Parse(text, CultureInfo.CurrentCulture),
            _ => text
        };
    }
}
