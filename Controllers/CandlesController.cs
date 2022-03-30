using Microsoft.AspNetCore.Mvc;
using Catalog.Services;
using Catalog.Entities;
using Catalog.UtilFunctions;

namespace Catalog.Controllers {
    [ApiController]
    [Route("api/v1/candles")]
    public class CandlesController : ControllerBase {
        private readonly CandleServices candleServices;

        public CandlesController () {
            this.candleServices = new CandleServices();
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Candles>>> getRecentTrades(string currency) {
            (string binanceCurrencyString, string huobiCurrencyString) = UtilClass.formatStrings(currency);
            
            try {
                return Ok(await this.candleServices.getCandles(binanceCurrencyString, huobiCurrencyString));    
            } catch (Exception err) {
                if (err.Message == "NOT_FOUND") {
                    return NotFound(new List<RecentTrade>());
                }
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }

}