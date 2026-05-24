using System;
using System.Collections.Generic;

namespace MuseumApp.Data.Entities;

public partial class Review
{
    public int IdReview { get; set; }

    public int IdVisitor { get; set; }

    public int? IdExhibition { get; set; }

    public int Rating { get; set; }

    public string? Comment { get; set; }

    public DateOnly ReviewDate { get; set; }

    public virtual Exhibition? IdExhibitionNavigation { get; set; }

    public virtual Visitor IdVisitorNavigation { get; set; } = null!;
}
