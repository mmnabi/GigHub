using GigHub.Configuration;
using GigHub.Core.Models;
using GigHub.Core.Resources;
using GigHub.Persistence;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace GigHub.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class GenresController : BaseApiController
    {
        private readonly ApplicationDbContext _context;
        //private readonly IMapper _mapper;

        public GenresController(ApplicationDbContext context)//,
            //IMapper mapper)
        {
            _context = context;
            //_mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetGenres()
        {
            //var genres = await _context.Genres.ToListAsync();
            //return Ok(_mapper.Map<List<GenreResource>>(genres));
            var genres = new List<string> { "hi", "there" };
            return OkResponse(genres);
        }
    }
}
