using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using Portfolio.Api.Context;
using Portfolio.Api.Models;
using Portfolio.Shared.Dtos;

namespace Portfolio.Api.Helpers;

public static class DatabaseInitializer
{
    public static async Task Initialize(IApplicationBuilder app)
    {
        using var scope = app.ApplicationServices.CreateScope();

        var services = scope.ServiceProvider;

        try
        {
            var context = services.GetRequiredService<PortfolioDbContext>();
            Console.WriteLine("Checking for pending EF migrations....");
            context.Database.Migrate();
            Console.WriteLine("EF migration checked and applied successfully.");

            SeedPersonalDetail(context);
            SeedCertification(context);
            SeedExperience(context);
            SeedSkill(context);
            SeedProject(context);
            SeedQualification(context);
        }
        catch (System.Exception)
        {
            Console.WriteLine("Critical Error");
        }
    }

    private static void SeedPersonalDetail(PortfolioDbContext context)
    {
        if (!context.PersonalDetails.Any())
        {
            Console.WriteLine("No existing personal detail found. Starting data seeding...");

            var personalDetail = new PersonalDetail
            {
                Id = 1,
                FirstName = "Avery",
                LastName = "Stone",
                Headline = "Software Engineer",
                Summary =
                    "A creative software engineer focused on delivering polished digital products, dependable APIs, and thoughtful user experiences through collaborative problem solving.",
                Introduction = @"Story about yourself lorem porem sorem",
                Email = "avery.stone@example.dev",
                Mobile = "+1 555 010 2048",
                Address = "Fictional City, WA",
                Github = "https://github.com/avery-stone",
                Hackerrank = "https://www.hackerrank.com/avery_stone",
                Stackoverflow = "https://stackoverflow.com/users/000000/avery-stone",
                Linkedin = "https://www.linkedin.com/in/avery-stone",
                ExperienceYears = 6,
            };

            context.PersonalDetails.AddAsync(personalDetail);
            context.SaveChangesAsync();
            Console.WriteLine(
                "🎉 Database successfully seeded with fictional personal detail values."
            );
        }
    }

    private static void SeedCertification(PortfolioDbContext context)
    {
        if (!context.Certifications.Any())
        {
            Console.WriteLine("No existing certification found. Starting data seeding...");
            var certifications = new List<Certification>()
            {
                new Certification()
                {
                    Id = 1,
                    Title = "Certified Cloud Builder",
                    Issuer = "Fictional Academy",
                    Url = "https://example.dev/cert/cloud-builder",
                    ImageUrl = "/public/images/cert/fake-cert-1.png",
                    Date = new DateOnly(2024, 06, 12),
                },
                new Certification()
                {
                    Id = 2,
                    Title = "Product Design Essentials",
                    Issuer = "Northstar Learning",
                    Url = "https://example.dev/cert/product-design",
                    ImageUrl = "/public/images/cert/fake-cert-2.png",
                    Date = new DateOnly(2024, 09, 24),
                },
                new Certification()
                {
                    Id = 3,
                    Title = "Modern API Engineering",
                    Issuer = "Blue Harbor Institute",
                    Url = "https://example.dev/cert/api-engineering",
                    ImageUrl = "/public/images/cert/fake-cert-3.png",
                    Date = new DateOnly(2025, 01, 16),
                },
                new Certification()
                {
                    Id = 4,
                    Title = "Secure Frontend Delivery",
                    Issuer = "Pixel Labs",
                    Url = "https://example.dev/cert/frontend-security",
                    ImageUrl = "/public/images/cert/fake-cert-4.png",
                    Date = new DateOnly(2025, 04, 08),
                },
                new Certification()
                {
                    Id = 5,
                    Title = "Agile Team Leadership",
                    Issuer = "Echo Training",
                    Url = "https://example.dev/cert/agile-leadership",
                    ImageUrl = "/public/images/cert/fake-cert-5.png",
                    Date = new DateOnly(2025, 07, 21),
                },
            };
            context.AddRangeAsync(certifications);
            context.SaveChangesAsync();
            Console.WriteLine(
                "🎉 Database successfully seeded with fictional certification values."
            );
        }
    }

