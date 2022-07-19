using Microsoft.AspNetCore.Mvc;
using neoc.Models;

namespace neoc.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class InvoiceController : Controller
    {
        private readonly IRepositoryInvoice _repositoryInvoice;
        public InvoiceController(IRepositoryInvoice repositoryInvoice)
        {
            _repositoryInvoice = repositoryInvoice;
        }

        [HttpGet]
        [Route("get")]
        public dynamic GetAllInvoices()
        {
            try
            {
                return _repositoryInvoice.GetAllInvoices();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        [HttpGet]
        [Route("get/{id}")]
        public dynamic GetInvoice(int id)
        {
            try
            {
                return _repositoryInvoice.GetInvoice(id);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        [HttpPost]
        [Route("create")]
        public dynamic CreateInvoice([FromBody] Invoice invoice)
        {
            try
            {
                return _repositoryInvoice.AddInvoice(invoice);
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
