using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FilmsController : ControllerBase
    {
        private static List<Film> _films = new List<Film>
        {
            new Film(1, "Film 1", 25.99m, new DateTime(2023, 1, 1)),
            new Film(2, "Film 2", 19.99m, new DateTime(2023, 2, 1)),
            new Film(3, "Film 3", 35.99m, new DateTime(2023, 3, 1))
        };

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_films);
        }

        [HttpGet("{id}")]
        public IActionResult GetByID(int id)
        {
            var film = _films.FirstOrDefault(f => f.Id == id);
            if (film == null)
            {
                return NotFound();
            }
            return Ok(film);
        }

        [HttpPost]
        public IActionResult Post([FromBody] Film film)
        {
            if (film == null)
            {
                return BadRequest();
            }

            var newId = _films.Max(f => f.Id) + 1;
            film.Id = newId;
            _films.Add(film);
            return Ok(true);
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Film film)
        {
            if (film == null || film.Id != id)
            {
                return BadRequest();
            }

            var existingFilm = _films.FirstOrDefault(f => f.Id == id);
            if (existingFilm == null)
            {
                return NotFound();
            }

            existingFilm.Tytul = film.Tytul;
            existingFilm.Cena = film.Cena;
            existingFilm.DataPremiery = film.DataPremiery;

            return Ok(true);
        }
    }

    public class Film
    {
        public int Id { get; set; }
        public string Tytul { get; set; }
        public decimal Cena { get; set; }
        public DateTime DataPremiery { get; set; }

        public Film(int id, string tytul, decimal cena, DateTime dataPremiery)
        {
            Id = id;
            Tytul = tytul;
            Cena = cena;
            DataPremiery = dataPremiery;
        }
    }
}