using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MySql.Data.MySqlClient;
using RMalekarEntityModels;

namespace RMalekarAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SkillsController : ControllerBase
    {
        private readonly ILogger<SkillsController> _logger;
        private readonly RmalekarDataContext _rmdb;
        public SkillsController(ILogger<SkillsController> logger, RmalekarDataContext rm) 
        {
            _logger = logger;
            _rmdb = rm;
        }
        
        [HttpGet]
        public async Task<ActionResult<IDictionary<string, IDictionary<string, List<Allskill>>>>> Index()
        {
            try
            {
                var allSkills = await _rmdb.Allskills.ToArrayAsync();
                var groupedAllSkills = allSkills.GroupBy(s => s.CatName)
                               .ToDictionary(g => g.Key, g => g.GroupBy(s => s.SubCatName)
                               .ToDictionary(sg => sg.Key, sg => sg.Select(s => s).ToList()
                               ));
                return Ok(groupedAllSkills);
            }
            catch(MySqlException ex)
            {
                _logger.LogError(ex, "MySQL error occurred while fetching skills.");
                return StatusCode(500, "Internal server error");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while fetching skills.");
                return StatusCode(500, "Internal server error");
            }
        }

      
    }
}
