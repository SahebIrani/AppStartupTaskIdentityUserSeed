using Demo.Data;

using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace Demo.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        public IndexModel(ILogger<IndexModel> logger, ApplicationDbContext context)
        {
            _logger = logger;
            Context = context;
        }

        public ApplicationDbContext Context { get; }

        public string UserName { get; set; }

        public void OnGet()
        {

        }
    }
}
