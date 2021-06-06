using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NLayerProject.Core.Models;
using NLayerProject.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NLayerProject.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonsController : ControllerBase
    {
        private IService<Person> _service;

        public PersonsController(IService<Person> service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var personels = await _service.GetAllAsync();
            return Ok(personels);
        }

        [HttpPost]
        public async Task<IActionResult> Save(Person person)
        {
            var dbItem = await _service.AddAsync(person);

            return Created(string.Empty, dbItem);
        }
    }
}
