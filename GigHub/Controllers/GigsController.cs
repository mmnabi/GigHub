using AutoMapper;
using GigHub.Core.Models;
using GigHub.Core.Resources;
using GigHub.Persistence;
using GigHub.Utilities.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GigHub.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class GigsController : BaseApiController
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GigsController(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllGigs()
        {
            var gigs = await _context.Gigs
                .Include(g => g.Artist)
                .Include(g => g.Genre)
                .ToListAsync();
            return OkResponse(_mapper.Map<List<GigResource>>(gigs));
        }

        [HttpPost]
        public async Task<IActionResult> CreateGig(SaveGigResource gigResource)
        {
            if (!ModelState.IsValid)
            {
                return BadModelStateResponse();
            }

            var genre = await _context.Genres.FindAsync(gigResource.GenreId);
            if (genre == null)
            {
                ModelState.AddModelError(nameof(gigResource.GenreId), "Invalid genreId.");
                return BadModelStateResponse();
            }

            var gig = _mapper.Map<SaveGigResource, Gig>(gigResource);

            gig.ArtistId = User.GetUserId();

            await _context.Gigs.AddAsync(gig);
            await _context.SaveChangesAsync();
            return OkResponse(gig);
        }

    }
}
