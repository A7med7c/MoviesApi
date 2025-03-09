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
        public async Task<IActionResult> GetAllAsync(
            int? take = null,
             int? skip = null,
             string orderBy = "Name",
             string orderDirection = "ASC")
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
            
        
        [HttpPost]
        public async Task<IActionResult> AddAsync(CreateGenreDto genreDto)
        {
            var genre = new Genre { Name = genreDto.Name};
          
            await _uniteOfWork.Genres.AddAsync(genre);

            _uniteOfWork.Save();

            return Ok(genre);
            
        }
    }
}
