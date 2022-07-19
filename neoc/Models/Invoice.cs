namespace neoc.Models
{
    public class Invoice
    {
        public int id { get; set; }
        public DateTime date { get; set; }
        public int customer { get; set; }
        public int product { get; set; }
        public int quantity { get; set; } = 0;
        public double lineTotal { get; set; }
    }
}
