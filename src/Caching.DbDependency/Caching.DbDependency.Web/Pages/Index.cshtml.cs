using Caching.DbDependency.Repository.Entities;
using Caching.DbDependency.Web.GetProductsFeature;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Caching.DbDependency.Web.Pages
{
    public class IndexModel : PageModel
    {
        private readonly IProductsManager productsManager;

        [BindProperty]
        public IEnumerable<Product> Products { get; set; } = [];

        public IndexModel(ILogger<IndexModel> logger, IProductsManager productsRepository)
        {
            _logger = logger;
            productsManager = productsRepository;
        }

        public async Task OnGet()
        {
            Products = await productsManager.GetAllProductsAsync();
        }
    }
}
