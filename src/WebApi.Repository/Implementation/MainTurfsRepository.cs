using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebApi.Persistence;
using WebApi.Repository.DTOs;
using WebApi.Repository.Interface;
using WebApi.Repository.Helpers;
namespace WebApi.Repository.Implementation
{
    public class MainTurfsRepository : IMainTurfsRepository
    {
        private readonly AppFootballTurfDbContext _context;

        public MainTurfsRepository(AppFootballTurfDbContext context)
        {
            _context = context;
        }

        public async Task<List<MainTurfDto>> GetMainTurfByIdUserAsync(Guid idUser)
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
            }).Where(x=>x.OwnerId == idUser).ToListAsync();
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

        public async Task<List<MainTurfDto>> SearchMainTurfAsync(SearchTurfDto searchTurfDto)
        {
            string convertString = string.Empty;
            var searchWithTypeTurf =  await _context.MainTurfs.Select(mt => new MainTurfDto
            {
                Id = mt.Id.ToString(),
                Name = mt.Name,
                Address = mt.Address,
                ImageLink = mt.ImageLinks,
                OwnerId = mt.OwnerId,
                OwnerName = mt.Owner.Name,
                OwnerPhoneNumber = mt.Owner.PhoneNumber,
                Longitude = mt.Longitude,
                Latitude = mt.Latitude,
                Turfs = mt.Turfs
            }).Where(x =>searchTurfDto.TurfType != null ? x.Turfs.Where(t => t.Type == searchTurfDto.TurfType).Any() : true).ToListAsync();

            if (!string.IsNullOrEmpty(searchTurfDto.Name))
            {
                convertString = searchTurfDto.Name.NonUnicodeAndToLowerAndRemoveSpace();
                searchWithTypeTurf = searchWithTypeTurf.Where(x => x.Name.NonUnicodeAndToLowerAndRemoveSpace().Contains(convertString)).ToList();
            }
            if (!(string.IsNullOrEmpty(searchTurfDto.Latitude)&&string.IsNullOrEmpty(searchTurfDto.Longitude)))
            {
                searchWithTypeTurf.Sort((mt1, mt2) =>
                    CoordinateHelper.Distance(mt1.Latitude,
                        mt1.Longitude,
                        mt2.Latitude,
                        mt2.Longitude));
            }
            return searchWithTypeTurf;
        }
    }


}
