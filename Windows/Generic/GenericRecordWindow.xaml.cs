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
        {
            throw new ArgumentException("Таблица не поддерживает универсальный CRUD: " + tableId);
        }

        _existing = existing;
        InitializeComponent();

        if (existing == null)
        {
            Title = "Добавление";
        }
        else
        {
            Title = "Изменение";
        }

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
                    {
                        MessageBox.Show($"Не удалось загрузить список: {field.Label}");
                    }
                }

                if (isEdit && field.ReadOnlyOnEdit)
                {
                    combo.IsEnabled = false;
                }

                input = combo;
            }
            else
            {
                var textBox = new TextBox { MinWidth = 320, Height = 28 };

                if (isEdit && field.ReadOnlyOnEdit)
                {
                    textBox.IsReadOnly = true;
                }

                input = textBox;
            }

            panel.Children.Add(input);
            fieldsPanel.Children.Add(panel);
            _controls[field.PropertyName] = input;

            if (_existing != null)
            {
                SetControlValue(field, GetPropertyValue(_existing, field.PropertyName));
            }
            else if (field.Kind == CrudFieldKind.DateOnly && input is TextBox dateBox)
            {
                dateBox.Text = DateOnly.FromDateTime(DateTime.Today).ToString("yyyy-MM-dd");
            }
            else if (field.Kind == CrudFieldKind.ForeignKey && input is ComboBox combo && combo.Items.Count > 0)
            {
                combo.SelectedIndex = 0;
            }
        }
    }

    private static object? GetPropertyValue(object entity, string propertyName)
    {
        return entity.GetType().GetProperty(propertyName)?.GetValue(entity);
    }

    private void SetControlValue(CrudFieldSpec field, object? value)
    {
        if (!_controls.TryGetValue(field.PropertyName, out var control))
        {
            return;
        }

        if (control is ComboBox combo)
        {
            if (value != null)
            {
                combo.SelectedValue = Convert.ToInt32(value);
            }

            return;
        }

        if (control is not TextBox textBox)
        {
            return;
        }

        if (value == null)
        {
            textBox.Text = "";
            return;
        }

        if (value is DateOnly dateOnly)
        {
            textBox.Text = dateOnly.ToString("yyyy-MM-dd");
            return;
        }

        if (value is DateTime dateTime)
        {
            textBox.Text = dateTime.ToString("yyyy-MM-dd HH:mm");
            return;
        }

        if (value is TimeOnly timeOnly)
        {
            textBox.Text = timeOnly.ToString("HH:mm");
            return;
        }

        textBox.Text = Convert.ToString(value, CultureInfo.CurrentCulture) ?? "";
    }

    private void btnSave_Click(object sender, RoutedEventArgs e)
    {
        try
        {
            object entity;

            if (_existing != null)
            {
                entity = _existing;
            }
            else
            {
                entity = Activator.CreateInstance(_spec.EntityType)
                    ?? throw new InvalidOperationException("Не удалось создать запись.");
            }

            foreach (var field in _spec.Fields)
            {
                var value = ReadFieldValue(field);
                var prop = _spec.EntityType.GetProperty(field.PropertyName);

                if (prop == null)
                {
                    continue;
                }

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
        {
            return null;
        }

        if (field.Kind == CrudFieldKind.ForeignKey)
        {
            return ReadForeignKeyValue(field, control);
        }

        if (control is not TextBox textBox)
        {
            return null;
        }

        var text = textBox.Text.Trim();
        var validationError = ValidateStringField(field, text);

        if (validationError != null)
        {
            throw new InvalidOperationException(validationError);
        }

        return ParseFieldValue(field, text);
    }

    private static object? ReadForeignKeyValue(CrudFieldSpec field, FrameworkElement control)
    {
        if (control is not ComboBox combo)
        {
            return null;
        }

        if (combo.SelectedValue == null)
        {
            if (field.IsNullable)
            {
                return null;
            }

            throw new InvalidOperationException($"Выберите значение: {field.Label}");
        }

        return Convert.ToInt32(combo.SelectedValue);
    }

    private static string? ValidateStringField(CrudFieldSpec field, string text)
    {
        if (field.Kind != CrudFieldKind.String && field.Kind != CrudFieldKind.NullableString)
        {
            return null;
        }

        var validation = field.StringValidation;

        if (validation == StringValidationKind.None)
        {
            if (field.Kind == CrudFieldKind.String)
            {
                validation = StringValidationKind.SafeText;
            }
            else
            {
                validation = StringValidationKind.OptionalSafeText;
            }
        }

        switch (validation)
        {
            case StringValidationKind.SafeText:
                return ValidationHelper.SafeText(text, field.MaxLength, field.Label);

            case StringValidationKind.OptionalSafeText:
                return ValidationHelper.OptionalSafeText(text, field.MaxLength, field.Label);

            case StringValidationKind.Phone:
                return ValidationHelper.Phone(text, field.Label, required: field.Kind == CrudFieldKind.String);

            case StringValidationKind.Email:
                return ValidationHelper.Email(text, field.Label, required: field.Kind == CrudFieldKind.String);

            case StringValidationKind.Inn:
                return ValidationHelper.Inn(text, field.Label);

            default:
                return null;
        }
    }

    private static object? ParseFieldValue(CrudFieldSpec field, string text)
    {
        switch (field.Kind)
        {
            case CrudFieldKind.String:
                if (string.IsNullOrEmpty(text))
                {
                    throw new InvalidOperationException($"Заполните: {field.Label}");
                }

                return text;

            case CrudFieldKind.NullableString:
                if (string.IsNullOrEmpty(text))
                {
                    return null;
                }

                return text;

            case CrudFieldKind.Int:
                return InputHelper.ParseInt(text);

            case CrudFieldKind.NullableInt:
                return InputHelper.ParseNullableInt(text);

            case CrudFieldKind.Decimal:
                return InputHelper.ParseDecimal(text);

            case CrudFieldKind.NullableDecimal:
                if (string.IsNullOrWhiteSpace(text))
                {
                    return null;
                }

                return InputHelper.ParseDecimal(text);

            case CrudFieldKind.Double:
                return double.Parse(text, CultureInfo.CurrentCulture);

            case CrudFieldKind.NullableDouble:
                return InputHelper.ParseNullableDouble(text);

            case CrudFieldKind.DateOnly:
                return DateOnly.Parse(text);

            case CrudFieldKind.NullableDateOnly:
                if (string.IsNullOrWhiteSpace(text))
                {
                    return null;
                }

                return DateOnly.Parse(text);

            case CrudFieldKind.DateTime:
                return DateTime.Parse(text, CultureInfo.CurrentCulture);

            case CrudFieldKind.NullableDateTime:
                if (string.IsNullOrWhiteSpace(text))
                {
                    return null;
                }

                return DateTime.Parse(text, CultureInfo.CurrentCulture);

            default:
                return text;
        }
    }
}
