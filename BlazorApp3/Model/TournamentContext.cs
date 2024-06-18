using System;
using System.Collections.Generic;
using BlazerApp1.Services;
using Microsoft.EntityFrameworkCore;

namespace BlazorApp3.Model;

public partial class TournamentContext : DbContext, IUnitOfWork
{
    public TournamentContext()
    {
    }

    public TournamentContext(DbContextOptions<TournamentContext> options)
        : base(options)
    {
    }

    public virtual DbSet<ElmahError> ElmahErrors { get; set; }

    public virtual DbSet<League> Leagues { get; set; }

    public virtual DbSet<Match> Matches { get; set; }

    public virtual DbSet<Membership> Memberships { get; set; }

    public virtual DbSet<Player> Players { get; set; }

    public virtual DbSet<RinkOrder> RinkOrders { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<Schedule> Schedules { get; set; }

    public virtual DbSet<Team> Teams { get; set; }

    public virtual DbSet<TeamsView> TeamsViews { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<UserRole> UserRoles { get; set; }

    public virtual DbSet<UserLeague> UserLeagues { get; set; }

    public virtual DbSet<UserLeagueView> UserLeagueViews { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=newdell\\SQLEXPRESS;Database=NewTournament;Trusted_Connection=True;Password=burnt#end1;User ID=AdminUser;Encrypt=False;TrustServerCertificate=False;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ElmahError>(entity =>
        {
            entity.HasKey(e => e.ErrorId).IsClustered(false);

            entity.ToTable("ELMAH_Error");

            entity.Property(e => e.ErrorId).HasDefaultValueSql("(newid())");
            entity.Property(e => e.AllXml).HasColumnType("ntext");
            entity.Property(e => e.Application).HasMaxLength(60);
            entity.Property(e => e.Host).HasMaxLength(50);
            entity.Property(e => e.Message).HasMaxLength(500);
            entity.Property(e => e.Sequence).ValueGeneratedOnAdd();
            entity.Property(e => e.Source).HasMaxLength(60);
            entity.Property(e => e.TimeUtc).HasColumnType("datetime");
            entity.Property(e => e.Type).HasMaxLength(100);
            entity.Property(e => e.User).HasMaxLength(50);
        });

        modelBuilder.Entity<League>(entity =>
        {
            entity.ToTable("League");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Active).HasDefaultValue(true);
            entity.Property(e => e.ByePoints).HasDefaultValue((short)1);
            entity.Property(e => e.Divisions).HasDefaultValue((short)1);
            entity.Property(e => e.LeagueName)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.PointsLimit).HasDefaultValue(true);
            entity.Property(e => e.Rowversion)
                .IsRowVersion()
                .IsConcurrencyToken()
                .HasColumnName("rowversion");
            entity.Property(e => e.StartWeek).HasDefaultValue(1);
            entity.Property(e => e.TiePoints).HasDefaultValue((short)1);
            entity.Property(e => e.WinPoints).HasDefaultValue((short)1);

            entity.HasOne(d => d.StartWeekNavigation).WithMany(p => p.Leagues)
                .HasForeignKey(d => d.StartWeek)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_League_RinkOrder");
        });

        modelBuilder.Entity<Match>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_Schedule");

