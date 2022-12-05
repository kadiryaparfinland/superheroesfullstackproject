
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SuperHero.API.Data;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SuperHero.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class SuperHeroController : Controller
    {
        private readonly DataContext _context;

        public SuperHeroController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<SuperHero>>> GetSuperHeroes()
        {

            return Ok(await _context.SuperHeroes.ToListAsync());
        }

        [HttpPost]
        public async Task<ActionResult<List<SuperHero>>> CreateSuperHero(SuperHero hero)
        {
            _context.SuperHeroes.Add(hero);
            await _context.SaveChangesAsync();

            return Ok(await _context.SuperHeroes.ToListAsync());
        }

        [HttpPut]
        public async Task<ActionResult<List<SuperHero>>> UpdateSuperHero(SuperHero hero)
        {
            var dbHero = await _context.SuperHeroes.FindAsync(hero.Id);
            if(dbHero == null)
            {
                return BadRequest("Hero not found");
            }
            else
            {
                dbHero.Name = hero.Name;
                dbHero.FirstName = hero.FirstName;
                dbHero.LastName = hero.LastName;
                dbHero.Place = hero.Place;

                await _context.SaveChangesAsync();

                return Ok(await _context.SuperHeroes.ToListAsync());
            }

        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<ActionResult<List<SuperHero>>> DeleteSuperHero(int id)
        {
            var dbHero = await _context.SuperHeroes.FindAsync(id);
            if (dbHero == null)
            {
                return BadRequest("Hero not found");
            }
            else
            {
                _context.SuperHeroes.Remove(dbHero);

                await _context.SaveChangesAsync();

                return Ok(await _context.SuperHeroes.ToListAsync());
            }
        }
    }
}

