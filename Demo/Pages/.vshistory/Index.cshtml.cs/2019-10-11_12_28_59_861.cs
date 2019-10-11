using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using Demo.Data;

using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
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

        public async Task OnGetAsync(CancellationToken cancellationToken)
        {
            UserName = await Context.Users.AsNoTracking().Select(c => c.UserName).FirstOrDefaultAsync(cancellationToken);
        }
    }
}
