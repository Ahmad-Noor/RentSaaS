using RentSaaS.Domain;
using RentSaaS.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace RentSaaS.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly ILogger<UserController> _logger; // ILogger takes the type of the class as a parameter
        private readonly IUnitOfWork _unitOfWork;

        public UserController(IUnitOfWork unitOfWork, ILogger<UserController> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        // GET: api/<UserController>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var countries = await _unitOfWork.Users.All();
            if (countries == null)
            {
                return NotFound();
            }
            return Ok(countries);
        }

        // GET api/<UserController>/5
        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetItem([FromRoute] Guid id)
        {
            var user = await _unitOfWork.Users.GetById(id);
            if (user != null)
            {
                return Ok(user);
            }
            return NotFound();
        }

        // POST api/<UserController>
        [HttpPost]
        public async Task<IActionResult> CreateUser(User user)
        {
            if (ModelState.IsValid)
            {
                
                await _unitOfWork.Users.Add(user);
                await _unitOfWork.CompleteAsync();
                return CreatedAtAction("GetItem", new { id = user.Id }, user);
            }

            return new JsonResult("Something went wrong") { StatusCode = 500 };

        }

        //PUT api/<UserController>
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(Guid id, User user)
        {
            if (id != user.Id)
            {
                return BadRequest();
            }
            await _unitOfWork.Users.Upsert(user);
            await _unitOfWork.CompleteAsync();

            return NoContent();
        }

        //DELETE api/<UserController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            User? user = await _unitOfWork.Users.GetById(id);
            if (user != null)
            {
                await _unitOfWork.Users.Delete(id);
                await _unitOfWork.CompleteAsync();
                return NoContent();
            }
            return NotFound(id);
        }
    }
}
