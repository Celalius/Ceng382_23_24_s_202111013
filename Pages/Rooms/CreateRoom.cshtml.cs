using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Ceng382_23_24_s_202111013;
namespace Ceng382_23_24_s_202111013.Pages.Rooms;

public class CreateRoomModel : PageModel
{
    private readonly AppDbContext _context;

    public CreateRoomModel(AppDbContext context)
    {
        _context = context;
    }

    [BindProperty]
    public Room? Room { get; set; }

    public void OnGet()
    {
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }
        if(Room != null){
        _context.Rooms.Add(Room);
        await _context.SaveChangesAsync();
        }
        return RedirectToPage("./Index");
    }
}