            entity.ToTable("Match");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Rowversion)
                .IsRowVersion()
                .IsConcurrencyToken()
                .HasColumnName("rowversion");

            entity.HasOne(d => d.TeamNo1Navigation).WithMany(p => p.MatchTeamNo1Navigations)
                .HasForeignKey(d => d.TeamNo1)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Match_Team");

            entity.HasOne(d => d.TeamNo2Navigation).WithMany(p => p.MatchTeamNo2Navigations)
                .HasForeignKey(d => d.TeamNo2)
                .HasConstraintName("FK_Match_Team1");

            entity.HasOne(d => d.Week).WithMany(p => p.Matches)
                .HasForeignKey(d => d.WeekId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Match_Schedule");
        });

        modelBuilder.Entity<Membership>(entity =>
        {
            entity.ToTable("Membership");

            entity.HasIndex(e => new { e.LastName, e.FirstName }, "IX_Membership").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.FirstName)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.FullName)
                .HasMaxLength(101)
                .IsUnicode(false)
                .HasComputedColumnSql("(([FirstName]+' ')+[Lastname])", false);
            entity.Property(e => e.LastName)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.NickName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasComputedColumnSql("(case when isnull([shortname],'')='' then [firstname] else [shortname] end)", false);
            entity.Property(e => e.Rowversion)
                .IsRowVersion()
                .IsConcurrencyToken()
                .HasColumnName("rowversion");
            entity.Property(e => e.Shortname)
                .HasMaxLength(25)
                .IsUnicode(false)
                .HasColumnName("shortname");
        });

        modelBuilder.Entity<UserRole>(entity =>
        {
            entity.HasKey(e => new { e.RoleId, e.UserId }).HasName("PK_UserRole");

            entity.ToTable("UserRole");

            entity.HasOne(d => d.Role).WithMany(p => p.RolesNavigator)
                .HasForeignKey(d => d.RoleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_UserRole_UserRole");

            entity.HasOne(d => d.User).WithMany(p => p.UsersNavigator)
               .HasForeignKey(d => d.UserId)
               .OnDelete(DeleteBehavior.ClientSetNull)
               .HasConstraintName("FK_UserRole_Role");
        });

        modelBuilder.Entity<Player>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_Players");

            entity.ToTable("Player");

            entity.HasIndex(e => new { e.MembershipId, e.Leagueid }, "IX_Player").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Rowversion)
                .IsRowVersion()
                .IsConcurrencyToken()
                .HasColumnName("rowversion");

            entity.HasOne(d => d.League).WithMany(p => p.Players)
                .HasForeignKey(d => d.Leagueid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Player__leagueid__4E88ABD4");

            entity.HasOne(d => d.Membership).WithMany(p => p.Players)
                .HasForeignKey(d => d.MembershipId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Player_Membership");
        });

        modelBuilder.Entity<RinkOrder>(entity =>
        {
            entity.ToTable("RinkOrder");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.Boundary)
                .HasMaxLength(25)
                .IsUnicode(false);
            entity.Property(e => e.Direction)
                .HasMaxLength(25)
                .IsUnicode(false);
            entity.Property(e => e.Green)
                .HasMaxLength(25)
                .IsUnicode(false);
            entity.Property(e => e.Rowversion)
                .IsRowVersion()
                .IsConcurrencyToken()
                .HasColumnName("rowversion");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.ToTable("Role");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.Name)
                .HasMaxLength(450)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Schedule>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_Schedule_1");

            entity.ToTable("Schedule");

            entity.HasIndex(e => new { e.Leagueid, e.GameDate }, "IX_Schedule_1").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Rowversion)
                .IsRowVersion()
                .IsConcurrencyToken()
                .HasColumnName("rowversion");
            entity.Property(e => e.WeekDate)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasComputedColumnSql("(CONVERT([varchar](10),[GameDate],(101)))", false);

            entity.HasOne(d => d.League).WithMany(p => p.Schedules)
                .HasForeignKey(d => d.Leagueid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Schedule__League__4F7CD00D");
        });

        modelBuilder.Entity<Team>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_");

            entity.ToTable("Team");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.DivisionId).HasDefaultValue((short)1);
            entity.Property(e => e.Rowversion)
                .IsRowVersion()
                .IsConcurrencyToken()
                .HasColumnName("rowversion");

            entity.HasOne(d => d.LeadNavigation).WithMany(p => p.TeamLeadNavigations)
                .HasForeignKey(d => d.Lead)
                .HasConstraintName("FK__Players2");

            entity.HasOne(d => d.League).WithMany(p => p.Teams)
                .HasForeignKey(d => d.Leagueid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Team__leagueid__4D94879B");

            entity.HasOne(d => d.SkipNavigation).WithMany(p => p.TeamSkipNavigations)
                .HasForeignKey(d => d.Skip)
                .HasConstraintName("FK__Players");

            entity.HasOne(d => d.ViceSkipNavigation).WithMany(p => p.TeamViceSkipNavigations)
                .HasForeignKey(d => d.ViceSkip)
                .HasConstraintName("FK__Players1");
        });

        modelBuilder.Entity<TeamsView>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("TeamsView");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Lead)
                .HasMaxLength(101)
                .IsUnicode(false);
            entity.Property(e => e.Skip)
                .HasMaxLength(101)
                .IsUnicode(false);
            entity.Property(e => e.ViceSkip)
                .HasMaxLength(101)
                .IsUnicode(false);
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.ToTable("User");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.DisplayName)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.Password)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.SerialNumber)
                .HasMaxLength(450)
                .IsUnicode(false);
            entity.Property(e => e.Username)
                .HasMaxLength(450)
                .IsUnicode(false);

            //entity.HasMany(d => d.Roles).WithMany(p => p.Users)
            //    .UsingEntity<Dictionary<string, object>>(
            //        "UserRole",
            //        r => r.HasOne<Role>().WithMany()
            //            .HasForeignKey("RoleId")
            //            .OnDelete(DeleteBehavior.ClientSetNull)
            //            .HasConstraintName("FK_UserRole_Role"),
            //        l => l.HasOne<User>().WithMany()
            //            .HasForeignKey("UserId")
            //            .OnDelete(DeleteBehavior.ClientSetNull)
            //            .HasConstraintName("FK_UserRole_UserRole"),
            //        j =>
            //        {
            //            j.HasKey("UserId", "RoleId");
            //            j.ToTable("UserRole");
            //        });
        });

        modelBuilder.Entity<UserLeague>(entity =>
        {
            entity.ToTable("UserLeague");

            entity.HasIndex(e => new { e.UserId, e.LeagueId }, "IX_UserLeague").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Roles)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Rowversion)
                .IsRowVersion()
                .IsConcurrencyToken()
                .HasColumnName("rowversion");

            entity.HasOne(d => d.League).WithMany(p => p.UserLeagues)
                .HasForeignKey(d => d.LeagueId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_UserLeague_League");

            entity.HasOne(d => d.User).WithMany(p => p.UserLeagues)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_UserLeague_User");
        });

        modelBuilder.Entity<UserLeagueView>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("UserLeagueView");

            entity.Property(e => e.Active).HasColumnName("active");
            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.LeagueName)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.LeagueRole)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("league role");
            entity.Property(e => e.SiteRole)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("site role");
            entity.Property(e => e.Username)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("username");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
