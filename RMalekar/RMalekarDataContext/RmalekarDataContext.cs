using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MySqlConnector;
using Pomelo.EntityFrameworkCore.MySql.Scaffolding.Internal;
using RMalekarEntityModels;

namespace RMalekarEntityModels;

public partial class RmalekarDataContext : DbContext
{
    public RmalekarDataContext()
    {
    }

    public RmalekarDataContext(DbContextOptions<RmalekarDataContext> options)
        : base(options)
    {
    }

    /** Views **/
    public virtual DbSet<AcademicProject> AcademicProjects { get; set; }

    public virtual DbSet<AcademicQualification> AcademicQualifications { get; set; }

    public virtual DbSet<Allskill> Allskills { get; set; }

    public virtual DbSet<EmploymentHistory> EmploymentHistories { get; set; }

    public virtual DbSet<PortfolioItem> PortfolioItems { get; set; }

    public virtual DbSet<Personalinfo> PersonalInfo { get; set; } = null!;
    /** Views End **/

    /** Tables **/
    public virtual DbSet<EmpDuty> EmpDuties { get; set; }

    public virtual DbSet<EmpHistory> EmpHistories { get; set; }

    public virtual DbSet<EmpKeySkill> EmpKeySkills { get; set; }

    public virtual DbSet<KeySubject> KeySubjects { get; set; }

    public virtual DbSet<PersonalDetail> PersonalDetails { get; set; }

    public virtual DbSet<Project> Projects { get; set; }

    public virtual DbSet<ProjectDuty> ProjectDuties { get; set; }

    public virtual DbSet<ProjectKeySkill> ProjectKeySkills { get; set; }

    public virtual DbSet<Qualification> Qualifications { get; set; }

    public virtual DbSet<Skill> Skills { get; set; }

    public virtual DbSet<SkillCat> SkillCats { get; set; }

    public virtual DbSet<SkillSubCat> SkillSubCats { get; set; }

