using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApi.Persistence;
using WebApi.Repository.DTOs;
using WebApi.Repository.Interface;

namespace WebApi.Repository.Implementation
{
    public class MainTurfRepository : IMainTurfRepository
    {
        private readonly AppFootballTurfDbContext _context;

        public MainTurfRepository(AppFootballTurfDbContext context)
        {
            _context = context;
        }

        public async Task<MainTurfDto> GetMainTurfById(string id)
        {
            // await _context.MainTurfs.Select(mt=>new MainTurfDto
            // {
            //     Address = mt.Address,
            //     Id=mt.Id,
            //     ImageLink = mt
            // })
            throw new NotImplementedException();
        }
    }
}
