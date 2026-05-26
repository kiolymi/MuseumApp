using System.Windows;
using Npgsql;

namespace MuseumApp.Helpers;

public static class DbErrorHelper
{
    public static void Show(Exception ex)
    {
        MessageBox.Show(GetMessage(ex));
    }

    public static string GetMessage(Exception ex)
    {
        if (ex is PostgresException pg)
        {
            return pg.SqlState switch
            {
                "42501" => "Недостаточно прав для этой операции.",
                "23503" => "Операция невозможна: есть связанные записи.",
                "23505" => "Такая запись уже существует.",
                "22001" => "Слишком длинное значение для поля.",
                _ => "Ошибка базы данных: " + pg.MessageText
            };
        }

        if (ex is FormatException)
            return ex.Message;

        return ex.Message;
    }
}