    private static void SeedExperience(PortfolioDbContext context)
    {
        if (!context.Experiences.Any())
        {
            Console.WriteLine("No existing experience found. Starting data seeding...");
            var experiences = new List<Experience>()
            {
                new Experience()
                {
                    CompanyAddress = "Fictional City, WA",
                    CompanyName = "Northstar Labs",
                    EndDate = new DateOnly(2025, 04, 01),
                    Id = 1,
                    Position = "Senior Software Engineer",
                    StartDate = new DateOnly(2023, 02, 01),
                    Type = "Related",
                },
                new Experience()
                {
                    CompanyAddress = "Lakeview, CA",
                    CompanyName = "Blue Harbor Systems",
                    EndDate = new DateOnly(2023, 01, 01),
                    Id = 2,
                    Position = "Frontend Developer",
                    StartDate = new DateOnly(2021, 06, 01),
                    Type = "Related",
                },
                new Experience()
                {
                    CompanyAddress = "Maple Grove, TX",
                    CompanyName = "Echo Studio",
                    EndDate = new DateOnly(2021, 03, 01),
                    Id = 3,
                    Position = "Product Engineer",
                    StartDate = new DateOnly(2019, 08, 01),
                    Type = "Related",
                },
                new Experience()
                {
                    CompanyAddress = "Brighton, OR",
                    CompanyName = "Pixel Forge",
                    EndDate = null,
                    Id = 4,
                    Position = "Consulting Developer",
                    StartDate = new DateOnly(2025, 05, 01),
                    Type = "Additional",
                },
            };
            context.AddRangeAsync(experiences);
            context.SaveChangesAsync();
            Console.WriteLine("🎉 Database successfully seeded with fictional experience values.");
        }
    }

    private static void SeedSkill(PortfolioDbContext context)
    {
        if (context.Skills.Any())
        {
            return;
        }
        try
        {
            Console.WriteLine("No existing skills found. Starting data seeding...");

            var json = File.ReadAllText("Data/skills.json");
            var skillAccordion = JsonSerializer.Deserialize<List<SkillAccordionDto>>(
                json,
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true }
            );

            int skillIndex = 1;
            for (int accordIndex = 0; accordIndex < skillAccordion.Count; accordIndex++)
            {
                var skillAccord = new SkillAccordion()
                {
                    Id = accordIndex + 1,
                    Name = skillAccordion[accordIndex].Category,
                    DisplayOrder = skillAccordion[accordIndex].DisplayOrder,
                };

                foreach (SkillDto skill in skillAccordion[accordIndex].Skills)
                {
                    var newSkill = new Skill
                    {
                        Id = skillIndex++,
                        Name = skill.Name,
                        SkillLogo = skill.SkillLogo,
                        SkillAccordionId = skillAccord.Id,
                    };
                    skillAccord.Skills.Add(newSkill);
                }
                context.AddAsync(skillAccord);
            }
            context.SaveChanges();
            Console.WriteLine("🎉 Database successfully seeded with default skills  values.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"{ex.Message}");
        }
    }

