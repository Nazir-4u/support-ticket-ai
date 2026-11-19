using System.Collections.Generic;
using System.Linq;

public interface ITicketRepository
{
    Ticket Save(Ticket ticket);
    IEnumerable<Ticket> GetAll();
}

public class TicketRepository : ITicketRepository
{
    private static readonly List<Ticket> Tickets = new();

    public Ticket Save(Ticket ticket)
    {
        if (ticket == null)
        {
            throw new ArgumentNullException(nameof(ticket));
        }

        ticket.Id = Tickets.Count == 0 ? 1 : Tickets.Max(t => t.Id) + 1;
        Tickets.Add(ticket);

        return ticket;
    }

    public IEnumerable<Ticket> GetAll()
    {
        return Tickets.AsReadOnly();
    }
}