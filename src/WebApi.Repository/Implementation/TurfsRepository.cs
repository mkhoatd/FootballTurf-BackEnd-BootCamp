using Microsoft.EntityFrameworkCore;
using WebApi.Persistence;
using WebApi.Repository.DTOs;
using WebApi.Repository.Interface;

namespace WebApi.Repository.Implementation;

public class TurfsRepository : ITurfsRepository
{
    private readonly AppFootballTurfDbContext _context;

    public TurfsRepository(AppFootballTurfDbContext context)
    {
        _context = context;
    }

    public async Task<List<TurfDto>> GetTurfsInMainTurf(string mainTurfId)
    {
        return await _context.Turfs.Where(t=>t.MainTurfId==Guid.Parse(mainTurfId))
            .Select(t => new TurfDto
        {
            Id = t.Id.ToString(),
            ImageLinks = t.ImageLinks,
            Name = t.Name,
            Rating = t.Rating,
            TurfType = t.Type.ToString()
        }).ToListAsync();
    }
}