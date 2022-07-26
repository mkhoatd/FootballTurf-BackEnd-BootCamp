using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebApi.Persistence;
using WebApi.Repository.DTOs;
using WebApi.Repository.Interface;

namespace WebApi.Repository.Implementation
{
    public class MainTurfsRepository : IMainTurfsRepository
    {
        private readonly AppFootballTurfDbContext _context;

        public MainTurfsRepository(AppFootballTurfDbContext context)
        {
            _context = context;
        }

        public async Task<MainTurfDto> GetMainTurfByIdAsync(string id)
        {
            return await _context.MainTurfs.Select(mt => new MainTurfDto
            {
                Id = mt.Id.ToString(),
                Name = mt.Name,
                Address = mt.Address,
                ImageLink = mt.ImageLinks,
                OwnerId = mt.OwnerId,
                OwnerName = mt.Owner.Name,
                OwnerPhoneNumber = mt.Owner.PhoneNumber,
                Longitude = mt.Longitude,
                Latitude = mt.Latitude
            }).FirstOrDefaultAsync(mt=>mt.Id==id);
        }

        public async Task<List<MainTurfDto>> GetAllMainTurfsAsync()
        {
            return await _context.MainTurfs.Select(mt => new MainTurfDto
            {
                Id = mt.Id.ToString(),
                Name = mt.Name,
                Address = mt.Address,
                ImageLink = mt.ImageLinks,
                OwnerId = mt.OwnerId,
                OwnerName = mt.Owner.Name,
                OwnerPhoneNumber = mt.Owner.PhoneNumber,
                Longitude = mt.Longitude,
                Latitude = mt.Latitude
            }).ToListAsync();
        }
    }
}
