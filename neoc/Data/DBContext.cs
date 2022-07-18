using Microsoft.EntityFrameworkCore;

namespace neoc.Data
{
    public class NEOCDBContext: DbContext
    {
        public NEOCDBContext(DbContextOptions<NEOCDBContext> options):base(options)
        {

        }
    }
}
