namespace neoc.Models
{
    public interface IRepositoryInvoice
    {
        int AddCustomer(Customer customer);
        int AddProduct(Product product);
        bool AddInvoice(Invoice invoice);
        GetInvoice GetInvoice(int id);
        IEnumerable<GetInvoice> GetAllInvoices();
    }
}
