using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApp1.Data;
using WebApp1.Models;

namespace MyApp.Namespace
{
    public class DeleteModel : PageModel
    {
         private readonly ApplicationDbContext _context;

    public DeleteModel(ApplicationDbContext context)
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
}
