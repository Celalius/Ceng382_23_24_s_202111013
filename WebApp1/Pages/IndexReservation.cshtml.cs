using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WebApp1.Data;
using WebApp1.Models;

namespace MyApp.Namespace
{
    public class IndexReservationModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public IndexReservationModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Reservation> Reservations { get; set; }

        public async Task OnGetAsync(string? roomFilter, DateTime? startDateFilter, DateTime? endDateFilter, int? capacityFilter)
        {
            IQueryable<Reservation> reservationsQuery = _context.Reservations
                .Include(r => r.Room);

            if (!string.IsNullOrEmpty(roomFilter))
            {
                reservationsQuery = reservationsQuery.Where(r => r.Room.RoomName.Contains(roomFilter));
            }

            if (startDateFilter != null && endDateFilter != null)
            {
                reservationsQuery = reservationsQuery.Where(r => r.StartTime >= startDateFilter && r.EndTime <= endDateFilter);
            }

            if (capacityFilter != null)
            {
                reservationsQuery = reservationsQuery.Where(r => r.Room.Capacity >= capacityFilter);
            }

            Reservations = await reservationsQuery.ToListAsync();
        }

    }
}
