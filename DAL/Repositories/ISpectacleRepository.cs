using System.Linq.Expressions;
using DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories;

public interface ISpectacleRepository
{
    Task<IEnumerable<Spectacle>> GetAllAsync();
    Task<IEnumerable<Spectacle>> GetAllAsync(Expression<Func<Spectacle, bool>> predicate);
    Task<Spectacle?> GetAsync(int id);
    Task<Spectacle> CreateAsync(Spectacle spectacle);
}

public class SpectacleRepository : ISpectacleRepository
{
    private readonly TheaterDbContext _context;

    public SpectacleRepository(TheaterDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Spectacle>> GetAllAsync()
    {
        return await _context.Spectacles
            .Include(s => s.Tickets)
            .ToListAsync();
    }

    public async Task<IEnumerable<Spectacle>> GetAllAsync(Expression<Func<Spectacle, bool>> predicate)
    {
        return await _context.Spectacles
            .Where(predicate)
            .Include(s => s.Tickets)
            .ToListAsync();
    }

    public async Task<Spectacle?> GetAsync(int id)
    {
        return await _context.Spectacles
            .Include(s => s.Tickets)
            .FirstOrDefaultAsync(s => s.Id == id);
    }

    public async Task<Spectacle> CreateAsync(Spectacle spectacle)
    {
        await _context.Spectacles.AddAsync(spectacle);
        return spectacle;
    }
}
