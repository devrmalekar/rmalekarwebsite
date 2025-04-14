using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MySql.Data.MySqlClient;
using RMalekarEntityModels;

namespace RMalekarAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ExperiencesController : ControllerBase
    {
        private readonly ILogger<ExperiencesController> _logger;
        private readonly RmalekarDataContext _rmdb;

        public ExperiencesController(ILogger<ExperiencesController> logger, RmalekarDataContext rmdb)
        {
            _logger = logger;
            _rmdb = rmdb;
        }

        [HttpGet]
        public async Task<ActionResult<IDictionary<string, IDictionary<string, List<EmploymentHistory>>>>> Index()
        {
            try
            {
                var empHistories = await _rmdb.EmploymentHistories.ToArrayAsync();
                return Ok(empHistories);
            }
            catch (MySqlException ex)
            {
                _logger.LogError(ex, "MySQL error occurred while fetching experiences.");
                return StatusCode(500, "Internal server error");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while fetching experiences.");
                return StatusCode(500, "Internal server error");
            }
                
        }
    }
}