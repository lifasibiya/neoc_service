using Microsoft.AspNetCore.Mvc;
using neoc.Models;

namespace neoc.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductController : Controller
    {
        private readonly IRepositoryInvoice _repositoryInvoice;
        public ProductController(IRepositoryInvoice repositoryInvoice)
        {
            _repositoryInvoice = repositoryInvoice;
        }

        [HttpPost]
        [Route("create")]
        public dynamic CreateProduct([FromBody] Product product)
        {
            try
            {
                return _repositoryInvoice.AddProduct(product);
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
