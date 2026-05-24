using System.Windows;
using MuseumApp.Data;
using MuseumApp.Data.Entities;

namespace MuseumApp.Views;

public partial class MainWindow
{
    private void DelExhibitionContext(object sender, RoutedEventArgs e)
    {
        if (dg_exhibitions.SelectedItem is Exhibition selected)
        {
            var result = MessageBox.Show(
                $"Удалить выставку {selected.ExhibitionName}?",
                "Подтверждение удаления", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                try
                {
                    using (var context = new MuseumDbContext())
                    {
                        var item = context.Exhibitions.Find(selected.IdExhibition);
                        if (item != null)
                        {
                            context.Exhibitions.Remove(item);
                            context.SaveChanges();
                            Load();
                            MessageBox.Show("Удалено");
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка: " + ex.Message);
                }
            }
        }
        else
        {
            MessageBox.Show("Выберите выставку для удаления.");
        }
    }

    private void DelExhibitContext(object sender, RoutedEventArgs e)
    {
        if (dg_exhibits.SelectedItem is Exhibit selected)
        {
            var result = MessageBox.Show(
                $"Удалить экспонат {selected.Name}?",
                "Подтверждение удаления", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                try
                {
                    using (var context = new MuseumDbContext())
                    {
                        var item = context.Exhibits.Find(selected.IdExhibit);
                        if (item != null)
                        {
                            context.Exhibits.Remove(item);
                            context.SaveChanges();
                            Load();
                            MessageBox.Show("Удалено");
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка: " + ex.Message);
                }
            }
        }
        else
        {
            MessageBox.Show("Выберите экспонат для удаления.");
        }
    }

    private void DelCollectionContext(object sender, RoutedEventArgs e)
    {
        if (dg_collections.SelectedItem is Collection selected)
        {
            var result = MessageBox.Show(
                $"Удалить коллекцию {selected.CollectionName}?",
                "Подтверждение удаления", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                try
                {
                    using (var context = new MuseumDbContext())
                    {
                        var item = context.Collections.Find(selected.IdCollection);
                        if (item != null)
                        {
                            context.Collections.Remove(item);
                            context.SaveChanges();
                            Load();
                            MessageBox.Show("Удалено");
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка: " + ex.Message);
                }
            }
        }
        else
        {
            MessageBox.Show("Выберите коллекцию для удаления.");
        }
    }

    private void DelHallContext(object sender, RoutedEventArgs e)
    {
        if (dg_halls.SelectedItem is Hall selected)
        {
            var result = MessageBox.Show(
                $"Удалить зал {selected.HallName}?",
                "Подтверждение удаления", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                try
                {
                    using (var context = new MuseumDbContext())
                    {
                        var item = context.Halls.Find(selected.IdHall);
                        if (item != null)
                        {
                            context.Halls.Remove(item);
                            context.SaveChanges();
                            Load();
                            MessageBox.Show("Удалено");
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка: " + ex.Message);
                }
            }
        }
        else
        {
            MessageBox.Show("Выберите зал для удаления.");
        }
    }

    private void DelAuthorContext(object sender, RoutedEventArgs e)
    {
        if (dg_authors.SelectedItem is Author selected)
        {
            var result = MessageBox.Show(
                $"Удалить автора {selected.LastName} {selected.FirstName}?",
                "Подтверждение удаления", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                try
                {
                    using (var context = new MuseumDbContext())
                    {
                        var item = context.Authors.Find(selected.IdAuthor);
                        if (item != null)
                        {
                            context.Authors.Remove(item);
                            context.SaveChanges();
                            Load();
                            MessageBox.Show("Удалено");
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка: " + ex.Message);
                }
            }
        }
        else
        {
            MessageBox.Show("Выберите автора для удаления.");
        }
    }

    private void DelVisitorContext(object sender, RoutedEventArgs e)
    {
        if (dg_visitors.SelectedItem is Visitor selected)
        {
            var result = MessageBox.Show(
                $"Удалить посетителя {selected.LastName} {selected.FirstName}?",
                "Подтверждение удаления", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                try
                {
                    using (var context = new MuseumDbContext())
                    {
                        var item = context.Visitors.Find(selected.IdVisitor);
                        if (item != null)
                        {
                            context.Visitors.Remove(item);
                            context.SaveChanges();
                            Load();
                            MessageBox.Show("Удалено");
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка: " + ex.Message);
                }
            }
        }
        else
        {
            MessageBox.Show("Выберите посетителя для удаления.");
        }
    }

    private void DelExhibitionTicketContext(object sender, RoutedEventArgs e)
    {
        if (dg_exhibitionTickets.SelectedItem is ExhibitionTicket selected)
        {
            var result = MessageBox.Show(
                $"Удалить билет (посетитель {selected.IdVisitor}, выставка {selected.IdExhibition})?",
                "Подтверждение удаления", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                try
                {
                    using (var context = new MuseumDbContext())
                    {
                        var item = context.ExhibitionTickets.Find(selected.IdExhibition, selected.IdVisitor);
                        if (item != null)
                        {
                            context.ExhibitionTickets.Remove(item);
                            context.SaveChanges();
                            Load();
                            MessageBox.Show("Удалено");
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка: " + ex.Message);
                }
            }
        }
        else
        {
            MessageBox.Show("Выберите билет для удаления.");
        }
    }

    private void DelExcursionTicketContext(object sender, RoutedEventArgs e)
    {
        if (dg_excursionTickets.SelectedItem is ExcursionTicket selected)
        {
            var result = MessageBox.Show(
                $"Удалить билет (посетитель {selected.IdVisitor}, экскурсия {selected.IdExcursion})?",
                "Подтверждение удаления", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                try
                {
                    using (var context = new MuseumDbContext())
                    {
                        var item = context.ExcursionTickets.Find(selected.IdVisitor, selected.IdExcursion);
                        if (item != null)
                        {
                            context.ExcursionTickets.Remove(item);
                            context.SaveChanges();
                            Load();
                            MessageBox.Show("Удалено");
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка: " + ex.Message);
                }
            }
        }
        else
        {
            MessageBox.Show("Выберите билет для удаления.");
        }
    }

    private void DelExcursionContext(object sender, RoutedEventArgs e)
    {
        if (dg_excursions.SelectedItem is Excursion selected)
        {
            var result = MessageBox.Show(
                $"Удалить экскурсию #{selected.IdExcursion}?",
                "Подтверждение удаления", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                try
                {
                    using (var context = new MuseumDbContext())
                    {
                        var item = context.Excursions.Find(selected.IdExcursion);
                        if (item != null)
                        {
                            context.Excursions.Remove(item);
                            context.SaveChanges();
                            Load();
                            MessageBox.Show("Удалено");
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка: " + ex.Message);
                }
            }
        }
        else
        {
            MessageBox.Show("Выберите экскурсию для удаления.");
        }
    }

    private void DelPrivilegeContext(object sender, RoutedEventArgs e)
    {
        if (dg_privileges.SelectedItem is Privilege selected)
        {
            var result = MessageBox.Show(
                $"Удалить льготу {selected.PrivilegeName}?",
                "Подтверждение удаления", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                try
                {
                    using (var context = new MuseumDbContext())
                    {
                        var item = context.Privileges.Find(selected.IdPrivilege);
                        if (item != null)
                        {
                            context.Privileges.Remove(item);
                            context.SaveChanges();
                            Load();
                            MessageBox.Show("Удалено");
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка: " + ex.Message);
                }
            }
        }
        else
        {
            MessageBox.Show("Выберите льготу для удаления.");
        }
    }

    private void DelEmployeeContext(object sender, RoutedEventArgs e)
    {
        if (dg_employees.SelectedItem is Employee selected)
        {
            var result = MessageBox.Show(
                $"Удалить сотрудника {selected.LastName} {selected.FirstName}?",
                "Подтверждение удаления", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                try
                {
                    using (var context = new MuseumDbContext())
                    {
                        var item = context.Employees.Find(selected.IdEmployee);
                        if (item != null)
                        {
                            context.Employees.Remove(item);
                            context.SaveChanges();
                            Load();
                            MessageBox.Show("Удалено");
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка: " + ex.Message);
                }
            }
        }
        else
        {
            MessageBox.Show("Выберите сотрудника для удаления.");
        }
    }

    private void DelEventContext(object sender, RoutedEventArgs e)
    {
        if (dg_events.SelectedItem is Event selected)
        {
            var result = MessageBox.Show(
                $"Удалить мероприятие {selected.EventName}?",
                "Подтверждение удаления", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                try
                {
                    using (var context = new MuseumDbContext())
                    {
                        var item = context.Events.Find(selected.IdEvent);
                        if (item != null)
                        {
                            context.Events.Remove(item);
                            context.SaveChanges();
                            Load();
                            MessageBox.Show("Удалено");
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка: " + ex.Message);
                }
            }
        }
        else
        {
            MessageBox.Show("Выберите мероприятие для удаления.");
        }
    }
}
