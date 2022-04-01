using Catalog.Entities;
using Catalog.UtilFunctions;
using Newtonsoft.Json;

namespace Catalog.Services
{
    public class CandleServices {
        public async Task<CandlesHuobi> fetchHuobiCandles(string huobiString) {
            string response = await UtilClass.makeNetworkCall($"https://api.huobi.pro/market/history/kline?symbol={huobiString}&period=15min");
            var result = JsonConvert.DeserializeObject<CandlesHuobi>(response);
            if (result.status == "error") {
                throw new ArgumentException("NOT_FOUND");
            }
            return result;
        }

        public async Task<List<List<object>>> fetchBinanceCandles(string binanceString) {
            try {
                string response = await UtilClass.makeNetworkCall($"https://api.binance.com/api/v3/klines?symbol={binanceString}&interval=15m");
                var result = JsonConvert.DeserializeObject<List<List<object>>>(response);
                return result;
            } catch {
                throw new ArgumentException("NOT_FOUND");
            }
        }
        public List<Candles> formatHuobiCandles(CandlesHuobi candlesHuobi) {
            return candlesHuobi.data.Select(item => new Candles{
                id = item.id,
                open = item.open,
                high = item.high,
                low = item.low,
                close = item.close,
                volume = item.vol,
                source = "Huobi"
            }).ToList();
        }
        public List<Candles> formatBinanceCandles(List<List<object>> candlesBinance) {
            return candlesBinance.Select(item => new Candles{
                id = Convert.ToInt64(item[0]),
                open = Convert.ToDouble(item[1]),
                high = Convert.ToDouble(item[2]),
                low = Convert.ToDouble(item[3]),
                close = Convert.ToDouble(item[4]),
                volume = Convert.ToDouble(item[7]),
                source = "Binance"
            }).ToList();
        }
        public async Task<IEnumerable<Candles>> getCandles(string binanceString, string huobiString) {
            var binanceResult = this.fetchHuobiCandles(huobiString);
            var huobiResult = this.fetchBinanceCandles(binanceString);
            await Task.WhenAll(binanceResult, huobiResult);
            var binance = this.formatHuobiCandles(await binanceResult);
            var huobi = this.formatBinanceCandles(await huobiResult);
            var result = binance.Concat(huobi);
            return result;
        }
    }
}