using System;
using System.Collections.Generic;

namespace HelpDeskBE.Models;

public partial class Ticket
{
    public int Id { get; set; }

    public string? OpenedBy { get; set; }

    public string? Issue { get; set; }

    public string? ResolvedBy { get; set; }

    public string? Resolution { get; set; }

    public bool? Resolved { get; set; }

    public bool? Favorited { get; set; }

    //public virtual ICollection<Favorite> Favorites { get; } = new List<Favorite>();
}
