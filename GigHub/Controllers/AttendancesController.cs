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
    public class AttendancesController : BaseApiController
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public AttendancesController(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> Attand(AttendanceResource dto)
        {
            var userId = User.GetUserId();

            if (await _context.Attendances.AsNoTracking().AnyAsync(e => e.AttendeeId == userId && e.GigId == dto.GigId))
                return BadRequest("Attendance already exists!");

            var attandance = new Attendance
            {
                GigId = dto.GigId,
                AttendeeId = userId
            };
            await _context.Attendances.AddAsync(attandance);
            await _context.SaveChangesAsync();

            return OkResponse(attandance);
        }
    }
}
