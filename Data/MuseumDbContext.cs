using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using MuseumApp.Data.Entities;

namespace MuseumApp.Data;

public partial class MuseumDbContext : DbContext
{
    public MuseumDbContext(DbContextOptions<MuseumDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Adress> Adresses { get; set; }

    public virtual DbSet<Author> Authors { get; set; }

    public virtual DbSet<Branch> Branches { get; set; }

    public virtual DbSet<Collection> Collections { get; set; }

    public virtual DbSet<Company> Companies { get; set; }

    public virtual DbSet<Country> Countries { get; set; }

    public virtual DbSet<Employee> Employees { get; set; }

    public virtual DbSet<Event> Events { get; set; }

    public virtual DbSet<EventTicket> EventTickets { get; set; }

    public virtual DbSet<Excursion> Excursions { get; set; }

    public virtual DbSet<ExcursionTicket> ExcursionTickets { get; set; }

    public virtual DbSet<Exhibit> Exhibits { get; set; }

    public virtual DbSet<ExhibitCondition> ExhibitConditions { get; set; }

    public virtual DbSet<ExhibitMovement> ExhibitMovements { get; set; }

    public virtual DbSet<Exhibition> Exhibitions { get; set; }

    public virtual DbSet<ExhibitionTicket> ExhibitionTickets { get; set; }

    public virtual DbSet<Hall> Halls { get; set; }

    public virtual DbSet<Inventory> Inventories { get; set; }

    public virtual DbSet<Material> Materials { get; set; }

    public virtual DbSet<Museum> Museums { get; set; }

    public virtual DbSet<Position> Positions { get; set; }

    public virtual DbSet<Privilege> Privileges { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<Reason> Reasons { get; set; }

    public virtual DbSet<Restoration> Restorations { get; set; }

    public virtual DbSet<Review> Reviews { get; set; }

    public virtual DbSet<Shop> Shops { get; set; }

    public virtual DbSet<Storage> Storages { get; set; }

    public virtual DbSet<TestIvanov> TestIvanovs { get; set; }

    public virtual DbSet<Visitor> Visitors { get; set; }

    public virtual DbSet<VwActiveExhibition> VwActiveExhibitions { get; set; }

    public virtual DbSet<VwEmployeeDuty> VwEmployeeDuties { get; set; }

    public virtual DbSet<VwExhibitFullInfo> VwExhibitFullInfos { get; set; }

    public virtual DbSet<VwProductStock> VwProductStocks { get; set; }

    public virtual DbSet<VwStorageOccupancy> VwStorageOccupancies { get; set; }

    public virtual DbSet<VwVisitorHistory> VwVisitorHistories { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Adress>(entity =>
        {
            entity.HasKey(e => e.IdAddress).HasName("adresses_pkey");

            entity.ToTable("adresses");

            entity.Property(e => e.IdAddress).HasColumnName("id_address");
            entity.Property(e => e.City)
                .HasMaxLength(45)
                .HasColumnName("city");
            entity.Property(e => e.House)
                .HasMaxLength(45)
                .HasColumnName("house");
            entity.Property(e => e.PostalCode)
                .HasMaxLength(45)
                .HasDefaultValueSql("NULL::character varying")
                .HasColumnName("postal_code");
            entity.Property(e => e.Street)
                .HasMaxLength(45)
                .HasColumnName("street");
        });

        modelBuilder.Entity<Author>(entity =>
        {
            entity.HasKey(e => e.IdAuthor).HasName("authors_pkey");

            entity.ToTable("authors");

            entity.Property(e => e.IdAuthor).HasColumnName("id_author");
            entity.Property(e => e.FirstName)
                .HasMaxLength(45)
                .HasColumnName("first_name");
            entity.Property(e => e.IdCountry).HasColumnName("id_country");
            entity.Property(e => e.LastName)
                .HasMaxLength(45)
                .HasColumnName("last_name");
            entity.Property(e => e.MiddleName)
                .HasMaxLength(45)
                .HasDefaultValueSql("NULL::character varying")
                .HasColumnName("middle_name");

            entity.HasOne(d => d.IdCountryNavigation).WithMany(p => p.Authors)
                .HasForeignKey(d => d.IdCountry)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FKaut");

        });

        modelBuilder.Entity<Branch>(entity =>
        {
            entity.HasKey(e => e.IdBranch).HasName("branches_pkey");

            entity.ToTable("branches");

            entity.Property(e => e.IdBranch).HasColumnName("id_branch");
            entity.Property(e => e.BranchName)
                .HasMaxLength(45)
                .HasColumnName("branch_name");
            entity.Property(e => e.IdAddress).HasColumnName("id_address");
            entity.Property(e => e.IdMuseum).HasColumnName("id_museum");
            entity.Property(e => e.IdResponsible).HasColumnName("id_responsible");
            entity.Property(e => e.Phone)
                .HasMaxLength(45)
                .HasDefaultValueSql("NULL::character varying")
                .HasColumnName("phone");

            entity.HasOne(d => d.IdAddressNavigation).WithMany(p => p.Branches)
                .HasForeignKey(d => d.IdAddress)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FKbr2");

            entity.HasOne(d => d.IdMuseumNavigation).WithMany(p => p.Branches)
                .HasForeignKey(d => d.IdMuseum)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FKbr1");

            entity.HasOne(d => d.IdResponsibleNavigation).WithMany(p => p.Branches)
                .HasForeignKey(d => d.IdResponsible)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FKbr3");
        });

        modelBuilder.Entity<Collection>(entity =>
        {
            entity.HasKey(e => e.IdCollection).HasName("collections_pkey");

            entity.ToTable("collections");

            entity.Property(e => e.IdCollection).HasColumnName("id_collection");
            entity.Property(e => e.CollectionName)
                .HasMaxLength(255)
                .HasColumnName("collection_name");
            entity.Property(e => e.Description)
                .HasMaxLength(255)
                .HasColumnName("description");
            entity.Property(e => e.IdKeeper).HasColumnName("id_keeper");

            entity.HasOne(d => d.IdKeeperNavigation).WithMany(p => p.Collections)
                .HasForeignKey(d => d.IdKeeper)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FKcol");
        });

        modelBuilder.Entity<Company>(entity =>
        {
            entity.HasKey(e => e.IdCompany).HasName("companies_pkey");

            entity.ToTable("companies");

            entity.Property(e => e.IdCompany).HasColumnName("id_company");
            entity.Property(e => e.CompanyName)
                .HasMaxLength(255)
                .HasColumnName("company_name");
            entity.Property(e => e.ContactEmail)
                .HasMaxLength(45)
                .HasColumnName("contact_email");
            entity.Property(e => e.ContactPhone)
                .HasMaxLength(45)
                .HasColumnName("contact_phone");
            entity.Property(e => e.Inn)
                .HasMaxLength(20)
                .HasColumnName("inn");
            entity.Property(e => e.LegalAddress).HasColumnName("legal_address");
        });

        modelBuilder.Entity<Country>(entity =>
        {
            entity.HasKey(e => e.IdCountry).HasName("countries_pkey");

            entity.ToTable("countries");

            entity.Property(e => e.IdCountry).HasColumnName("id_country");
            entity.Property(e => e.CountryName)
                .HasMaxLength(255)
                .HasColumnName("country_name");
        });

        modelBuilder.Entity<Employee>(entity =>
        {
            entity.HasKey(e => e.IdEmployee).HasName("employees_pkey");

            entity.ToTable("employees");

            entity.Property(e => e.IdEmployee).HasColumnName("id_employee");
            entity.Property(e => e.BirthDate).HasColumnName("birth_date");
            entity.Property(e => e.EducationLevel)
                .HasMaxLength(45)
                .HasDefaultValueSql("NULL::character varying")
                .HasColumnName("education_level");
            entity.Property(e => e.FirstName)
                .HasMaxLength(45)
                .HasColumnName("first_name");
            entity.Property(e => e.IdPosition).HasColumnName("id_position");
            entity.Property(e => e.LastName)
                .HasMaxLength(45)
                .HasColumnName("last_name");
            entity.Property(e => e.MiddleName)
                .HasMaxLength(45)
                .HasColumnName("middle_name");
            entity.Property(e => e.Phone)
                .HasMaxLength(45)
                .HasDefaultValueSql("NULL::character varying")
                .HasColumnName("phone");

            entity.HasOne(d => d.IdPositionNavigation).WithMany(p => p.Employees)
                .HasForeignKey(d => d.IdPosition)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FKpos");
        });

        modelBuilder.Entity<Event>(entity =>
        {
            entity.HasKey(e => e.IdEvent).HasName("events_pkey");

            entity.ToTable("events");

            entity.Property(e => e.IdEvent).HasColumnName("id_event");
            entity.Property(e => e.DurationMinutes).HasColumnName("duration_minutes");
            entity.Property(e => e.EventDate).HasColumnName("event_date");
            entity.Property(e => e.EventName)
                .HasMaxLength(255)
                .HasColumnName("event_name");
            entity.Property(e => e.IdBranch).HasColumnName("id_branch");
            entity.Property(e => e.IdEmployee).HasColumnName("id_employee");
            entity.Property(e => e.Price)
                .HasPrecision(10, 2)
                .HasColumnName("price");
            entity.Property(e => e.StartTime).HasColumnName("start_time");

            entity.HasOne(d => d.IdBranchNavigation).WithMany(p => p.Events)
                .HasForeignKey(d => d.IdBranch)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("fk_events_branch");

            entity.HasOne(d => d.IdEmployeeNavigation).WithMany(p => p.Events)
                .HasForeignKey(d => d.IdEmployee)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("fk_events_employee");
        });

        modelBuilder.Entity<EventTicket>(entity =>
        {
            entity.HasKey(e => new { e.IdVisitor, e.IdEvent }).HasName("event_tickets_pkey");

            entity.ToTable("event_tickets");

            entity.Property(e => e.IdVisitor).HasColumnName("id_visitor");
            entity.Property(e => e.IdEvent).HasColumnName("id_event");
            entity.Property(e => e.ActualPrice)
                .HasPrecision(10, 2)
                .HasColumnName("actual_price");
            entity.Property(e => e.PurchaseDate)
                .HasDefaultValueSql("CURRENT_DATE")
                .HasColumnName("purchase_date");

            entity.HasOne(d => d.IdEventNavigation).WithMany(p => p.EventTickets)
                .HasForeignKey(d => d.IdEvent)
                .HasConstraintName("fk_event_tickets_event");

            entity.HasOne(d => d.IdVisitorNavigation).WithMany(p => p.EventTickets)
                .HasForeignKey(d => d.IdVisitor)
                .HasConstraintName("fk_event_tickets_visitor");
        });

        modelBuilder.Entity<Excursion>(entity =>
        {
            entity.HasKey(e => e.IdExcursion).HasName("excursions_pkey");

            entity.ToTable("excursions");

            entity.Property(e => e.IdExcursion).HasColumnName("id_excursion");
            entity.Property(e => e.Duration).HasColumnName("duration");
            entity.Property(e => e.IdCustomer).HasColumnName("id_customer");
            entity.Property(e => e.IdExhibition).HasColumnName("id_exhibition");
            entity.Property(e => e.IdGuide).HasColumnName("id_guide");
            entity.Property(e => e.Price)
                .HasPrecision(10, 2)
                .HasColumnName("price");

            entity.HasOne(d => d.IdCustomerNavigation).WithMany(p => p.Excursions)
                .HasForeignKey(d => d.IdCustomer)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FKex2");

            entity.HasOne(d => d.IdExhibitionNavigation).WithMany(p => p.Excursions)
                .HasForeignKey(d => d.IdExhibition)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FKex3");

            entity.HasOne(d => d.IdGuideNavigation).WithMany(p => p.Excursions)
                .HasForeignKey(d => d.IdGuide)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FKex1");
        });

        modelBuilder.Entity<ExcursionTicket>(entity =>
        {
            entity.HasKey(e => new { e.IdVisitor, e.IdExcursion }).HasName("excursion_tickets_pkey");

            entity.ToTable("excursion_tickets");

            entity.Property(e => e.IdVisitor).HasColumnName("id_visitor");
            entity.Property(e => e.IdExcursion).HasColumnName("id_excursion");
            entity.Property(e => e.ActualCost)
                .HasPrecision(10, 2)
                .HasColumnName("actual_cost");
            entity.Property(e => e.VisitDate).HasColumnName("visit_date");
            entity.Property(e => e.VisitTime).HasColumnName("visit_time");

            entity.HasOne(d => d.IdExcursionNavigation).WithMany(p => p.ExcursionTickets)
                .HasForeignKey(d => d.IdExcursion)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK2exc");

            entity.HasOne(d => d.IdVisitorNavigation).WithMany(p => p.ExcursionTickets)
                .HasForeignKey(d => d.IdVisitor)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FKvis");
        });

        modelBuilder.Entity<Exhibit>(entity =>
        {
            entity.HasKey(e => e.IdExhibit).HasName("exhibits_pkey");

            entity.ToTable("exhibits");

            entity.Property(e => e.IdExhibit)
                .ValueGeneratedNever()
                .HasColumnName("id_exhibit");
            entity.Property(e => e.CreationDate).HasColumnName("creation_date");
            entity.Property(e => e.IdAuthor).HasColumnName("id_author");
            entity.Property(e => e.IdCollection).HasColumnName("id_collection");
            entity.Property(e => e.IdCondition).HasColumnName("id_condition");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .HasColumnName("name");

            entity.HasOne(d => d.IdCollectionNavigation).WithMany(p => p.Exhibits)
                .HasForeignKey(d => d.IdCollection)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FKexbt1");

            entity.HasOne(d => d.IdConditionNavigation).WithMany(p => p.Exhibits)
                .HasForeignKey(d => d.IdCondition)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FKexbt2");
        });

        modelBuilder.Entity<ExhibitCondition>(entity =>
        {
            entity.HasKey(e => e.IdCondition).HasName("exhibit_conditions_pkey");

            entity.ToTable("exhibit_conditions");

            entity.Property(e => e.IdCondition).HasColumnName("id_condition");
            entity.Property(e => e.ConditionName)
                .HasMaxLength(45)
                .HasColumnName("condition_name");
            entity.Property(e => e.Description)
                .HasMaxLength(255)
                .HasColumnName("description");
        });

        modelBuilder.Entity<ExhibitMovement>(entity =>
        {
            entity.HasKey(e => e.IdMovement).HasName("exhibit_movements_pkey");

            entity.ToTable("exhibit_movements");

            entity.Property(e => e.IdMovement).HasColumnName("id_movement");
            entity.Property(e => e.FromStorageId).HasColumnName("from_storage_id");
            entity.Property(e => e.IdExhibit).HasColumnName("id_exhibit");
            entity.Property(e => e.IdReason).HasColumnName("id_reason");
            entity.Property(e => e.IdResponsible).HasColumnName("id_responsible");
            entity.Property(e => e.MovementDate).HasColumnName("movement_date");
            entity.Property(e => e.ToStorageId).HasColumnName("to_storage_id");

            entity.HasOne(d => d.FromStorage).WithMany(p => p.ExhibitMovementFromStorages)
                .HasForeignKey(d => d.FromStorageId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FKem1");

            entity.HasOne(d => d.IdExhibitNavigation).WithMany(p => p.ExhibitMovements)
                .HasForeignKey(d => d.IdExhibit)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FKem4");

            entity.HasOne(d => d.IdReasonNavigation).WithMany(p => p.ExhibitMovements)
                .HasForeignKey(d => d.IdReason)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FKem3");

            entity.HasOne(d => d.IdResponsibleNavigation).WithMany(p => p.ExhibitMovements)
                .HasForeignKey(d => d.IdResponsible)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FKem5");

            entity.HasOne(d => d.ToStorage).WithMany(p => p.ExhibitMovementToStorages)
                .HasForeignKey(d => d.ToStorageId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FKem2");
        });

        modelBuilder.Entity<Exhibition>(entity =>
        {
            entity.HasKey(e => e.IdExhibition).HasName("exhibitions_pkey");

            entity.ToTable("exhibitions");

            entity.Property(e => e.IdExhibition)
                .ValueGeneratedNever()
                .HasColumnName("id_exhibition");
            entity.Property(e => e.EndDate)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("end_date");
            entity.Property(e => e.ExhibitionName)
                .HasMaxLength(255)
                .HasColumnName("exhibition_name");
            entity.Property(e => e.IdCurator).HasColumnName("id_curator");
            entity.Property(e => e.Price)
                .HasPrecision(10, 2)
                .HasColumnName("price");
            entity.Property(e => e.StartDate)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("start_date");
            entity.Property(e => e.Theme)
                .HasMaxLength(255)
                .HasDefaultValueSql("NULL::character varying")
                .HasColumnName("theme");

            entity.HasOne(d => d.IdCuratorNavigation).WithMany(p => p.Exhibitions)
                .HasForeignKey(d => d.IdCurator)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FKexhs");
        });

        modelBuilder.Entity<ExhibitionTicket>(entity =>
        {
            entity.HasKey(e => new { e.IdExhibition, e.IdVisitor }).HasName("exhibition_tickets_pkey");

            entity.ToTable("exhibition_tickets");

            entity.Property(e => e.IdExhibition).HasColumnName("id_exhibition");
            entity.Property(e => e.IdVisitor).HasColumnName("id_visitor");
            entity.Property(e => e.ActualCost)
                .HasPrecision(10, 2)
                .HasColumnName("actual_cost");
            entity.Property(e => e.VisitDate).HasColumnName("visit_date");
            entity.Property(e => e.VisitTime).HasColumnName("visit_time");

            entity.HasOne(d => d.IdExhibitionNavigation).WithMany(p => p.ExhibitionTickets)
                .HasForeignKey(d => d.IdExhibition)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FKext2");

            entity.HasOne(d => d.IdVisitorNavigation).WithMany(p => p.ExhibitionTickets)
                .HasForeignKey(d => d.IdVisitor)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FKext1");
        });

        modelBuilder.Entity<Hall>(entity =>
        {
            entity.HasKey(e => e.IdHall).HasName("halls_pkey");

            entity.ToTable("halls");

            entity.Property(e => e.IdHall).HasColumnName("id_hall");
            entity.Property(e => e.Area).HasColumnName("area");
            entity.Property(e => e.Capacity).HasColumnName("capacity");
            entity.Property(e => e.HallName)
                .HasMaxLength(255)
                .HasColumnName("hall_name");
            entity.Property(e => e.IdBranch).HasColumnName("id_branch");

            entity.HasOne(d => d.IdBranchNavigation).WithMany(p => p.Halls)
                .HasForeignKey(d => d.IdBranch)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FKh");
        });

        modelBuilder.Entity<Inventory>(entity =>
        {
            entity.HasKey(e => new { e.IdShop, e.IdProduct }).HasName("inventory_pkey");

            entity.ToTable("inventory");

            entity.Property(e => e.IdShop).HasColumnName("id_shop");
            entity.Property(e => e.IdProduct).HasColumnName("id_product");
            entity.Property(e => e.Quantity)
                .HasDefaultValue(0)
                .HasColumnName("quantity");

            entity.HasOne(d => d.IdProductNavigation).WithMany(p => p.Inventories)
                .HasForeignKey(d => d.IdProduct)
                .HasConstraintName("fk_inventory_product");

            entity.HasOne(d => d.IdShopNavigation).WithMany(p => p.Inventories)
                .HasForeignKey(d => d.IdShop)
                .HasConstraintName("fk_inventory_shop");
        });

        modelBuilder.Entity<Material>(entity =>
        {
            entity.HasKey(e => e.IdMaterial).HasName("materials_pkey");

            entity.ToTable("materials");

            entity.Property(e => e.IdMaterial).HasColumnName("id_material");
            entity.Property(e => e.Description)
                .HasMaxLength(255)
                .HasDefaultValueSql("NULL::character varying")
                .HasColumnName("description");
            entity.Property(e => e.MaterialName)
                .HasMaxLength(100)
                .HasColumnName("material_name");
        });

        modelBuilder.Entity<Museum>(entity =>
        {
            entity.HasKey(e => e.IdMuseum).HasName("museum_pkey");

            entity.ToTable("museum");

            entity.Property(e => e.IdMuseum).HasColumnName("id_museum");
            entity.Property(e => e.IdAddress).HasColumnName("id_address");
            entity.Property(e => e.IdDirector).HasColumnName("id_director");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .HasColumnName("name");

            entity.HasOne(d => d.IdAddressNavigation).WithMany(p => p.Museums)
                .HasForeignKey(d => d.IdAddress)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK2");

            entity.HasOne(d => d.IdDirectorNavigation).WithMany(p => p.Museums)
                .HasForeignKey(d => d.IdDirector)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK1");
        });

        modelBuilder.Entity<Position>(entity =>
        {
            entity.HasKey(e => e.IdPosition).HasName("positions_pkey");

            entity.ToTable("positions");

            entity.Property(e => e.IdPosition).HasColumnName("id_position");
            entity.Property(e => e.Description)
                .HasMaxLength(255)
                .HasDefaultValueSql("NULL::character varying")
                .HasColumnName("description");
            entity.Property(e => e.PositionName)
                .HasMaxLength(100)
                .HasColumnName("position_name");
            entity.Property(e => e.Salary)
                .HasPrecision(10, 2)
                .HasDefaultValueSql("NULL::numeric")
                .HasColumnName("salary");
        });

        modelBuilder.Entity<Privilege>(entity =>
        {
            entity.HasKey(e => e.IdPrivilege).HasName("privileges_pkey");

            entity.ToTable("privileges");

            entity.Property(e => e.IdPrivilege).HasColumnName("id_privilege");
            entity.Property(e => e.DiscountRate).HasColumnName("discount_rate");
            entity.Property(e => e.PrivilegeName)
                .HasMaxLength(100)
                .HasColumnName("privilege_name");
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.IdProduct).HasName("products_pkey");

            entity.ToTable("products");

            entity.Property(e => e.IdProduct).HasColumnName("id_product");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.IdCompanySupplier).HasColumnName("id_company_supplier");
            entity.Property(e => e.Price)
                .HasPrecision(10, 2)
                .HasColumnName("price");
            entity.Property(e => e.ProductName)
                .HasMaxLength(255)
                .HasColumnName("product_name");

            entity.HasOne(d => d.IdCompanySupplierNavigation).WithMany(p => p.Products)
                .HasForeignKey(d => d.IdCompanySupplier)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("fk_product_company");
        });

        modelBuilder.Entity<Reason>(entity =>
        {
            entity.HasKey(e => e.IdReason).HasName("reasons_pkey");

            entity.ToTable("reasons");

            entity.Property(e => e.IdReason).HasColumnName("id_reason");
            entity.Property(e => e.ReasonDescription)
                .HasMaxLength(255)
                .HasColumnName("reason_description");
        });

        modelBuilder.Entity<Restoration>(entity =>
        {
            entity.HasKey(e => e.IdRestoration).HasName("restorations_pkey");

            entity.ToTable("restorations");

            entity.Property(e => e.IdRestoration).HasColumnName("id_restoration");
            entity.Property(e => e.Cost)
                .HasPrecision(10, 2)
                .HasDefaultValueSql("NULL::numeric")
                .HasColumnName("cost");
            entity.Property(e => e.EndDate)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("end_date");
            entity.Property(e => e.IdExhibit).HasColumnName("id_exhibit");
            entity.Property(e => e.IdRestorer).HasColumnName("id_restorer");
            entity.Property(e => e.StartDate)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("start_date");
            entity.Property(e => e.WorkDescription)
                .HasMaxLength(255)
                .HasDefaultValueSql("NULL::character varying")
                .HasColumnName("work_description");

            entity.HasOne(d => d.IdExhibitNavigation).WithMany(p => p.Restorations)
                .HasForeignKey(d => d.IdExhibit)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FKrest1");

            entity.HasOne(d => d.IdRestorerNavigation).WithMany(p => p.Restorations)
                .HasForeignKey(d => d.IdRestorer)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK2rest2");
        });

        modelBuilder.Entity<Review>(entity =>
        {
            entity.HasKey(e => e.IdReview).HasName("reviews_pkey");

            entity.ToTable("reviews");

            entity.Property(e => e.IdReview).HasColumnName("id_review");
            entity.Property(e => e.Comment).HasColumnName("comment");
            entity.Property(e => e.IdExhibition).HasColumnName("id_exhibition");
            entity.Property(e => e.IdVisitor).HasColumnName("id_visitor");
            entity.Property(e => e.Rating).HasColumnName("rating");
            entity.Property(e => e.ReviewDate)
                .HasDefaultValueSql("CURRENT_DATE")
                .HasColumnName("review_date");

            entity.HasOne(d => d.IdExhibitionNavigation).WithMany(p => p.Reviews)
                .HasForeignKey(d => d.IdExhibition)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("fk_reviews_exhibition");

            entity.HasOne(d => d.IdVisitorNavigation).WithMany(p => p.Reviews)
                .HasForeignKey(d => d.IdVisitor)
                .HasConstraintName("fk_reviews_visitor");
        });

        modelBuilder.Entity<Shop>(entity =>
        {
            entity.HasKey(e => e.IdShop).HasName("shops_pkey");

            entity.ToTable("shops");

            entity.HasIndex(e => e.IdBranch, "shops_id_branch_key").IsUnique();

            entity.Property(e => e.IdShop).HasColumnName("id_shop");
            entity.Property(e => e.Email)
                .HasMaxLength(45)
                .HasColumnName("email");
            entity.Property(e => e.IdBranch).HasColumnName("id_branch");
            entity.Property(e => e.Phone)
                .HasMaxLength(45)
                .HasColumnName("phone");
            entity.Property(e => e.ShopName)
                .HasMaxLength(255)
                .HasColumnName("shop_name");
            entity.Property(e => e.WorkingHours)
                .HasMaxLength(255)
                .HasColumnName("working_hours");

            entity.HasOne(d => d.IdBranchNavigation).WithOne(p => p.Shop)
                .HasForeignKey<Shop>(d => d.IdBranch)
                .HasConstraintName("fk_shop_branch");
        });

        modelBuilder.Entity<Storage>(entity =>
        {
            entity.HasKey(e => e.IdStorage).HasName("storages_pkey");

            entity.ToTable("storages");

            entity.Property(e => e.IdStorage).HasColumnName("id_storage");
            entity.Property(e => e.Humidity)
                .HasPrecision(5, 2)
                .HasDefaultValueSql("NULL::numeric")
                .HasColumnName("humidity");
            entity.Property(e => e.IdBranch).HasColumnName("id_branch");
            entity.Property(e => e.StorageName)
                .HasMaxLength(255)
                .HasColumnName("storage_name");
            entity.Property(e => e.Temperature)
                .HasPrecision(5, 2)
                .HasDefaultValueSql("NULL::numeric")
                .HasColumnName("temperature");

            entity.HasOne(d => d.IdBranchNavigation).WithMany(p => p.Storages)
                .HasForeignKey(d => d.IdBranch)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FKst");
        });

        modelBuilder.Entity<TestIvanov>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("test_ivanov");

            entity.Property(e => e.Id).HasColumnName("id");
        });

        modelBuilder.Entity<Visitor>(entity =>
        {
            entity.HasKey(e => e.IdVisitor).HasName("visitors_pkey");

            entity.ToTable("visitors");

            entity.Property(e => e.IdVisitor).HasColumnName("id_visitor");
            entity.Property(e => e.Email)
                .HasMaxLength(45)
                .HasDefaultValueSql("NULL::character varying")
                .HasColumnName("email");
            entity.Property(e => e.FirstName)
                .HasMaxLength(100)
                .HasColumnName("first_name");
            entity.Property(e => e.IdPrivilege).HasColumnName("id_privilege");
            entity.Property(e => e.LastName)
                .HasMaxLength(100)
                .HasColumnName("last_name");
            entity.Property(e => e.MiddleName)
                .HasMaxLength(100)
                .HasColumnName("middle_name");
            entity.Property(e => e.Phone)
                .HasMaxLength(45)
                .HasDefaultValueSql("NULL::character varying")
                .HasColumnName("phone");

            entity.HasOne(d => d.IdPrivilegeNavigation).WithMany(p => p.Visitors)
                .HasForeignKey(d => d.IdPrivilege)
                .HasConstraintName("FKvisitors");
        });

        modelBuilder.Entity<VwActiveExhibition>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("vw_active_exhibitions");

            entity.Property(e => e.EndDate)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("end_date");
            entity.Property(e => e.ExhibitionName)
                .HasMaxLength(255)
                .HasColumnName("exhibition_name");
            entity.Property(e => e.IdExhibition).HasColumnName("id_exhibition");
            entity.Property(e => e.StartDate)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("start_date");
        });

        modelBuilder.Entity<VwEmployeeDuty>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("vw_employee_duties");

            entity.Property(e => e.BirthDate).HasColumnName("birth_date");
            entity.Property(e => e.CollectionsKept).HasColumnName("collections_kept");
            entity.Property(e => e.EducationLevel)
                .HasMaxLength(45)
                .HasColumnName("education_level");
            entity.Property(e => e.ExcursionsConducted).HasColumnName("excursions_conducted");
            entity.Property(e => e.ExhibitionsCurated).HasColumnName("exhibitions_curated");
            entity.Property(e => e.FullName).HasColumnName("full_name");
            entity.Property(e => e.IdEmployee).HasColumnName("id_employee");
            entity.Property(e => e.Phone)
                .HasMaxLength(45)
                .HasColumnName("phone");
            entity.Property(e => e.PositionName)
                .HasMaxLength(100)
                .HasColumnName("position_name");
        });

        modelBuilder.Entity<VwExhibitFullInfo>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("vw_exhibit_full_info");

            entity.Property(e => e.Author).HasColumnName("author");
            entity.Property(e => e.CollectionName)
                .HasMaxLength(255)
                .HasColumnName("collection_name");
            entity.Property(e => e.ConditionName)
                .HasMaxLength(45)
                .HasColumnName("condition_name");
            entity.Property(e => e.CreationDate).HasColumnName("creation_date");
            entity.Property(e => e.CurrentStorage)
                .HasMaxLength(255)
                .HasColumnName("current_storage");
            entity.Property(e => e.ExhibitName)
                .HasMaxLength(255)
                .HasColumnName("exhibit_name");
            entity.Property(e => e.IdExhibit).HasColumnName("id_exhibit");
            entity.Property(e => e.Materials).HasColumnName("materials");
        });

        modelBuilder.Entity<VwProductStock>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("vw_product_stock");

            entity.Property(e => e.IdProduct).HasColumnName("id_product");
            entity.Property(e => e.IdShop).HasColumnName("id_shop");
            entity.Property(e => e.Price)
                .HasPrecision(10, 2)
                .HasColumnName("price");
            entity.Property(e => e.ProductName)
                .HasMaxLength(255)
                .HasColumnName("product_name");
            entity.Property(e => e.Quantity).HasColumnName("quantity");
            entity.Property(e => e.ShopName)
                .HasMaxLength(255)
                .HasColumnName("shop_name");
            entity.Property(e => e.TotalValue).HasColumnName("total_value");
        });

        modelBuilder.Entity<VwStorageOccupancy>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("vw_storage_occupancy");

            entity.Property(e => e.AvgHumidity).HasColumnName("avg_humidity");
            entity.Property(e => e.AvgTemperature).HasColumnName("avg_temperature");
            entity.Property(e => e.BranchName)
                .HasMaxLength(45)
                .HasColumnName("branch_name");
            entity.Property(e => e.CollectionsHeld).HasColumnName("collections_held");
            entity.Property(e => e.ExhibitCount).HasColumnName("exhibit_count");
            entity.Property(e => e.IdStorage).HasColumnName("id_storage");
            entity.Property(e => e.StorageName)
                .HasMaxLength(255)
                .HasColumnName("storage_name");
        });

        modelBuilder.Entity<VwVisitorHistory>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("vw_visitor_history");

            entity.Property(e => e.AppliedPrivilege)
                .HasMaxLength(100)
                .HasColumnName("applied_privilege");
            entity.Property(e => e.ExhibitionName)
                .HasMaxLength(255)
                .HasColumnName("exhibition_name");
            entity.Property(e => e.IdVisitor).HasColumnName("id_visitor");
            entity.Property(e => e.TicketPrice)
                .HasPrecision(10, 2)
                .HasColumnName("ticket_price");
            entity.Property(e => e.VisitDate).HasColumnName("visit_date");
            entity.Property(e => e.VisitorName).HasColumnName("visitor_name");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
