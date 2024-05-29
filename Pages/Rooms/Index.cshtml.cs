using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Ceng382_23_24_s_202111013;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Ceng382_23_24_s_202111013.Pages.Rooms;

public class IndexModel : PageModel
{
    private readonly AppDbContext _context;

    public IndexModel(AppDbContext context)
    {
        _context = context;
    }

    public IList<Room> Rooms { get; set; }

    public async Task OnGetAsync()
    {
        Rooms = await _context.Rooms.ToListAsync();
    }
}
