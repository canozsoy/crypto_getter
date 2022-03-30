namespace Catalog.Entities
{
    public record RecentTradeBinance {
        public string id {get; init;}
        public double price {get; init;}
        public double qty {get; init;}
        public double quoteQty {get; init;}
        public long time {get; init;}
        public bool isBuyerMaker {get; init;}
        public bool isBestMatch {get; init;}
    }

    public record RecentTradeHuobi {
        public string ch {get; init;}
        public string status {get; init;}
        public long ts {get; init;}

        public List<HuobiDataObjectOuter> data {get; init;}

    }

    public record HuobiDataObjectOuter {
        public string id {get; init;}
        public long ts {get; init;}
        public List<HuobiDataObjectInner> data {get; init;}
    }

    public record HuobiDataObjectInner {
        public string id {get; init;}
        public long ts {get; init;}

        public string trade_id {get; init;}

        public double amount {get; init;}

        public double price {get; init;}

        public string direction {get; init;}
    }

    public record RecentTrade {
        public string id {get; init;}
        public double price {get; init;}
        public double qty {get; init;}
        public long time {get; init;}
        public string source {get; init;}
    }
}