using Application.Features.User.Commands;
using Application.Features.User.Queries;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Infrastructure.Persistence.Repositories;
using Infrastructure.Persistence.Contexts;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Domain.Entities;
using Application.Interfaces.Repositories;

namespace WebApi.Controllers.v1.Gateway
{
    [EnableCors("CorsApi")]
    [ApiVersion("1.0")]

    public class UserController : BaseApiController
    {
        private readonly ApplicationDbContext _context;
        private readonly IUserRepositoryAsync _userRepository;

        public UserController(ApplicationDbContext context, IUserRepositoryAsync userRepository)
        {
            _context = context;
            _userRepository = userRepository; // Initialize the repository
        }

        // GET: api/<controller>
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] GetAllUser filter)
        {
            return Ok(await Mediator.Send(new GetAllUser { PageNumber = filter.PageNumber, PageSize = filter.PageSize }));
        }
        [HttpGet("GetActivePersons")]
        public async Task<IActionResult> GetActivePersons()
        {
            var activePersons = await _context.Person
                .Where(p => p.IsActive)
                .Select(p => new
                {
                    p.Code,
                    FirstName = p.FirstName +
                               (p.MiddleName != null && p.MiddleName.Length > 0 ? " " + p.MiddleName : string.Empty) +
                               " " + p.LastName,
                    SuggestedUserName = p.FirstName.ToLower() +
                                        (p.MiddleName != null && p.MiddleName.Length > 0 ? p.MiddleName.Substring(0, 1).ToUpper() : string.Empty)
                })
                .ToListAsync();

            return Ok(activePersons);
        }

        [HttpGet("check-username/{username}")]
        public async Task<IActionResult> CheckUserName(string username)
        {
            // Check if the repository is properly injected and used here
            var user = await _userRepository.GetUserByUserName(username);
            if (user != null)
            {
                return Ok(new { exists = true });
            }
            return Ok(new { exists = false });
        }

        // POST api/<controller>
        // [Authorize]

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateUser command)
        {
            return Ok(await Mediator.Send(command));
        }
       
        // PUT api/<controller>/5
        // [Authorize]
        [HttpPut("{code}")]
        public async Task<IActionResult> Put(long code, [FromBody] UpdateUser command)
        {
            if (code != command.Code)
            {
                return BadRequest();
            }
            return Ok(await Mediator.Send(command));
        }

        // DELETE api/<controller>/5
        // [Authorize]
        [HttpDelete("{code}")]
        public async Task<IActionResult> Delete(long code)
        {
            return Ok(await Mediator.Send(new DeleteUser { Code = code }));
        }
    }

}
