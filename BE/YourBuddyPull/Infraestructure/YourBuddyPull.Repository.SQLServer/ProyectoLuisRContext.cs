using Microsoft.EntityFrameworkCore;
using YourBuddyPull.Repository.SQLServer.DatabaseModels;

namespace YourBuddyPull.Repository.SQLServer;

public partial class ProyectoLuisRContext : DbContext
{
    public ProyectoLuisRContext()
    {
    }

    public ProyectoLuisRContext(DbContextOptions<ProyectoLuisRContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Exercise> Exercises { get; set; }

    public virtual DbSet<ExerciseRoutine> ExerciseRoutines { get; set; }

    public virtual DbSet<ExerciseType> ExerciseTypes { get; set; }

    public virtual DbSet<MeasurementUnit> MeasurementUnits { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<Routine> Routines { get; set; }

    public virtual DbSet<Session> Sessions { get; set; }

    public virtual DbSet<SessionExercise> SessionExercises { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=LAPTOP-LQTK9RL1;Initial Catalog=Proyecto_Luis_R;Encrypt=False;Trusted_Connection=true");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Exercise>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Exercise__3214EC0746907398");

            entity.ToTable("Exercise");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Description).HasMaxLength(200);
            entity.Property(e => e.ImageUrl).HasMaxLength(200);
            entity.Property(e => e.Name).HasMaxLength(200);
            entity.Property(e => e.VideoUrl)
                .HasMaxLength(200)
                .HasColumnName("VideoURL");

            entity.HasOne(d => d.Type).WithMany(p => p.Exercises)
                .HasForeignKey(d => d.TypeId)
                .HasConstraintName("FK__Exercise__TypeId__300424B4");
        });

        modelBuilder.Entity<ExerciseRoutine>(entity =>
        {
            entity.HasKey(e => new { e.RoutineId, e.ExerciseId }).HasName("PK__Exercise__4CE4AE2822AB89B9");

            entity.ToTable("ExerciseRoutine");

            entity.HasOne(d => d.Exercise).WithMany(p => p.ExerciseRoutines)
                .HasForeignKey(d => d.ExerciseId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__ExerciseR__Exerc__3B75D760");

            entity.HasOne(d => d.Routine).WithMany(p => p.ExerciseRoutines)
                .HasForeignKey(d => d.RoutineId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__ExerciseR__Routi__3A81B327");
        });

        modelBuilder.Entity<ExerciseType>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Exercise__3214EC071458B3DD");

            entity.ToTable("ExerciseType");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Name).HasMaxLength(200);

            entity.HasOne(d => d.Measurement).WithMany(p => p.ExerciseTypes)
                .HasForeignKey(d => d.MeasurementId)
                .HasConstraintName("FK__ExerciseT__Measu__2D27B809");
        });

        modelBuilder.Entity<MeasurementUnit>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Measurem__3214EC07168D651B");

            entity.ToTable("MeasurementUnit");

            entity.Property(e => e.Name).HasMaxLength(200);
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Role__3214EC07A629EE06");

            entity.ToTable("Role");

            entity.HasIndex(e => e.Name, "UQ__Role__737584F6DA63A07E").IsUnique();

            entity.Property(e => e.Name).HasMaxLength(100);
        });

        modelBuilder.Entity<Routine>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Routine__3214EC07C4130A27");

            entity.ToTable("Routine");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.IsEnabled).HasDefaultValueSql("((1))");
            entity.Property(e => e.Name).HasMaxLength(200);

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.RoutineCreatedByNavigations)
                .HasForeignKey(d => d.CreatedBy)
                .HasConstraintName("FK__Routine__Created__37A5467C");

            entity.HasOne(d => d.UserAssigned).WithMany(p => p.RoutineUserAssigneds)
                .HasForeignKey(d => d.UserAssignedId)
                .HasConstraintName("FK__Routine__UserAss__36B12243");
        });

        modelBuilder.Entity<Session>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Session__3214EC07DB363F6F");

            entity.ToTable("Session");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.EndTime).HasColumnType("datetime");
            entity.Property(e => e.StartTime).HasColumnType("datetime");

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.Sessions)
                .HasForeignKey(d => d.CreatedBy)
                .HasConstraintName("FK__Session__Created__32E0915F");
        });

        modelBuilder.Entity<SessionExercise>(entity =>
        {
            entity.HasKey(e => new { e.SessionId, e.ExerciseId }).HasName("PK__SessionE__23F3D8428ABC9532");

            entity.ToTable("SessionExercise");

            entity.HasOne(d => d.Exercise).WithMany(p => p.SessionExercises)
                .HasForeignKey(d => d.ExerciseId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__SessionEx__Exerc__4316F928");

            entity.HasOne(d => d.Session).WithMany(p => p.SessionExercises)
                .HasForeignKey(d => d.SessionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__SessionEx__Sessi__4222D4EF");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__User__3214EC0724E64B60");

            entity.ToTable("User");

            entity.HasIndex(e => e.Email, "UQ__User__A9D105348C75BF13").IsUnique();

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Email).HasMaxLength(200);
            entity.Property(e => e.IsDeleted).HasDefaultValueSql("((0))");
            entity.Property(e => e.LastName).HasMaxLength(200);
            entity.Property(e => e.Name).HasMaxLength(200);

            entity.HasMany(d => d.Roles).WithMany(p => p.Users)
                .UsingEntity<Dictionary<string, object>>(
                    "UserRole",
                    r => r.HasOne<Role>().WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__UserRole__RoleId__3F466844"),
                    l => l.HasOne<User>().WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__UserRole__UserId__3E52440B"),
                    j =>
                    {
                        j.HasKey("UserId", "RoleId").HasName("PK__UserRole__AF2760AD54796384");
                        j.ToTable("UserRole");
                    });
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
