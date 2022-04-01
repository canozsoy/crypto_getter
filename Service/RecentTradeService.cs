using Microsoft.VisualBasic.CompilerServices;
using Catalog.Entities;
using Catalog.UtilFunctions;
using Newtonsoft.Json;

namespace Catalog.Services

{
    public class RecentTradeService {
        public async Task<List<RecentTradeBinance>> fetchBinanceTrades(string binanceString) {
            try {
                string response = await UtilClass.makeNetworkCall($"https://api.binance.com/api/v3/trades?symbol={binanceString}&limit=10");
                var result = JsonConvert.DeserializeObject<List<RecentTradeBinance>>(response);
                return result;
            } catch {
                throw new ArgumentException("NOT_FOUND");
            }
        }
        public async Task<RecentTradeHuobi> fetchHuobiTrades(string huobiString) {
            string response = await UtilClass.makeNetworkCall($"https://api.huobi.pro/market/history/trade?symbol={huobiString}&size=10");
            var result = JsonConvert.DeserializeObject<RecentTradeHuobi>(response);
            if (result.status == "error") {
                throw new ArgumentException("NOT_FOUND");
            }
            return result;
        }
        public List<RecentTrade> formatBinanceTrade(List<RecentTradeBinance> unformatted) {
            return unformatted.Select(item => new RecentTrade{
                id = item.id,
                price = item.price,
                qty = item.qty,
                time = item.time,
                source = "Binance"
            }).ToList();
        }
        public List<RecentTrade> formatHuobiTrade(RecentTradeHuobi unformatted) {
            return unformatted.data.Select(item => new RecentTrade{
                id = item.data[0].id,
                price = item.data[0].price,
                qty = item.data[0].amount,
                time = item.data[0].ts,
                source = "Huobi"
            }).ToList();
        }
        public async Task<IEnumerable<RecentTrade>> getRecentTrade(string binanceString, string huobiString) {
            var binanceResult = this.fetchBinanceTrades(binanceString);
            var huobiResult = this.fetchHuobiTrades(huobiString);
            await Task.WhenAll(binanceResult, huobiResult);
            var binance = this.formatBinanceTrade(await binanceResult);
            var huobi = this.formatHuobiTrade(await huobiResult);
            var result = binance.Concat(huobi);
            return result;
        }
    }
}