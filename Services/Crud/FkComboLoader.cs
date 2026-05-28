using System.Windows.Controls;
using MuseumApp.Data;

namespace MuseumApp.Services.Crud;

public static class FkComboLoader
{
    public static bool TryLoad(ComboBox combo, FkReference reference)
    {
        try
        {
            using var context = new MuseumDbContext();
            combo.DisplayMemberPath = "Name";
            combo.SelectedValuePath = "Id";
            combo.ItemsSource = reference switch
            {
                FkReference.Country => context.Countries
                    .Select(c => new { Id = c.IdCountry, Name = c.CountryName }).ToList(),
                FkReference.Address => context.Adresses
                    .Select(a => new { Id = a.IdAddress, Name = $"{a.City}, {a.Street} {a.House}" }).ToList(),
                FkReference.Museum => context.Museums
                    .Select(m => new { Id = m.IdMuseum, Name = m.Name }).ToList(),
                FkReference.Employee => context.Employees
                    .Select(e => new { Id = e.IdEmployee, Name = $"{e.LastName} {e.FirstName} {e.MiddleName}".Trim() }).ToList(),
                FkReference.Branch => context.Branches
                    .Select(b => new { Id = b.IdBranch, Name = b.BranchName }).ToList(),
                FkReference.Position => context.Positions
                    .Select(p => new { Id = p.IdPosition, Name = p.PositionName }).ToList(),
                FkReference.Company => context.Companies
                    .Select(c => new { Id = c.IdCompany, Name = c.CompanyName }).ToList(),
                FkReference.Exhibit => context.Exhibits
                    .Select(e => new { Id = e.IdExhibit, Name = e.Name }).ToList(),
                FkReference.Reason => context.Reasons
                    .Select(r => new { Id = r.IdReason, Name = r.ReasonDescription }).ToList(),
                FkReference.Storage => context.Storages
                    .Select(s => new { Id = s.IdStorage, Name = s.StorageName }).ToList(),
                FkReference.Visitor => context.Visitors
                    .Select(v => new { Id = v.IdVisitor, Name = $"{v.LastName} {v.FirstName} {v.MiddleName}".Trim() }).ToList(),
                FkReference.Event => context.Events
                    .Select(ev => new { Id = ev.IdEvent, Name = ev.EventName }).ToList(),
                FkReference.Exhibition => context.Exhibitions
                    .Select(ex => new { Id = ex.IdExhibition, Name = ex.ExhibitionName }).ToList(),
                FkReference.Shop => context.Shops
                    .Select(s => new { Id = s.IdShop, Name = s.ShopName }).ToList(),
                FkReference.Product => context.Products
                    .Select(p => new { Id = p.IdProduct, Name = p.ProductName }).ToList(),
                _ => []
            };
            return combo.Items.Count > 0;
        }
        catch
        {
            return false;
        }
    }
}
