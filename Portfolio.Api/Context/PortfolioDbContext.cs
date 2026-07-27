using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Portfolio.Api.Models;

namespace Portfolio.Api.Context;

public class PortfolioDbContext : DbContext
{
    public PortfolioDbContext(DbContextOptions<PortfolioDbContext> options)
        : base(options) { }

    // -----------------------------
    // DbSets (All 10 Models)
    // -----------------------------
    public DbSet<PersonalDetail> PersonalDetails { get; set; }
    public DbSet<Certification> Certifications { get; set; }
    public DbSet<Experience> Experiences { get; set; }
    public DbSet<Qualification> Qualifications { get; set; }

    public DbSet<SkillAccordion> SkillAccordions { get; set; }
    public DbSet<Skill> Skills { get; set; }

    public DbSet<Project> Projects { get; set; }
    public DbSet<ProjectDuty> ProjectDuties { get; set; }
    public DbSet<ProjectKeySkill> ProjectKeySkills { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // -----------------------------
        // DateOnly Converter (SQLite)
        // -----------------------------
        var dateOnlyConverter = new ValueConverter<DateOnly, string>(
            d => d.ToString("yyyy-MM-dd"),
            s => DateOnly.Parse(s)
        );

        modelBuilder.Entity<Certification>().Property(c => c.Date).HasConversion(dateOnlyConverter);

        modelBuilder
            .Entity<Experience>()
            .Property(e => e.StartDate)
            .HasConversion(dateOnlyConverter);

        modelBuilder.Entity<Experience>().Property(e => e.EndDate).HasConversion(dateOnlyConverter);

        modelBuilder
            .Entity<Qualification>()
            .Property(q => q.StartDate)
            .HasConversion(dateOnlyConverter);

        modelBuilder
            .Entity<Qualification>()
            .Property(q => q.EndDate)
            .HasConversion(dateOnlyConverter);

        // -----------------------------
        // Skill Relationships
        // -----------------------------
        modelBuilder
            .Entity<SkillAccordion>()
            .HasMany(c => c.Skills)
            .WithOne(s => s.SkillAccordion)
            .HasForeignKey(s => s.SkillAccordionId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<SkillAccordion>().HasIndex(a => a.Name).IsUnique();
        //modelBuilder.Entity<Skill>().HasIndex(s => s.Name).IsUnique();
        // -----------------------------
        // Project Relationships
        // -----------------------------
        modelBuilder
            .Entity<Project>()
            .HasMany(p => p.Duties)
            .WithOne(d => d.Project)
            .HasForeignKey(d => d.ProjectId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder
            .Entity<Project>()
            .HasMany(p => p.KeySkills)
            .WithOne(ks => ks.Project)
            .HasForeignKey(ks => ks.ProjectId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder
            .Entity<ProjectKeySkill>()
            .HasOne(ks => ks.Skill)
            .WithMany()
            .HasForeignKey(ks => ks.SkillId)
            .OnDelete(DeleteBehavior.Restrict);

        // -----------------------------
        // Unique Constraints (Optional but Recommended)
        // -----------------------------
        modelBuilder.Entity<Project>().HasIndex(p => p.ProjectCode).IsUnique();

        modelBuilder.Entity<Qualification>().HasIndex(q => q.Qid).IsUnique();

        modelBuilder.Entity<PersonalDetail>().HasIndex(p => p.Email).IsUnique();
    }
}