    public virtual DbSet<Certification> Certifications { get; set; }


    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        try
        {
            var connectionString = Environment.GetEnvironmentVariable("MYSQL_CONNECTION_STRING");
            if (connectionString is null)
            {
                var server = Environment.GetEnvironmentVariable("RMALEKAR_WEBAPP_DB_SERVER");
                var db = Environment.GetEnvironmentVariable("RMALEKAR_WEBAPP_DB_NAME");
                MySqlConnectionStringBuilder builder = new MySqlConnectionStringBuilder
                {
                    Server = Environment.GetEnvironmentVariable("RMALEKAR_WEBAPP_DB_SERVER"),
                    Database = Environment.GetEnvironmentVariable("RMALEKAR_WEBAPP_DB_NAME"),
                    UserID = Environment.GetEnvironmentVariable("MYSQL_USER"),
                    Password = Environment.GetEnvironmentVariable("MYSQL_PASSWORD"),
                    SslMode = MySqlSslMode.None,
                    AllowUserVariables = true,
                    ConnectionIdleTimeout = 3
                };
                connectionString = builder.ConnectionString;
            }

            optionsBuilder.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString), options=> options.EnableRetryOnFailure());
        }
        catch(Exception ex)
        {
            // Handle exception
        }
        
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8mb4_general_ci")
            .HasCharSet("utf8mb4");

        modelBuilder.Entity<AcademicProject>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("academic projects");

            entity.Property(e => e.Duties)
                .HasColumnType("mediumtext")
                .HasColumnName("duties")
                .UseCollation("utf8_general_ci")
                .HasCharSet("utf8");
            entity.Property(e => e.ProjectId)
                .HasMaxLength(10)
                .HasColumnName("projectId");
            entity.Property(e => e.ProjectSummary)
                .HasMaxLength(500)
                .HasColumnName("project_summary")
                .UseCollation("utf8_general_ci")
                .HasCharSet("utf8");
            entity.Property(e => e.ProjectTitle)
                .HasMaxLength(45)
                .HasColumnName("project_title");
            entity.Property(e => e.Skills)
                .HasColumnType("mediumtext")
                .HasColumnName("skills")
                .UseCollation("utf8_general_ci")
                .HasCharSet("utf8");
        });

        modelBuilder.Entity<AcademicQualification>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("academic qualifications");

            entity.Property(e => e.Addr)
                .HasMaxLength(20)
                .HasColumnName("addr");
            entity.Property(e => e.Degree)
                .HasMaxLength(68)
                .HasColumnName("degree");
            entity.Property(e => e.EndDate).HasColumnName("end_date");
            entity.Property(e => e.Institute)
                .HasMaxLength(50)
                .HasColumnName("institute");
            entity.Property(e => e.KeySubjects)
                .HasColumnType("mediumtext")
                .HasColumnName("key_subjects");
            entity.Property(e => e.Qid)
                .HasMaxLength(5)
                .HasColumnName("qid");
            entity.Property(e => e.StartDate).HasColumnName("start_date");
        });

        modelBuilder.Entity<Allskill>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("allskills");

            entity.Property(e => e.CatName)
                .HasMaxLength(30)
                .HasColumnName("cat_name")
                .UseCollation("utf8_general_ci")
                .HasCharSet("utf8");
            entity.Property(e => e.Skill)
                .HasMaxLength(45)
                .HasColumnName("skill")
                .UseCollation("utf8_general_ci")
                .HasCharSet("utf8");
            entity.Property(e => e.SkillLogo)
                .HasMaxLength(250)
                .HasColumnName("skill_logo")
                .UseCollation("utf8_general_ci")
                .HasCharSet("utf8");
            entity.Property(e => e.SkillType)
                .HasColumnType("enum('Hard','Soft')")
                .HasColumnName("skill_type");
            entity.Property(e => e.SubCatName)
                .HasMaxLength(20)
                .HasColumnName("sub_cat_name")
                .UseCollation("utf8_general_ci")
                .HasCharSet("utf8");
        });

        modelBuilder.Entity<EmpDuty>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("emp duties");

            entity.HasIndex(e => e.EmpHistoryCode, "FK_EmpDuties_EmpHistory_EmpHistoryCode");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnType("tinyint(4)")
                .HasColumnName("id");
            entity.Property(e => e.ChildDutyOf)
                .HasColumnType("tinyint(4)")
                .HasColumnName("child_duty_of");
            entity.Property(e => e.Duties)
                .HasMaxLength(300)
                .HasColumnName("duties")
                .UseCollation("utf8_general_ci")
                .HasCharSet("utf8");
            entity.Property(e => e.EmpHistoryCode)
                .HasMaxLength(10)
                .HasColumnName("emp_history_code")
                .UseCollation("utf8mb4_unicode_ci");
        });

        modelBuilder.Entity<EmpHistory>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("emp history");

            entity.HasIndex(e => e.EmpHistoryCode, "emp_history_code_UNIQUE").IsUnique();

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnType("tinyint(4)")
                .HasColumnName("id");
            entity.Property(e => e.CompanyAddr)
                .HasMaxLength(50)
                .HasColumnName("company_addr")
                .UseCollation("utf8_general_ci")
                .HasCharSet("utf8");
            entity.Property(e => e.CompanyName)
                .HasMaxLength(50)
                .HasColumnName("company_name")
                .UseCollation("utf8_general_ci")
                .HasCharSet("utf8");
            entity.Property(e => e.EmpHistoryCode)
                .HasMaxLength(10)
                .HasColumnName("emp_history_code")
                .UseCollation("utf8mb4_unicode_ci");
            entity.Property(e => e.EndDate).HasColumnName("end_date");
            entity.Property(e => e.Position)
                .HasMaxLength(20)
                .HasColumnName("position")
                .UseCollation("utf8_general_ci")
                .HasCharSet("utf8");
            entity.Property(e => e.StartDate).HasColumnName("start_date");
            entity.Property(e => e.Type)
                .HasColumnType("enum('Related','Additional')")
                .HasColumnName("type");
        });

        modelBuilder.Entity<EmpKeySkill>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("emp key skill");

            entity.HasIndex(e => e.EmpHistoryCode, "FK_EmploymentKeySkill_EmpHistory_EmpCode");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnType("tinyint(4)")
                .HasColumnName("id");
            entity.Property(e => e.EmpHistoryCode)
                .HasMaxLength(10)
                .HasColumnName("emp_history_code")
                .UseCollation("utf8mb4_unicode_ci");
            entity.Property(e => e.SkillId)
                .HasColumnType("tinyint(4)")
                .HasColumnName("skill_id");
        });

        modelBuilder.Entity<KeySubject>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("key subjects");

            entity.HasIndex(e => e.Qid, "FK_KeySubjects_Qualification_Qid_idx");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnType("tinyint(4)")
                .HasColumnName("id");
            entity.Property(e => e.Ksid)
                .HasMaxLength(10)
                .HasColumnName("ksid")
                .UseCollation("utf8_general_ci")
                .HasCharSet("utf8");
            entity.Property(e => e.Qid)
                .HasMaxLength(5)
                .HasColumnName("qid");
            entity.Property(e => e.SubjectTitle)
                .HasMaxLength(45)
                .HasColumnName("subject_title");
        });

        modelBuilder.Entity<PersonalDetail>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("personal details");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnType("tinyint(4)")
                .HasColumnName("id");
            entity.Property(e => e.Address)
                .HasMaxLength(100)
                .HasColumnName("address");
            entity.Property(e => e.Email)
                .HasMaxLength(20)
                .HasColumnName("email")
                .UseCollation("utf8_general_ci")
                .HasCharSet("utf8");
            entity.Property(e => e.FirstName)
                .HasMaxLength(20)
                .HasColumnName("first_name")
                .UseCollation("utf8_general_ci")
                .HasCharSet("utf8");
            entity.Property(e => e.Github)
                .HasMaxLength(50)
                .HasColumnName("github");
            entity.Property(e => e.LastName)
                .HasMaxLength(20)
                .HasColumnName("last_name")
                .UseCollation("utf8_general_ci")
                .HasCharSet("utf8");
            entity.Property(e => e.Linkedin)
                .HasMaxLength(200)
                .HasColumnName("linkedin")
                .UseCollation("utf8_general_ci")
                .HasCharSet("utf8");
            entity.Property(e => e.Mobile)
                .HasMaxLength(20)
                .HasColumnName("mobile")
                .UseCollation("utf8_general_ci")
                .HasCharSet("utf8");
            entity.Property(e => e.Summary)
                .HasMaxLength(250)
                .HasColumnName("summary")
                .UseCollation("utf8_general_ci")
                .HasCharSet("utf8");
            entity.Property(e => e.Title)
                .HasMaxLength(30)
                .HasColumnName("title");
            entity.Property(e => e.ExperienceYears)
                .HasColumnName("experienceYears");
            entity.Property(e => e.Hackerrank)
                .HasMaxLength(50)
                .HasColumnName("hackerrank");
        });

        modelBuilder.Entity<Project>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("projects");

            entity.HasIndex(e => e.ProjectId, "projectId_UNIQUE").IsUnique();

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnType("tinyint(4)")
                .HasColumnName("id");
            entity.Property(e => e.ProjectId)
                .HasMaxLength(10)
                .HasColumnName("projectId");
            entity.Property(e => e.ProjectSummary)
                .HasMaxLength(500)
                .HasColumnName("project_summary")
                .UseCollation("utf8_general_ci")
                .HasCharSet("utf8");
            entity.Property(e => e.ProjectTitle)
                .HasMaxLength(45)
                .HasColumnName("project_title");
        });

        modelBuilder.Entity<ProjectDuty>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("project duties");

            entity.HasIndex(e => e.ProjectId, "fk_academic_project_idx");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnType("tinyint(4)")
                .HasColumnName("id");
            entity.Property(e => e.DutiesDesc)
                .HasMaxLength(250)
                .HasColumnName("duties_desc")
                .UseCollation("utf8_general_ci")
                .HasCharSet("utf8");
            entity.Property(e => e.ProjectId)
                .HasMaxLength(10)
                .HasColumnName("project_id");
        });

        modelBuilder.Entity<ProjectKeySkill>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("project key skills");

            entity.HasIndex(e => e.ProjectId, "fk_project_id_idx");

            entity.HasIndex(e => e.SkillId, "fk_skill_id_idx");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnType("tinyint(4)")
                .HasColumnName("id");
            entity.Property(e => e.ProjectId)
                .HasMaxLength(10)
                .HasColumnName("project_id");
            entity.Property(e => e.SkillId)
                .HasColumnType("tinyint(4)")
                .HasColumnName("skill_id");
        });

        modelBuilder.Entity<Qualification>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("qualification");

            entity.HasIndex(e => e.Qid, "qid_UNIQUE").IsUnique();

            entity.HasIndex(e => e.Title, "title_UNIQUE").IsUnique();

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnType("tinyint(4)")
                .HasColumnName("id");
            entity.Property(e => e.Addr)
                .HasMaxLength(20)
                .HasColumnName("addr");
            entity.Property(e => e.EndDate).HasColumnName("end_date");
            entity.Property(e => e.Institute)
                .HasMaxLength(50)
                .HasColumnName("institute");
            entity.Property(e => e.Qid)
                .HasMaxLength(5)
                .HasColumnName("qid");
            entity.Property(e => e.StartDate).HasColumnName("start_date");
            entity.Property(e => e.Title)
                .HasMaxLength(68)
                .HasColumnName("title");
        });

        modelBuilder.Entity<Skill>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("skills");

            entity.HasIndex(e => e.SkillSubcat, "FK_Skills_SkillSubCat_SkillSubCat");

            entity.HasIndex(e => e.Skill1, "technology_UNIQUE").IsUnique();

            entity.Property(e => e.Id)
                .HasColumnType("tinyint(4)")
                .HasColumnName("id");
            entity.Property(e => e.Skill1)
                .HasMaxLength(45)
                .HasColumnName("skill")
                .UseCollation("utf8_general_ci")
                .HasCharSet("utf8");
            entity.Property(e => e.SkillLogo)
                .HasMaxLength(250)
                .HasColumnName("skill_logo")
                .UseCollation("utf8_general_ci")
                .HasCharSet("utf8");
            entity.Property(e => e.SkillSubcat)
                .HasMaxLength(5)
                .HasColumnName("skill_subcat")
                .UseCollation("utf8_general_ci")
                .HasCharSet("utf8");
            entity.Property(e => e.SkillType)
                .HasColumnType("enum('Hard','Soft')")
                .HasColumnName("skill_type");

            entity.HasOne(d => d.SkillSubcatNavigation).WithMany(p => p.Skills)
                .HasPrincipalKey(p => p.SkillSubcat1)
                .HasForeignKey(d => d.SkillSubcat)
                .HasConstraintName("FK_Skills_SkillSubCat_SkillSubCat");
        });

        modelBuilder.Entity<SkillCat>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("skill cat");

            entity.HasIndex(e => e.SkillCat1, "skill_cat").IsUnique();

            entity.Property(e => e.Id)
                .HasColumnType("tinyint(4)")
                .HasColumnName("id");
            entity.Property(e => e.CatName)
                .HasMaxLength(30)
                .HasColumnName("cat_name")
                .UseCollation("utf8_general_ci")
                .HasCharSet("utf8");
            entity.Property(e => e.SkillCat1)
                .HasMaxLength(5)
                .HasColumnName("skill_cat");
        });

        modelBuilder.Entity<SkillSubCat>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("skill sub cat");

            entity.HasIndex(e => e.SkillCat, "FK_SkillSubCat_SkillCat_SkillCatId_idx");

            entity.HasIndex(e => e.SkillSubcat1, "skill_subcat").IsUnique();

            entity.Property(e => e.Id)
                .HasColumnType("tinyint(4)")
                .HasColumnName("id");
            entity.Property(e => e.SkillCat)
                .HasMaxLength(5)
                .HasColumnName("skill_cat");
            entity.Property(e => e.SkillSubcat1)
                .HasMaxLength(5)
                .HasColumnName("skill_subcat")
                .UseCollation("utf8_general_ci")
                .HasCharSet("utf8");
            entity.Property(e => e.SubCatName)
                .HasMaxLength(20)
                .HasColumnName("sub_cat_name")
                .UseCollation("utf8_general_ci")
                .HasCharSet("utf8");

            entity.HasOne(d => d.SkillCatNavigation).WithMany(p => p.SkillSubCats)
                .HasPrincipalKey(p => p.SkillCat1)
                .HasForeignKey(d => d.SkillCat)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_SkillSubCat_SkillCat_SkillCatId");
        });

        modelBuilder.Entity<Certification>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");
            entity.ToTable("certifications");
            entity.Property(e => e.Id)
                .HasColumnType("int(11)")
                .HasColumnName("id");
            entity.Property(e => e.Date).HasColumnName("date");
            entity.Property(e => e.Img)
                .HasMaxLength(250)
                .HasColumnName("img");
            entity.Property(e => e.Issuer)
                .HasMaxLength(100)
                .HasColumnName("issuer");
            entity.Property(e => e.Title)
                .HasMaxLength(250)
                .HasColumnName("title");
            entity.Property(e => e.Url)
                .HasMaxLength(500)
                .HasColumnName("url");
        });

        modelBuilder.Entity<Allskill>().HasNoKey();
        modelBuilder.Entity<AcademicQualification>().HasNoKey();
        modelBuilder.Entity<AcademicProject>().HasNoKey();
        modelBuilder.Entity<EmploymentHistory>(e =>
        {
            e.HasNoKey()
                .ToView("employment history");
        });
        modelBuilder.Entity<PortfolioItem>().HasNoKey();
        modelBuilder.Entity<Personalinfo>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("personalinfo");

            entity.Property(e => e.ContactRawJson)
                .HasMaxLength(184)
                .HasDefaultValueSql("''")
                .HasColumnName("contact");
            entity.Property(e => e.FirstName)
                .HasMaxLength(20)
                .HasColumnName("first_name")
                .UseCollation("utf8_general_ci")
                .HasCharSet("utf8");
            entity.Property(e => e.LastName)
                .HasMaxLength(20)
                .HasColumnName("last_name")
                .UseCollation("utf8_general_ci")
                .HasCharSet("utf8");
            entity.Property(e => e.SocialLinksRawJson)
                .HasMaxLength(347)
                .HasColumnName("socialLinks");
            entity.Property(e => e.Summary)
                .HasMaxLength(250)
                .HasColumnName("summary")
                .UseCollation("utf8_general_ci")
                .HasCharSet("utf8");
            entity.Property(e => e.ExperienceYears)
                .HasColumnName("experienceYears");
        });

        modelBuilder.Entity<PortfolioItem>(entity =>
        {
            entity.HasNoKey()
                .ToView("portfolio items");
             
        });
        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
