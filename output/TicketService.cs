using System.ComponentModel.DataAnnotations;

public interface ITicketService
{
    Ticket CreateTicket(TicketDto dto);
    IEnumerable<Ticket> GetAll();
}

public class TicketService : ITicketService
{
    private readonly ITicketRepository _repository;

    public TicketService(ITicketRepository repository)
    {
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
    }

    public Ticket CreateTicket(TicketDto dto)
    {
        if (dto == null)
        {
            throw new ArgumentNullException(nameof(dto));
        }

        if (string.IsNullOrWhiteSpace(dto.Title))
        {
            throw new ValidationException("Title is required.");
        }

        if (string.IsNullOrWhiteSpace(dto.Description))
        {
            throw new ValidationException("Description is required.");
        }

        var ticket = new Ticket
        {
            Title = dto.Title.Trim(),
            Description = dto.Description.Trim(),
            Status = "Open"
        };

        return _repository.Save(ticket);
    }

    public IEnumerable<Ticket> GetAll()
    {
        return _repository.GetAll();
    }
}