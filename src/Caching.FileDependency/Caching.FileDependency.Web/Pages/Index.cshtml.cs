using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.FileProviders;

namespace Caching.FileDependency.Web.Pages
{
    public class IndexModel : PageModel
    {
        private const string fileKey = "FileKey";
        private readonly ILogger<IndexModel> logger;
        private readonly IMemoryCache cache;
        private readonly IFileProvider fileProvider;

        [BindProperty]
        public string FilePath { get; set; } = @"C:\tmp\test.txt";


        [BindProperty]
        public string FileContent { get; set; }

        public IndexModel(IMemoryCache cache, ILogger<IndexModel> logger)
        {
            this.cache = cache;
            this.logger = logger;
            this.fileProvider = new PhysicalFileProvider(@"C:\tmp");
        }

        public async Task OnGet()
        {
            if (cache.TryGetValue(fileKey, out string? cachedContent))
            {
                FileContent = cachedContent!;
                logger.LogInformation("Cache hit: File Content Loaded from the cache");
            }
            else
            {
                FileContent = await System.IO.File.ReadAllTextAsync(FilePath);
                logger.LogInformation("Cache miss: File Content Loaded from the file");
                AddFileContentToCache();
            }
        }

        private void AddFileContentToCache()
        {
            var relativePath = Path.GetRelativePath(@"C:\tmp", FilePath);
            var changeToken = fileProvider.Watch(relativePath);

            //Combine more than one condition for cache expiration - OR logic
            var cacheEntryOptions = new MemoryCacheEntryOptions()
                   //.SetSlidingExpiration(TimeSpan.FromSeconds(20))
                   .AddExpirationToken(changeToken)
                   .RegisterPostEvictionCallback((key, value, reason, state) =>
                   {
                       logger.LogInformation("Cache entry '{key}' was evicted. Reason: {reason}", key, reason);
                   });

            cache.Set(fileKey, FileContent, cacheEntryOptions);
        }
    }
}
