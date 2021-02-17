using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Example.Controllers
{
    [Route("api/data")]
    [ApiController]
    public class DataController : ControllerBase
    {
        // GET: api/<DataController>
        [HttpGet]
        public IEnumerable<User> Get()
        {
            return Data.GetData().ToArray();
        }

        // Get a single user
        [HttpGet("{id}")]
        public User Get(int id)
        {
            User user = Data.GetUser(id);
            Response.StatusCode = 200;
            return user;
        }

        // POST api/<DataController>
        [HttpPost]
        public IActionResult Post([FromBody] User value)
        {
            if (value.FirstName == "" || value.LastName == "")
            {
                return BadRequest();
            }
            Data.PushData(value);
            return Ok();
        }

        // DELETE api/data
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            bool exisist = Data.Delete(id);
            if (exisist) return Ok();
            else return NotFound();
        }

        // PUT api/data
        [HttpPut("{id}")]
        public IActionResult Put([FromBody] User user, int id)
        {
            Data.Edit(id, user);
            return Ok();
        }
    }
}