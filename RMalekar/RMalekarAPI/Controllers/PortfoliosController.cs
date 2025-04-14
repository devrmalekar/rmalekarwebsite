using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using RMalekarEntityModels;
using MySql.Data.MySqlClient;

namespace RMalekarAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PortfoliosController : ControllerBase
    {
        private readonly ILogger<PortfoliosController> _logger;
        private readonly RmalekarDataContext _rmdb;

        public PortfoliosController(ILogger<PortfoliosController> logger, RmalekarDataContext rmdb)
        {
            _logger = logger;
            _rmdb = rmdb;
        }

        [HttpGet]
        public async Task<ActionResult<IDictionary<string, IDictionary<string, List<PortfolioItem>>>>> Index()
        {
            try
            {
                var portfolios = await _rmdb.PortfolioItems.ToListAsync();

                return Ok(portfolios);
            }
            catch(MySqlException ex)
            {
                _logger.LogError(ex, "MySQL error occurred while fetching portfolios.");
                return StatusCode(500, "Internal server error");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while fetching portfolios.");
                return StatusCode(500, "Internal server error");
            }
        }
    }
}
