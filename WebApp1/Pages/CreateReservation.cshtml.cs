using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApp1.Data;
using WebApp1.Models;

namespace MyApp.Namespace
{
    public class CreateReservationModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public CreateReservationModel(ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Reservation? Reservation { get; set; }

        public List<Room> Rooms { get; set; }

        public IActionResult OnGet()
        {
            Rooms = _context.Rooms.ToList();
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            // Get the logged-in user's information
            var userName = User.Identity.Name;

            Reservation.ReserverName = userName;
            _context.Reservations.Add(Reservation);
            await _context.SaveChangesAsync();

            // Log reservation creation
            // Add your logging code here

            return RedirectToPage("./Index");
        }
    }
}
