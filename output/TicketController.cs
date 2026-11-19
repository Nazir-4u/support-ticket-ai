using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/tickets")]
public class TicketController : ControllerBase
{
    private readonly ITicketService _ticketService;

    public TicketController(ITicketService ticketService)
    {
        _ticketService = ticketService ?? throw new ArgumentNullException(nameof(ticketService));
    }

    [HttpGet]
    public ActionResult<IEnumerable<Ticket>> GetAll()
    {
        var tickets = _ticketService.GetAll();
        return Ok(tickets);
    }

    [HttpPost]
    public ActionResult<Ticket> Create([FromBody] TicketDto dto)
    {
        if (!ModelState.IsValid)
        {
            return ValidationProblem(ModelState);
        }

        try
        {
            var ticket = _ticketService.CreateTicket(dto);
            return CreatedAtAction(nameof(Create), new { id = ticket.Id }, ticket);
        }
        catch (ValidationException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }
}