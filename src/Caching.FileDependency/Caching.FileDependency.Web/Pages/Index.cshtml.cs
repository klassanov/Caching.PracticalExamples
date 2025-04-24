using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Caching.FileDependency.Web.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        

        [BindProperty]
        public string FilePath { get; set; } = @"C:\tmp\test.txt";


        [BindProperty]
        public string FileContent { get; set; } = string.Empty;

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        public async Task OnGet()
        {
            FileContent = await System.IO.File.ReadAllTextAsync (FilePath);
        }
    }
}
