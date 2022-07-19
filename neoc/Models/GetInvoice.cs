namespace neoc.Models
{
    public class GetInvoice
    {
        public int id { get; set; }
        public DateTime date { get; set; }
        public double lineTotal { get; set; }
        public int quantity { get; set; }
        public string name { get; set; }
        public string address { get; set; }
        public string tel { get; set; }
        public string desc { get; set; }
        public double price { get; set; }
    }
}
