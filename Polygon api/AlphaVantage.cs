namespace Polygon_api
{
    public class AlphaVantage
    {
        public int Id { get; set; }

        public string Ticker { get; set; }
        public DateTime Date { get; set; }

        public decimal O { get; set; }
        public decimal H { get; set; }
        public decimal L { get; set; }
        public decimal C { get; set; }
        public long V { get; set; }
    }
}
