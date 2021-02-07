using Kolokwium2_poprawa.DTOs;
using Kolokwium2_poprawa.Models;
using Kolokwium2_poprawa.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kolokwium2_poprawa.Controllers
{
    [Route("api/pets")]
    [ApiController]
    public class PetController : ControllerBase
    {
        private readonly IPetsDbService _context;
        public PetController(IPetsDbService context)
        {
            _context = context;
        }

        [HttpGet("{year}")]
        public IActionResult GetTeamsWithScores(int? year)
        {
            if (year > DateTime.Now.Year)
                return BadRequest("Ukazany zostal rok z przyszlosci");
            var result = _context.GetPetsByYear(year);
            return Ok(result);
        }
        [HttpPost]
        public IActionResult AddPet(AddPetRequest pet)
        {
            var result = _context.AddPet(pet);
            if (result)
            {
                return Ok();
            }
            return BadRequest();
        }
    }
}