    private static void SeedProject(PortfolioDbContext context)
    {
        if (!context.Projects.Any())
        {
            Console.WriteLine("No existing projects found. Starting data seeding...");
            int pksId = 1;
            var projects = new List<Project>()
            {
                new Project()
                {
                    Id = 1,
                    ProjectCode = "PRJ001",
                    ProjectSummary =
                        "A fictional dashboard experience for tracking product delivery, team health, and release milestones in a fast-moving startup environment.",
                    Title = "Nova Dashboard",
                    Url = "https://example.dev/projects/nova-dashboard",
                    KeySkills = new()
                    {
                        new ProjectKeySkill()
                        {
                            Id = pksId++,
                            SkillId = 1,
                            ProjectId = 2,
                        },
                        new ProjectKeySkill()
                        {
                            Id = pksId++,
                            SkillId = 6,
                            ProjectId = 2,
                        },
                        new ProjectKeySkill()
                        {
                            Id = pksId++,
                            SkillId = 4,
                            ProjectId = 2,
                        },
                    },
                },
                new Project()
                {
                    Id = 2,
                    ProjectCode = "PRJ002",
                    ProjectSummary =
                        "A sample service layer for managing users, permissions, and audit events with a clean API and mock data workflows.",
                    Title = "Atlas Access",
                    Url = "https://example.dev/projects/atlas-access",
                    KeySkills = new()
                    {
                        new ProjectKeySkill()
                        {
                            Id = pksId++,
                            SkillId = 1,
                            ProjectId = 3,
                        },
                        new ProjectKeySkill()
                        {
                            Id = pksId++,
                            SkillId = 6,
                            ProjectId = 3,
                        },
                        new ProjectKeySkill()
                        {
                            Id = pksId++,
                            SkillId = 4,
                            ProjectId = 3,
                        },
                    },
                },
                new Project()
                {
                    Id = 3,
                    ProjectCode = "PRJ003",
                    ProjectSummary =
                        "A staged product catalog experience combining a storefront, admin tools, and a lightweight order workflow for a fictional retail brand.",
                    Title = "Harbor Commerce",
                    Url = "https://example.dev/projects/harbor-commerce",
                    KeySkills = new()
                    {
                        new ProjectKeySkill()
                        {
                            Id = pksId++,
                            SkillId = 1,
                            ProjectId = 2,
                        },
                        new ProjectKeySkill()
                        {
                            Id = pksId++,
                            SkillId = 6,
                            ProjectId = 2,
                        },
                        new ProjectKeySkill()
                        {
                            Id = pksId++,
                            SkillId = 4,
                            ProjectId = 2,
                        },
                    },
                },
                new Project()
                {
                    Id = 4,
                    ProjectCode = "PRJ004",
                    ProjectSummary =
                        "A playful task manager built to demonstrate realtime board updates, notifications, and simple collaboration patterns.",
                    Title = "Pulse Board",
                    Url = "https://example.dev/projects/pulse-board",
                    KeySkills = new()
                    {
                        new ProjectKeySkill()
                        {
                            Id = pksId++,
                            SkillId = 1,
                            ProjectId = 2,
                        },
                        new ProjectKeySkill()
                        {
                            Id = pksId++,
                            SkillId = 6,
                            ProjectId = 2,
                        },
                        new ProjectKeySkill()
                        {
                            Id = pksId++,
                            SkillId = 4,
                            ProjectId = 2,
                        },
                    },
                },
                new Project()
                {
                    Id = 5,
                    ProjectCode = "PRJ005",
                    ProjectSummary =
                        "A multi-view portfolio concept that brings together a landing page, case studies, and a simple content management flow.",
                    Title = "Silver Portfolio",
                    Url = "https://example.dev/projects/silver-portfolio",
                    KeySkills = new()
                    {
                        new ProjectKeySkill()
                        {
                            Id = pksId++,
                            SkillId = 1,
                            ProjectId = 3,
                        },
                        new ProjectKeySkill()
                        {
                            Id = pksId++,
                            SkillId = 6,
                            ProjectId = 3,
                        },
                        new ProjectKeySkill()
                        {
                            Id = pksId++,
                            SkillId = 4,
                            ProjectId = 3,
                        },
                    },
                },
            };
            context.AddRangeAsync(projects);
            context.SaveChangesAsync();
            Console.WriteLine("🎉 Database successfully seeded with fictional project values.");
        }
    }

    private static void SeedQualification(PortfolioDbContext context)
    {
        if (!context.Qualifications.Any())
        {
            Console.WriteLine("No existing qualification detail found. Starting data seeding...");
            var qualifications = new List<Qualification>()
            {
                new Qualification()
                {
                    Address = "Fictional State",
                    EndDate = new DateOnly(2022, 10, 30),
                    GPA = "6.1/7.0",
                    Id = 1,
                    Institute = "Northbridge University",
                    Qid = "MCS",
                    StartDate = new DateOnly(2020, 03, 01),
                    Title = "Master of Computer Science",
                },
                new Qualification()
                {
                    Address = "Example County",
                    EndDate = new DateOnly(2019, 02, 01),
                    GPA = "78.4%",
                    Id = 2,
                    Institute = "Riverstone College",
                    Qid = "BScIT",
                    StartDate = new DateOnly(2016, 02, 01),
                    Title = "Bachelor of Science in Information Technology",
                },
            };
            context.AddRangeAsync(qualifications);
            context.SaveChangesAsync();
            Console.WriteLine(
                "🎉 Database successfully seeded with fictional qualification values."
            );
        }
    }
}
