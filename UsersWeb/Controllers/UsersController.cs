using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using UsersWeb.Interfaces;
using UsersWeb.Models;

namespace UsersWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        public IUserRepository Repository;
        public UsersController(IUserRepository repository)
        {
            Repository = repository;
        }

        [HttpPost("CreateUser")]
        public async Task<IActionResult> CreateUser(User u)
        {
            try
            {
                await Repository.AddAsync(u);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpDelete("DeleteById/{id}")]
        public async Task<IActionResult> DeleteById(int id)
        {
            try
            {
                if (await Repository.DeleteAsync(id))
                    return Ok();
                return NotFound();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPut("Validate")]
        public async Task<IActionResult> Validate(User u)
        {
            try
            {

                if (await Repository.Validate(u.UserName, u.Password))
                    return Ok();
                else
                    return Unauthorized();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

    }
}
