using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MoviesApi.Core.Consts;
using MoviesApi.Core.Dtos;
using MoviesApi.Core.IRepositories;
using MoviesApi.Core.Models;

namespace MoviesApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IdentityController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly IMapper _mapper;
        private readonly IAuthService _authService;

        public IdentityController(UserManager<User> userManager ,IMapper mapper , IAuthService authService)
        {
            _userManager = userManager;
            _mapper = mapper;
            _authService = authService;
        }

        [HttpPatch("UpdateUser")]
        public async Task<IActionResult> UpdateUserDetailsAsync(string id, UpdateUserDetailsDto userdto)
        {

            if (userdto == null)
                return BadRequest(ModelState);

            var dbUser = await _userManager.FindByIdAsync(id);


            if (dbUser == null)
                return NotFound("There is no user with this id");

            dbUser = _mapper.Map<User>(userdto);

            await _userManager.UpdateAsync(dbUser);



            return Ok(new {dbUser.Id,dbUser.DateOfBirth , dbUser.Nationality});
            
        }

        [HttpPost("AddRole")]
        public async Task<IActionResult> AddROleAsync([FromBody]AddRoleDto addRoleDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _authService.AddRoleAsync(addRoleDto);

            if (string.IsNullOrEmpty(result))
                return BadRequest(result);

            return Ok(addRoleDto);
        }

    }
}
