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

            var retval = ExecProc<int>("[dbo].[sp_addCustomer]", param);
            return retval;
        }

        bool IRepositoryInvoice.AddInvoice(Invoice invoice)
        {
            throw new NotImplementedException();
        }

        int IRepositoryInvoice.AddProduct(Product product)
        {
            throw new NotImplementedException();
        }

        IEnumerable<GetInvoice> IRepositoryInvoice.GetAllInvoices()
        {
            throw new NotImplementedException();
        }

        GetInvoice IRepositoryInvoice.GetInvoice(int id)
        {
            var retval = ExecProc<int>("[dbo].[sp_getAllInvoices]", null);
            return retval;
        }
    }
}
