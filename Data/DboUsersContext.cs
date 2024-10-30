using Microsoft.EntityFrameworkCore;

namespace UserDataManagementAPI.Data;

public partial class DboUsersContext : DbContext
{
    public DboUsersContext()
    {
    }

    public DboUsersContext(DbContextOptions<DboUsersContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Users> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8mb4_0900_ai_ci")
            .HasCharSet("utf8mb4");
        modelBuilder.Entity<Users>()
            .Property(u => u.Id)
            .ValueGeneratedOnAdd();

        modelBuilder.Entity<Users>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("users");
            entity.Property(e => e.Name)
                .IsRequired();
            entity.Property(e => e.Birthdate)
                .IsRequired();
            entity.Property(e => e.Active)
                .IsRequired()
                .HasDefaultValueSql("'1'");
            entity.Property(e => e.Name).HasMaxLength(45);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
