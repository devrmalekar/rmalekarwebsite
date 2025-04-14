using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MySql.Data.MySqlClient;
using RMalekarEntityModels;

namespace RMalekarAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CertificationsController : ControllerBase
    {
        private readonly ILogger<CertificationsController> _logger;
        private readonly RmalekarDataContext _rmdb;
        public CertificationsController(ILogger<CertificationsController> logger, RmalekarDataContext rm)
        {
            _logger = logger;
            _rmdb = rm;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Certification>>> Index()
        {
            try
            {
                var certifications = await _rmdb.Certifications.ToListAsync();
                return Ok(certifications);
            }
            catch (MySqlException ex)
            {
                _logger.LogError(ex, "MySQL error occurred while fetching certifications.");
                return StatusCode(500, "Internal server error");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while fetching certifications.");
                return StatusCode(500, "Internal server error");
            }
        }
        // Other methods can be added here as needed
    
    }
}
