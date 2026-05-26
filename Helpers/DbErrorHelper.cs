using System.Windows;
using Microsoft.EntityFrameworkCore;
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
        if (FindPostgres(ex) is PostgresException pg)
        {
            return pg.SqlState switch
            {
                "42501" => "Недостаточно прав для удаления или изменения. " +
                           "Попросите администратора БД выдать роли DELETE на эту таблицу " +
                           "(см. Database/grant_curator_delete.sql).",
                "23503" => "Нельзя удалить: на эту запись ссылаются другие таблицы " +
                           "(экспонаты, билеты, связи выставки и т.д.). " +
                           "Сначала удалите или измените связанные записи.",
                "23505" => "Такая запись уже существует.",
                "22001" => "Слишком длинное значение для поля.",
                _ => "Ошибка базы данных: " + pg.MessageText
            };
        }

        if (ex is FormatException)
            return ex.Message;

        if (ex is DbUpdateException)
            return "Не удалось сохранить изменения в базе данных. " +
                   "Чаще всего это отсутствие прав DELETE или связанные записи в других таблицах.";

        return ex.Message;
    }

    private static PostgresException? FindPostgres(Exception ex)
    {
        for (var e = ex; e != null; e = e.InnerException)
        {
            if (e is PostgresException pg)
                return pg;
        }

        return null;
    }
}
