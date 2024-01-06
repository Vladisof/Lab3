using DAL;
using DAL.Repositories;

namespace BLL.UnitOfWork;

public interface IUnitOfWork
{
    ISpectacleRepository Spectacles { get; }
    ITicketRepository Tickets { get; }

    Task<int> SaveChangesAsync();
}

public class UnitOfWork : IUnitOfWork
{
    private readonly TheaterDbContext _context;

    public UnitOfWork(TheaterDbContext context)
    {
        _context = context;
        Spectacles = new SpectacleRepository(_context);
        Tickets = new TicketRepository(_context);
    }

    public ISpectacleRepository Spectacles { get; }
    public ITicketRepository Tickets { get; }

    public async Task<int> SaveChangesAsync()
    {
        return await _context.SaveChangesAsync();
    }
}
