using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WebApp1.Data;
using WebApp1.Models;

namespace MyApp.Namespace
{
    public class RoomsModel : PageModel
    {
        private readonly ApplicationDbContext _context;

    public RoomsModel(ApplicationDbContext context)
    {
        _context = context;
    }

    public IList<Room>? Rooms { get; set; }

    public async Task OnGetAsync()
    {
        Rooms = await _context.Rooms.ToListAsync();
    }
    }
}
