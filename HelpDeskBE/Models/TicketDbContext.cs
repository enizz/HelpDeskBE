using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace HelpDeskBE.Models;

public partial class TicketDbContext : DbContext
{
    public TicketDbContext()
    {
    }

    public TicketDbContext(DbContextOptions<TicketDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Favorite> Favorites { get; set; }

    public virtual DbSet<Ticket> Tickets { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=localhost;Database=_ticketDb;Trusted_Connection=True;Encrypt=False;TrustServerCertificate=true");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Favorite>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Favorite__3214EC07E11036E1");

            entity.Property(e => e.FavoritedBy).HasMaxLength(30);

            //entity.HasOne(d => d.Ticket).WithMany(p => p.Favorites)
            //.HasForeignKey(d => d.TicketId)
            //.HasConstraintName("FK__Favorites__Ticke__3C69FB99");
            entity.Property(e => e.TicketId);
        });

        modelBuilder.Entity<Ticket>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Tickets__3214EC078F2CC412");

            entity.Property(e => e.Issue).HasMaxLength(300);
            entity.Property(e => e.OpenedBy).HasMaxLength(30);
            entity.Property(e => e.Resolution).HasMaxLength(300);
            entity.Property(e => e.ResolvedBy).HasMaxLength(30);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
