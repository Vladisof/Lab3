using BLL.DTOs;
using BLL.UnitOfWork;
using DAL.Models;

namespace BLL.Services;

public interface ISpectacleService
{
    Task<IEnumerable<Spectacle>> GetSpectaclesAsync();
    Task<Spectacle?> GetSpectacleAsync(int id);
    Task<Spectacle> CreateSpectacleAsync(SpectacleDto spectacleDto);
    Task<IEnumerable<Spectacle>> GetSpectaclesAsync(string choice, string value);
}

public class SpectacleService : ISpectacleService
{
    public SpectacleService(IUnitOfWork workModel)
    {
        WorkModel = workModel;
    }

    private IUnitOfWork WorkModel { get; set; }

    public async Task<IEnumerable<Spectacle>> GetSpectaclesAsync()
    {
        var spectacles = await WorkModel.Spectacles.GetAllAsync();

        return spectacles;
    }

    public async Task<Spectacle?> GetSpectacleAsync(int id)
    {
        var spectacle = await WorkModel.Spectacles.GetAsync(id);

        return spectacle;
    }

    public async Task<Spectacle> CreateSpectacleAsync(SpectacleDto spectacleDto)
    {
        var spectacle = new Spectacle
        {
            Name = spectacleDto.Name,
            Description = spectacleDto.Description,
            Genre = spectacleDto.Genre,
            Author = spectacleDto.Author,
            Date = spectacleDto.Date
        };

        await WorkModel.Spectacles.CreateAsync(spectacle);
        await WorkModel.SaveChangesAsync();

        return spectacle;
    }

    public async Task<IEnumerable<Spectacle>> GetSpectaclesAsync(string choice, string value)
    {
        if (string.IsNullOrEmpty(value))
            return await WorkModel.Spectacles.GetAllAsync();

        switch (choice)
        {
            case "name":
                return await WorkModel.Spectacles.GetAllAsync(s => s.Name.Contains(value));
            case "date":
                var date = DateTime.Parse(value);

                return await WorkModel.Spectacles.GetAllAsync(s => s.Date.Date == date.Date);
            case "author":
                return await WorkModel.Spectacles.GetAllAsync(s => s.Author.Contains(value));
            case "genre":
                return await WorkModel.Spectacles.GetAllAsync(s => s.Genre.Contains(value));
            default:
                return new List<Spectacle>();
        }
    }
}
