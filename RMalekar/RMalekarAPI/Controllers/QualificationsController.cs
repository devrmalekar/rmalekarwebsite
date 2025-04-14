using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MySql.Data.MySqlClient;
using RMalekarEntityModels;

namespace RMalekarAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class QualificationsController : ControllerBase
    {
        private readonly ILogger<QualificationsController> _logger;
        private readonly RmalekarDataContext _rmdb;
        public QualificationsController(ILogger<QualificationsController> logger, RmalekarDataContext rm) 
        {
            _logger = logger;
            _rmdb = rm;
        }
       
        [HttpGet]
        public async Task<ActionResult<IDictionary<string, IDictionary<string, List<AcademicQualification>>>>> Index()
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

                return Ok(academicQualifications);
            }
            catch (MySqlException ex)
            {
                _logger.LogError(ex, "MySQL error occurred while fetching qualifications.");
                return StatusCode(500, "Internal server error");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while fetching qualifications.");
                return StatusCode(500, "Internal server error");
            }   
        }
    }
}
