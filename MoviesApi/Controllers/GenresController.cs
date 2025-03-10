using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MoviesApi.Core.Dtos;
using MoviesApi.Core.IRepositories;
using MoviesApi.Core.Models;
using MoviesApi.DataAccess.Repositories;
using System.Linq.Expressions;

namespace MoviesApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GenresController : ControllerBase
    {
        private readonly IUniteOfWork _uniteOfWork;

        public GenresController(IUniteOfWork uniteOfWork)
        {
            _uniteOfWork = uniteOfWork;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllAsync(int? take = null, int? skip = null, string orderBy = "Name", string orderDirection = "ASC")
        {

            //get all genre
            Expression<Func<Genre, bool>> criteria = g => true;

            // ordering by Name
            Expression<Func<Genre, object>> orderByExpression = g => g.Name;

            if (orderBy.ToLower() == "id")
                orderByExpression = g => g.Id;

            var genres = await _uniteOfWork.Genres.GetAllAsync(criteria, take, skip, orderByExpression, orderDirection);

            return Ok(genres);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            var entity = await _uniteOfWork.Genres.GetByIdAsync(id);
            return Ok(entity);
        }


        [HttpPost]
        public async Task<IActionResult> AddAsync(GenreDto genreDto)
        {
            var genre = new Genre { Name = genreDto.Name };

            await _uniteOfWork.Genres.AddAsync(genre);

            await _uniteOfWork.SaveAsync();

            return Ok(genre);

        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] GenreDto genreDto)
        {
            var genre = await _uniteOfWork.Genres.GetByIdAsync(id);

            if (genre == null)
                return NotFound($"There Is No Genre With this Id{id}");

            genre.Name = genreDto.Name;

            _uniteOfWork.Genres.Update(genre);
            await _uniteOfWork.SaveAsync();

            return Ok(genre);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var genre = await _uniteOfWork.Genres.GetByIdAsync(id);
            
            if(genre == null)
                return NotFound($"There Is No Genre With this Id {id}");


            _uniteOfWork.Genres.Delete(genre);
            await _uniteOfWork.SaveAsync();

            return Ok(genre);
        }
    }
}
