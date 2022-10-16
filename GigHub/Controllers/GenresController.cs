using AutoMapper;
using GigHub.Core.Resources;
using GigHub.Persistence;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GigHub.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class GenresController : BaseApiController
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GenresController(ApplicationDbContext context,
            IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetGenres()
        {
            var genres = await _context.Genres.ToListAsync();
            return OkResponse(_mapper.Map<List<GenreResource>>(genres));
        }
    }
}
