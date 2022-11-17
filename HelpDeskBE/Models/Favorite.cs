using System;
using System.Collections.Generic;

namespace HelpDeskBE.Models;

public partial class Favorite
{
    public int Id { get; set; }

    public string? FavoritedBy { get; set; }

    public int? TicketId { get; set; }

    //public virtual Ticket? Ticket { get; set; }
}
