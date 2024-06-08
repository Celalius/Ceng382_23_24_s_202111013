using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApp1.Data;
using WebApp1.Models;
using Microsoft.Extensions.Logging;
using Serilog;


namespace MyApp.Namespace
{
    public class CreateReservationModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<CreateReservationModel> _logger;
        public CreateReservationModel(ApplicationDbContext context, ILogger<CreateReservationModel> logger)
        {
            _context = context;
            Log.Logger = new LoggerConfiguration()
                .WriteTo.File("logs/log-.txt",rollingInterval: RollingInterval.Day)
                .CreateLogger();
            
            _logger = new LoggerFactory().AddSerilog().CreateLogger<CreateReservationModel>();
        }

        [BindProperty]
        public Reservation? Reservation { get; set; }

        public List<Room>? Rooms { get; set; }

        public Room TempRoom {get; set;}

        public IActionResult OnGet()
        {
            Rooms = _context.Rooms.ToList();
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    _logger.LogWarning("Model state is invalid.");
                    foreach (var state in ModelState)
                    {
                        foreach (var error in state.Value.Errors)
                        {
                            _logger.LogWarning($"Model state error: {error.ErrorMessage}");
                        }
                    }
                    return Page();
                }
                

                // Get the logged-in user's information
                var userName = User.Identity.Name;
                if (Reservation != null)
                {   
                    Reservation.Room = TempRoom;
                    Reservation.ReserverName = userName;
                    _context.Reservations.Add(Reservation);
                    await _context.SaveChangesAsync();

                    _logger.LogInformation($"Reservation created by user {userName} for room {Reservation.RoomId} from {Reservation.StartTime} to {Reservation.EndTime}");

                    return RedirectToPage("./Index");
                }

                _logger.LogError("Reservation model is null.");
                return Page();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating reservation.");
                return RedirectToPage("./Error");
            }
        }
    }
}
