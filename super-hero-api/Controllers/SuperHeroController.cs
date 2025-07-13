using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using super_hero_api.Data;
using super_hero_api.Dtos;
using super_hero_api.Entities;

namespace super_hero_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SuperHeroController : ControllerBase
    {
        private readonly DataContext _context;

        public SuperHeroController(DataContext context)
        {
            _context = context;
        }

        [HttpGet("get-all-heroes")]
        public async Task<ActionResult<List<SuperHero>>> GetAllHeroes()
        {
            var heroes = await _context.SuperHeros.ToListAsync();
            return Ok(heroes);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<List<SuperHero>>> GetHero(int id)
        {
            var hero = await _context.SuperHeros.FindAsync(id);
            if(hero == null)
            {
                return NotFound("Hero not foound.");
            }
            return Ok(hero);
        }
        [HttpPost("add-a-hero")]
        public async Task<ActionResult<List<SuperHero>>> AddHero([FromBody] HeroCreateDto  dto)
        {
            var Hero = new SuperHero
            {
                Name = dto.Name,
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                Place = dto.Place
            };
            _context.SuperHeros.Add(Hero);
            await _context.SaveChangesAsync();
            return Ok(await _context.SuperHeros.ToListAsync());
        }
        [HttpPut("{id}")]
        public async Task<ActionResult<List<SuperHero>>> UpdateHero(int id, HeroUpdateDto dto)
        {
            var hero = await _context.SuperHeros.FindAsync(id);
            if (hero == null)
            return NotFound("Hero not found.");

            hero.Name = dto.Name;
            hero.FirstName = dto.FirstName;
            hero.LastName = dto.LastName;
            hero.Place = dto.Place;
            
            await _context.SaveChangesAsync();
            return Ok(await _context.SuperHeros.ToListAsync());
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult<List<SuperHero>>> DeleteHero(int id)
        {
            var hero = await _context.SuperHeros.FindAsync(id);
            if (hero == null)
            {
                return NotFound("Hero not foound.");
            }
           _context.SuperHeros.Remove(hero);
            await _context.SaveChangesAsync();
            
            return Ok(await _context.SuperHeros.ToListAsync());
        }
    }
}

