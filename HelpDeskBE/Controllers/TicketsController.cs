using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HelpDeskBE.Models;
using System.Net.Sockets;

namespace HelpDeskBE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TicketsController : ControllerBase
    {
        private readonly TicketDbContext _context;

        public TicketsController(TicketDbContext context)
        {
            _context = context;
        }

        // GET: api/Tickets
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Ticket>>> GetTickets()
        {
            return await _context.Tickets.ToListAsync();
            //return await _context.Tickets.Where(x => x.Favorited == true).ToListAsync();
        }

        // GET: api/Tickets/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Ticket>> GetTicket(int id)
        {
            var ticket = await _context.Tickets.FindAsync(id);

            if (ticket == null)
            {
                return NotFound();
            }

            return ticket;
        }
        

        // PUT: api/Tickets/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("resolve/{id}")]
        public async Task<IActionResult> ResolveTicket(int id, string resolution, string username)
        {
            try
            {
                var ticket = await _context.Tickets.FindAsync(id);
                ticket.Resolution = resolution;
                ticket.ResolvedBy = username;
                ticket.Resolved = true;
                _context.Update(ticket);
                await _context.SaveChangesAsync();
                return Ok();
            }
            catch
            {
                return BadRequest();
            }
        }
        [HttpPut("addfavorite/{id}")]
        public async Task<IActionResult> FavoriteTicket (int id, string username)
        {
            var ticket = await _context.Tickets.FindAsync(id);
            ticket.Favorited = true;
            await _context.SaveChangesAsync();
            var ft = new Favorite
            {
                FavoritedBy = username,
                TicketId = id
            };
            _context.Favorites.Add(ft);
            await _context.SaveChangesAsync();
            return Ok();
        }

        // POST: api/Tickets
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Ticket>> PostTicket(string name, string issue)
        {
            var newTicket = new Ticket
            {
                OpenedBy = name,
                Issue = issue,
                Resolution = null,
                ResolvedBy = null,
                Resolved = false,
                Favorited = false,
            };
            _context.Tickets.Add(newTicket);
            await _context.SaveChangesAsync();
            return newTicket;
        }

        // DELETE: api/Tickets/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTicket(int id)
        {
            var ticket = await _context.Tickets.FindAsync(id);
            if (ticket == null)
            {
                return NotFound();
            }

            _context.Tickets.Remove(ticket);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TicketExists(int id)
        {
            return _context.Tickets.Any(e => e.Id == id);
        }
    }
}
