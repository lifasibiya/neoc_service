using Dapper;
using System.Data.SqlClient;
using System.Collections.Generic;

namespace neoc.Models
{
    public class InvoiceManagement: IRepositoryInvoice
    {
        private static string? _configuration;
        public InvoiceManagement(IConfiguration configuration)
        {
            _configuration = configuration["ConnectionStrings:neoc"];
        }

        public static SqlConnection GetConnection()
        {
            return new SqlConnection(_configuration);
        }

        public dynamic ExecProc<T>(string proc, DynamicParameters? param)
        {
            return GetConnection().Query<T>(proc, param, commandType: System.Data.CommandType.StoredProcedure);  
        }

        int IRepositoryInvoice.AddCustomer(Customer customer)
        {
            var param = new DynamicParameters();
            param.Add("name", customer.name, System.Data.DbType.String, System.Data.ParameterDirection.Input);
            param.Add("address", customer.address, System.Data.DbType.String, System.Data.ParameterDirection.Input);
            param.Add("tel", customer.tel, System.Data.DbType.String, System.Data.ParameterDirection.Input);

            IEnumerable<int> retval = ExecProc<int>("[dbo].[sp_addCustomer]", param);
            return retval.FirstOrDefault();
        }

        bool IRepositoryInvoice.AddInvoice(Invoice invoice)
        {
            var param = new DynamicParameters();
            param.Add("customer", invoice.customer, System.Data.DbType.Int16, System.Data.ParameterDirection.Input);
            param.Add("product", invoice.product, System.Data.DbType.Int16, System.Data.ParameterDirection.Input);
            param.Add("quantity", invoice.quantity, System.Data.DbType.Int16, System.Data.ParameterDirection.Input);

            IEnumerable<bool> retval = ExecProc<bool>("[dbo].[sp_addInvoice]", param);
            return retval.FirstOrDefault();
        }

        int IRepositoryInvoice.AddProduct(Product product)
        {
            var param = new DynamicParameters();
            param.Add("desc", product.desc, System.Data.DbType.String, System.Data.ParameterDirection.Input);
            param.Add("price", product.price, System.Data.DbType.Double, System.Data.ParameterDirection.Input);

            IEnumerable<int> retval = ExecProc<int>("[dbo].[sp_addProduct]", param);
            return retval.FirstOrDefault();
        }

        IEnumerable<GetInvoice> IRepositoryInvoice.GetAllInvoices()
        {
            IEnumerable<GetInvoice> retval = ExecProc<GetInvoice>("[dbo].[sp_getAllInvoices]", null);
            return retval;
        }

        GetInvoice? IRepositoryInvoice.GetInvoice(int id)
        {
            var param = new DynamicParameters();
            param.Add("id", id, System.Data.DbType.Int16, System.Data.ParameterDirection.Input);

            IEnumerable<GetInvoice> retval = ExecProc<GetInvoice>("[dbo].[sp_getInvoice]", param);
            return retval.FirstOrDefault();
        }
    }
}
