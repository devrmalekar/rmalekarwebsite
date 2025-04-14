using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MySql.Data.MySqlClient;
using RMalekarEntityModels;
using Serilog.Core;
using System.IO;

namespace RMalekarAPI.Services
{
    public class UpdateDataScheduler : BackgroundService
    {
        private readonly ILogger<UpdateDataScheduler> _logger;
        //private readonly IServiceProvider _serviceProvider;
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly TimeSpan interval = TimeSpan.FromMinutes(5);
        private readonly GitHubJsonUpdater _gitHubJsonUpdater;
        public UpdateDataScheduler(ILogger<UpdateDataScheduler> logger, IServiceProvider serviceProvider, IServiceScopeFactory scopeFactory, GitHubJsonUpdater gitHubJsonUpdater)
        {
            _logger = logger;
            _serviceProvider = serviceProvider;
            _scopeFactory = scopeFactory;
            _gitHubJsonUpdater = gitHubJsonUpdater;
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("UpdateDataScheduler is started.");
            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    await UpdateDataAsync();
                    _logger.LogInformation("Json data updated successfully at : {time}.", DateTime.Now);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "An error occurred while updating data.");
                }
                await Task.Delay(interval, stoppingToken);
            }
        }

        private async Task UpdateDataAsync()
        {
            using (var scope = _scopeFactory.CreateScope())
            {
                try
                {
                    var _rmdb = scope.ServiceProvider.GetRequiredService<RmalekarDataContext>();
                    string path = Path.Combine(AppContext.BaseDirectory, "React", "public", "data");
                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }

                    await UpdateCertificationsAsync(_rmdb, path);
                    await UpdatePersonalDataAsync(_rmdb, path);
                    await UpdatePortfolioDataAsync(_rmdb, path);
                    await UpdateWorkExperienceAsync(_rmdb, path);
                    await UpdateGroupedSkillsAsync(_rmdb, path);
                    await UpdateQualificationsAsync(_rmdb, path);
                    

                    _logger.LogInformation("JSON updated successfully with new data.");
                } catch (Exception ex)
                {
                    _logger.LogError(ex, "An error occurred while creating scope.");
                }
               
            }
           
        }

        private async Task UpdateCertificationsAsync(RmalekarDataContext _rmdb, string path)
        {
            try
            {
                var personalInfo = await _rmdb.Certifications.ToListAsync();
                string jsonPersonalInfo = JsonSerializer.Serialize(personalInfo, new JsonSerializerOptions { WriteIndented = true });
                await _gitHubJsonUpdater.UpdateJsonFileAsync(jsonPersonalInfo, "rmfrontendpro/public/data/CertificationData.json");
                // await File.WriteAllTextAsync(Path.Combine(path, "PersonalData.json"), jsonPersonalInfo);
            }
            catch (MySqlException ex)
            {
                _logger.LogError(ex, "An error occurred while updating personal data.");
            }
            catch (IOException ex)
            {
                _logger.LogError(ex, "An error occurred while writing to file.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred.");
            }
        }

        private async Task UpdatePersonalDataAsync(RmalekarDataContext _rmdb, string path)
        {
            try
            {
                var personalInfo = await _rmdb.PersonalInfo.FirstOrDefaultAsync();
                string jsonPersonalInfo = JsonSerializer.Serialize(personalInfo, new JsonSerializerOptions { WriteIndented = true });
                await _gitHubJsonUpdater.UpdateJsonFileAsync(jsonPersonalInfo, "rmfrontendpro/public/data/PersonalData.json");
                await File.WriteAllTextAsync(Path.Combine(path, "PersonalData.json"), jsonPersonalInfo);
            }
            catch (MySqlException ex)
            {
                _logger.LogError(ex, "An error occurred while updating personal data.");
            }
            catch (IOException ex)
            {
                _logger.LogError(ex, "An error occurred while writing to file.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred.");
            }
        }

           

        private async Task UpdatePortfolioDataAsync(RmalekarDataContext _rmdb, string path)
        {
            try
            {
                var portfolios = await _rmdb.PortfolioItems.ToListAsync();
                string jsonPortfolios = JsonSerializer.Serialize(portfolios, new JsonSerializerOptions { WriteIndented = true });
                await _gitHubJsonUpdater.UpdateJsonFileAsync(jsonPortfolios, "rmfrontendpro/public/data/PortfolioData.json");

                //await File.WriteAllTextAsync(Path.Combine(path, "PortfolioData.json"), jsonPortfolios);
            }
            catch (MySqlException ex)
            {
                _logger.LogError(ex, "An error occurred while updating personal data.");
            }
            catch (IOException ex)
            {
                _logger.LogError(ex, "An error occurred while writing to file.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred.");
            }
            
        }

        private async Task UpdateWorkExperienceAsync(RmalekarDataContext _rmdb, string path)
        {
            try
            {
                var workExperiences = await _rmdb.EmploymentHistories.ToListAsync();
                string jsonWorkExperiences = JsonSerializer.Serialize(workExperiences, new JsonSerializerOptions { WriteIndented = true });
                await _gitHubJsonUpdater.UpdateJsonFileAsync(jsonWorkExperiences, "rmfrontendpro/public/data/ExperienceData.json");

                //await File.WriteAllTextAsync(Path.Combine(path, "ExperienceData.json"), jsonWorkExperiences);
            }
            catch (MySqlException ex)
            {
                _logger.LogError(ex, "An error occurred while updating personal data.");
            }
            catch (IOException ex)
            {
                _logger.LogError(ex, "An error occurred while writing to file.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred.");
            }
            
        }

        private async Task UpdateGroupedSkillsAsync(RmalekarDataContext _rmdb, string path)
        {
            try
            {
                var allSkills = await _rmdb.Allskills.ToArrayAsync();
                var groupedAllSkills = allSkills.GroupBy(s => s.CatName)
                               .ToDictionary(g => g.Key, g => g.GroupBy(s => s.SubCatName)
                               .ToDictionary(sg => sg.Key, sg => sg.Select(s => s).ToList()
                               ));
                string jsonGroupedAllSkills = JsonSerializer.Serialize(groupedAllSkills, new JsonSerializerOptions { WriteIndented = true });
                await _gitHubJsonUpdater.UpdateJsonFileAsync(jsonGroupedAllSkills, "rmfrontendpro/public/data/SkillData.json");

                //await File.WriteAllTextAsync(Path.Combine(path, "SkillData.json"), jsonGroupedAllSkills);
            }
            catch (MySqlException ex)
            {
                _logger.LogError(ex, "An error occurred while updating personal data.");
            }
            catch (IOException ex)
            {
                _logger.LogError(ex, "An error occurred while writing to file.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred.");
            }
           
        }

        private async Task UpdateQualificationsAsync(RmalekarDataContext _rmdb, string path)
        {
            try
            {
                var qualifications = await _rmdb.AcademicQualifications.ToArrayAsync();
                var academicProjects = await _rmdb.AcademicProjects.ToArrayAsync();

                var groupedQualifications = qualifications.GroupJoin(
                        academicProjects,
                        q => q.Qid[0],
                        p => p.ProjectId[0],
                        (q, p) => new
                        {
                            q.Degree,
                            q.StartDate,
                            q.EndDate,
                            q.Institute,
                            q.Addr,
                            q.KeySubjects,
                            Projects = p.Select(p => new { p.ProjectTitle, p.ProjectSummary, p.Skills })
                        }).ToList();

                Dictionary<string, object> academicQualifications = new();
                groupedQualifications.ForEach(a =>
                {
                    var key = (a.Degree.Contains("Master")) ? "Master" : "Bachelor";
                    academicQualifications[key] = a;

                });
                string jsonAcademicQualificatoins = JsonSerializer.Serialize(academicQualifications, new JsonSerializerOptions { WriteIndented = true });
                await _gitHubJsonUpdater.UpdateJsonFileAsync(jsonAcademicQualificatoins, "rmfrontendpro/public/data/QualificationData.json");

                //await File.WriteAllTextAsync(Path.Combine(path, "QualificationData.json"), jsonPortfolios);
            }
            catch (MySqlException ex)
            {
                _logger.LogError(ex, "An error occurred while updating personal data.");
            }
            catch (IOException ex)
            {
                _logger.LogError(ex, "An error occurred while writing to file.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred.");
            }
            
        }

       

    } 
}
