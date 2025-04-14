using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MySql.Data.MySqlClient;
using RMalekarEntityModels;

namespace RMalekarAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Route("")]

    public class PersonalInfoController : ControllerBase
    {
        private readonly ILogger<PersonalInfoController> _logger;
        private readonly RmalekarDataContext _rmdb;

        public PersonalInfoController(ILogger<PersonalInfoController> logger, RmalekarDataContext rmdb)
        {
            _logger = logger;
            _rmdb = rmdb;
        }
    
        [HttpGet]
        public async Task<ActionResult<IDictionary<string, IDictionary<string, List<Personalinfo>>>>> Index()
        {
            try
            {
                var personalDetail = await _rmdb.PersonalInfo.FirstOrDefaultAsync();
                return Ok(personalDetail);
            }
            catch (MySqlException ex)
            {
                _logger.LogError(ex, "MySQL error occurred while fetching personal info.");
                return StatusCode(500, "Internal Server Error");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occured while fetching personal info.");
                return StatusCode(500, "Internal Server Error");
            }
          
        }
    }
}
