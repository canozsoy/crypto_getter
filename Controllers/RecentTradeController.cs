using Microsoft.AspNetCore.Mvc;
using Catalog.Services;
using Catalog.Entities;
using Catalog.UtilFunctions;

namespace Catalog.Controllers {

    [ApiController]
    [Route("api/v1/recent-trades")]
    public class RecentTradeController : ControllerBase {

        private readonly RecentTradeService recentTradeService;

        public RecentTradeController () {
            this.recentTradeService = new RecentTradeService();
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RecentTrade>>> getRecentTrades(string currency) {
            (string binanceString, string huobiString) = UtilClass.formatStrings(currency);
            try {
                return Ok(await this.recentTradeService.getRecentTrade(binanceString, huobiString));
            } catch (Exception err) {
                if (err.Message == "NOT_FOUND") {
                    return NotFound(new List<RecentTrade>());
                }
                return StatusCode(StatusCodes.Status500InternalServerError);
            }   
        }
    }
}