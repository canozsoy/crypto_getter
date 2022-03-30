namespace Catalog.Entities {

    public record Candles {
        public long id {get; init;}
        public double open {get;init;}
        public double high {get;init;}
        public double low {get;init;}
        public double close {get;init;}
        public double volume {get;init;}
        public string source {get; init;}
    }
    public record CandlesHuobi {
        public string ch {get; init;}
        public string status {get; init;}
        public long ts {get; init;}
        public List<CandlesHuobiData> data {get; init;}
    }

    public record CandlesHuobiData {
        public long id {get; init;}
        public double open {get; init;}
        public double close {get; init;}
        public double low {get; init;}
        public double high {get; init;}
        public double amount {get; init;}
        public double vol {get; init;}
        public int count {get; init;}

    }
}