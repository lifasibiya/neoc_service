using Microsoft.AspNetCore.Mvc;
using neoc.Models;

namespace neoc.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CustomerController : Controller
    {
        private readonly IRepositoryInvoice _repositoryInvoice;
        public CustomerController(IRepositoryInvoice repositoryInvoice)
        {
            _repositoryInvoice = repositoryInvoice;
        }

        [HttpPost]
        [Route("create")]
        public dynamic CreateCustomer([FromBody] Customer customer)
        {
            try
            {
                return _repositoryInvoice.AddCustomer(customer);
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
