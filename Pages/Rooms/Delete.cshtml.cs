using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Ceng382_23_24_s_202111013;
using System.Threading.Tasks;

namespace Ceng382_23_24_s_202111013.Pages.Rooms;
public class DeleteModel : PageModel
{
    private readonly AppDbContext _context;

    public DeleteModel(AppDbContext context)
    {
        _context = context;
    }

    [BindProperty]
    public Room Room { get; set; }

    public async Task<IActionResult> OnGetAsync(int id)
    {
        Room = await _context.Rooms.FindAsync(id);

        if (Room == null)
        {
            return NotFound();
        }

        return Page();
    }

    public async Task<IActionResult> OnPostAsync(int id)
    {
        Room = await _context.Rooms.FindAsync(id);

        if (Room != null)
        {
            _context.Rooms.Remove(Room);
            await _context.SaveChangesAsync();
        }

        return RedirectToPage("./Index");
    }
}

